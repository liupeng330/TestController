using System;
using System.Configuration;
using System.ServiceModel;
using System.Threading;
using TestTechnology.Controller.DTO;
using TestTechnology.Controller.Interface;
using TestTechnology.Shared.DTO;

namespace TestTechnology.TestClient
{
    class Program
    {
        private readonly static LocalJobManager LocalJobManager = new LocalJobManager();
        private readonly static InstanceContext Callback = new InstanceContext(new LocalJobManager());
        public static readonly DuplexChannelFactory<IJobService> ChannelFactory = new DuplexChannelFactory<IJobService>(Callback, "JobService");

        private static void Main()
        {
            string clientId = ConfigurationManager.AppSettings.Get("ClientId");

            //Unnecessary codes, clientid will be never equal
            if (String.IsNullOrEmpty(clientId))
            {
                throw new Exception("The client id is empty!!");
            }

            while (true)
            {
                IJobService jobChannel = ChannelFactory.CreateChannel();
                int assignmentId = -1;
                bool jobGroupResult = false;
                try
                {
                    if (LocalJobManager.HasJobRunning)
                    {
                        Console.WriteLine("There is a job running on this client!!");
                        Thread.Sleep(5000);
                        continue;
                    }

                    Console.WriteLine("Client Thread ID:" + Thread.CurrentThread.ManagedThreadId);
                    Console.WriteLine(clientId + " is calling JobService to get untaken jobs");

                    JobGroup jobGroup;
                    if (jobChannel.GetUnTakenTopJobsByClientsId(clientId, out jobGroup, out assignmentId))
                    {
                        Console.WriteLine("Update job assignment id [{0}] status to be running", assignmentId);
                        jobChannel.UpdateJobAssignmentStatus(assignmentId, JobAssignmentStatus.Running);

                        Console.WriteLine(clientId + " got jobgroup ID [{0}]", jobGroup.JobGroupID);
                        Console.WriteLine("Start to execute jobgroup ID [{0}]", jobGroup.JobGroupID);

                        jobGroupResult = LocalJobManager.ExecuteTestJobs(clientId, jobGroup);

                        Console.WriteLine("Finish to execute jobgroup ID [{0}]", jobGroup.JobGroupID);
                        Console.WriteLine();
                    }
                    else
                    {
                        Console.WriteLine("These are no any jobs assigned to this client, will waiting...");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.WriteLine(ex.StackTrace);
                }
                finally
                {
                    if (assignmentId != -1)
                    {
                        jobChannel.UpdateJobAssignmentStatus(assignmentId, JobAssignmentStatus.Completed);
                        Console.WriteLine("Update job assignment id [{0}] status to be completed", assignmentId);

                        jobChannel.UpdateJobAssignmentResult(assignmentId, jobGroupResult?JobAssignmentResult.Pass : JobAssignmentResult.Fail);
                        Console.WriteLine("Update job assignment id [{0}] result to be " + (jobGroupResult ? "passed" : "failed"), assignmentId);
                    }
                    Thread.Sleep(5000);
                }
            }
            ((IDisposable)ChannelFactory).Dispose();
        }
    }
}
