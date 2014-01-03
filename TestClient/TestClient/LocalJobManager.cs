using System;
using System.Collections.Generic;
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

namespace TestTechnology.TestClient
{
    public class LocalJobManager : IJobCallbackService
    {
        private static readonly string CurrentClientID = ConfigurationManager.AppSettings.Get("ClientId");
        private static readonly Queue<Job> JobQueue = new Queue<Job>();

        public bool HasJobRunning { get; set; }

        public void DoTestJobs(string clientID, JobGroup jobGroup)
        {

        }

        public void ExecuteTestJobs(string clientID, JobGroup jobGroup)
        {
            Console.WriteLine("ClientID=" + clientID);
            Console.WriteLine("JobGroupID=" + jobGroup.JobGroupID);
            Console.WriteLine("Get job list, " + Thread.CurrentThread.ManagedThreadId);

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
                    DoJob(job);
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

            IJobService jobChannel = Program.ChannelFactory.CreateChannel();
            jobChannel.UpdateJobStatus(job.JobID, JobStatus.Pass);
            return true;
        }
    }
}
