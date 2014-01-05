﻿using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TestTechnology.Controller.DTO;
using TestTechnology.Controller.Interface;
using TestTechnology.Shared.DTO;
using TestTechnology.TestClient.Utilities;

namespace TestTechnology.TestClient
{
    public class LocalJobManager : IJobCallbackService
    {
        private static readonly string CurrentClientID = ConfigurationManager.AppSettings.Get("ClientId");
        private static readonly Queue<Job> JobQueue = new Queue<Job>();
        private static readonly NameValueCollection ConnectionManagerDatabaseServers = ConfigurationManager.GetSection("JobCorrectReturnValues") as NameValueCollection;

        public bool HasJobRunning { get; set; }

        public void DoTestJobs(string clientID, JobGroup jobGroup)
        {

        }

        public void ExecuteTestJobs(string clientID, JobGroup jobGroup)
        {
            Console.WriteLine("ClientID = " + clientID);
            Console.WriteLine("JobGroupID = " + jobGroup.JobGroupID);
            Console.WriteLine("Thread ID = " + Thread.CurrentThread.ManagedThreadId);

            //Verify if this job group is for current client
            if (!string.Equals(CurrentClientID, clientID, StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine(string.Format("Client ID '{0}' is not for current client, drop it!!", clientID));
                return;
            }

            HasJobRunning = true;
            try
            {
                foreach (var job in jobGroup.Jobs)
                {
                    bool result = DoJob(job);
                    if (result)
                    {
                        CommandLineHelper.Pass(string.Format("Pass for Task {0}", job.TaskName));
                    }
                    else
                    {
                        CommandLineHelper.Fail(string.Format("Fail for Task {0}", job.TaskName));
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                HasJobRunning = false;
            }
        }

        public bool DoJob(Job job)
        {
            Console.WriteLine(job.ToString());
            if (!File.Exists(job.TaskExecuteFilePath))
            {
                Console.WriteLine("The execute file path {0} doesn't exist!!", job.TaskExecuteFilePath);
                return false;
            }

            //Running job
            Console.WriteLine("Start to execute job '{0} {1}'", job.TaskExecuteFilePath, job.TaskArgs);
            int ret = ProcessHelper.StartProcess(job.TaskExecuteFilePath, job.TaskArgs);

            //Parse job result
            bool jobReturnValue = VerifyJobReturnValue(job.TaskExecuteFilePath, ret);
            Console.WriteLine("Job result is " + jobReturnValue);

            //Update job status to DB
            IJobService jobChannel = Program.ChannelFactory.CreateChannel();
            if (jobReturnValue)
            {
                jobChannel.UpdateJobStatus(job.JobID, JobStatus.Pass);
            }
            else
            {
                jobChannel.UpdateJobStatus(job.JobID, JobStatus.Fail);
            }
            return jobReturnValue;
        }

        private bool VerifyJobReturnValue(string executeFilePath, int returnedValue)
        {
            if (ConnectionManagerDatabaseServers == null)
            {
                throw new NullReferenceException("The ConnectionManagerDatabaseServers object is NULL!!");
            }

            string ret = ConnectionManagerDatabaseServers[Path.GetFileNameWithoutExtension(executeFilePath)];
            if (string.IsNullOrEmpty(ret))
            {
                throw new NullReferenceException("Fail to get returned value from execute file name in config file!!");
            }

            return int.Parse(ret) == returnedValue;
        }
    }
}
