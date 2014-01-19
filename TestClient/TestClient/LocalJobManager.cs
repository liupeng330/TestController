using System;
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

        public bool ExecuteTestJobs(string clientID, JobGroup jobGroup)
        {
            Console.WriteLine("ClientID = " + clientID);
            Console.WriteLine("JobGroupID = " + jobGroup.JobGroupID);
            Console.WriteLine("Thread ID = " + Thread.CurrentThread.ManagedThreadId);

            //Verify if this job group is for current client
            if (!string.Equals(CurrentClientID, clientID, StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine(string.Format("Client ID '{0}' is not for current client, drop it!!", clientID));
                return false;
            }

            //Set job is running on this client, will block others job to assign to this client
            HasJobRunning = true;
            IJobService jobChannel = Program.ChannelFactory.CreateChannel();
            bool jobgroupResult = true;
            try
            {
                var jobArray = jobGroup.Jobs.ToArray();
                for (int i = 0; i < jobArray.Length; i++)
                {
                    bool result = DoJob(jobArray[i]);
                    if (result)
                    {
                        CommandLineHelper.Pass(string.Format("Pass for Task {0}", jobArray[i].TaskName));
                    }
                    else
                    {
                        CommandLineHelper.Fail(string.Format("Fail for Task {0}", jobArray[i].TaskName));
                        CommandLineHelper.Fail("Will ignore other jobs in this jobgroup");

                        //Update other jobs to be NotRun
                        for (int j = i + 1; j < jobArray.Length; j++)
                        {
                            jobChannel.UpdateJobStatus(jobArray[j].JobID, JobStatus.NotRun);
                        }

                        jobgroupResult = false;
                        break;
                    }
                }

                //Update jobgroup status
                jobChannel.UpdateJobGroupStatus(jobGroup.JobGroupID, jobgroupResult ? JobStatus.Pass : JobStatus.Fail);
            }
            catch (Exception ex)
            {
                CommandLineHelper.Fail(ex.Message);
                CommandLineHelper.Fail(ex.StackTrace);
                jobChannel.UpdateJobGroupStatus(jobGroup.JobGroupID, JobStatus.NotRun);
                throw;
            }
            finally
            {
                HasJobRunning = false;
            }
            return jobgroupResult;
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
            string outputResult;
            int ret = ProcessHelper.StartProcess(job.TaskExecuteFilePath, job.TaskArgs, out outputResult);

            //Parse job result
            bool isJobPassed = IsJobPassed(job.TaskExecuteFilePath, ret);
            Console.WriteLine("Job result is " + isJobPassed);

            //Update job status to DB
            IJobService jobChannel = Program.ChannelFactory.CreateChannel();
            jobChannel.UpdateJobStatus(job.JobID, isJobPassed? JobStatus.Pass : JobStatus.Fail);
            jobChannel.UploadJobResult(job.JobID, outputResult);
            return isJobPassed;
        }

        private bool IsJobPassed(string executeFilePath, int returnedValue)
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
