using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TestClient;
using TestTechnology.Controller.DTO;
using TestTechnology.Controller.Interface;
using TestTechnology.Shared.DTO;

namespace TestTechnology.TestClient
{
    public class LocalJobManager : IJobCallbackService
    {
        private static readonly string currentClientID = ConfigurationManager.AppSettings.Get("ClientId");
        private static readonly Queue<Job> jobQueue = new Queue<Job>();

        public bool HasJobRunning { get; set; }

        public void DoTestJobs(string clientID, JobGroup jobGroup)
        {

        }

        public void ExecuteTestJobs(string clientID, JobGroup jobGroup)
        {
            Console.WriteLine("ClientID=" + clientID);
            Console.WriteLine("jobGroupID=" + jobGroup.JobGroupID);
            Console.WriteLine("Get job list, " + Thread.CurrentThread.ManagedThreadId);

            //Verify if this job group is for current client
            if (!string.Equals(currentClientID, clientID, StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine(string.Format("Client ID '{0}' is not for current client, drop it!!"));
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
            IJobService jobChannel = Program.channelFactory.CreateChannel();
            jobChannel.UpdateJobStatus(job.JobID, JobStatus.Pass);
            return true;
        }
    }
}
