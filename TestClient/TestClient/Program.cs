using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Configuration;
using TestTechnology.Controller.DTO;
using TestTechnology.Shared.DTO;
using TestTechnology.TestClient;
using TestTechnology.Controller.Interface;

namespace TestClient
{
    class Program
    {
        private readonly static LocalJobManager localJobManager = new LocalJobManager();
        private readonly static InstanceContext callback = new InstanceContext(new LocalJobManager());
        public static readonly DuplexChannelFactory<IJobService> channelFactory = new DuplexChannelFactory<IJobService>(callback, "JobService");

        private static void Main(string[] args)
        {
            try
            {
                string clientId = ConfigurationManager.AppSettings.Get("ClientId");
                if (String.IsNullOrEmpty(clientId))
                {
                    throw new Exception("The client id is empty!!");
                }

                while (true)
                {
                    JobGroup jobGroup = new JobGroup();
                    InstanceContext callback = new InstanceContext(new LocalJobManager());
                    IJobService jobChannel = channelFactory.CreateChannel();
                    if (localJobManager.HasJobRunning)
                    {
                        Console.WriteLine("There is job running on this client!!");
                        Thread.Sleep(5000);
                        continue;
                    }
                    Console.WriteLine("Client Thread ID:" + Thread.CurrentThread.ManagedThreadId);
                    Console.WriteLine(clientId + " is calling JobService");
                    jobGroup = jobChannel.GetUnTakenTopJobsByClientsID(clientId);

                    localJobManager.ExecuteTestJobs(clientId, jobGroup);
                    Thread.Sleep(5000);
                }
            }
            finally
            {
                channelFactory.Close();
            }
        }

        //private static void Main(string[] args)
        //{
        //    string clientId = ConfigurationManager.AppSettings.Get("ClientId");
        //    if (String.IsNullOrEmpty(clientId))
        //    {
        //        throw new Exception("The client id is empty!!");
        //    }

        //    InstanceContext callback = new InstanceContext(new LocalJobManager());
        //    using (
        //        DuplexChannelFactory<IJobService> channelFactory = new DuplexChannelFactory<IJobService>(callback,
        //            "JobService"))
        //    {
        //        IJobService jobChannel = channelFactory.CreateChannel();

        //        while (true)
        //        {
        //            if (localJobManager.HasJobRunning)
        //            {
        //                Console.WriteLine("There is job running on this client!!");
        //                Thread.Sleep(5000);
        //                continue;
        //            }
        //            Console.WriteLine("Client Thread ID:" + Thread.CurrentThread.ManagedThreadId);
        //            Console.WriteLine(clientId + " is calling JobService");
        //            var jobGroup = jobChannel.GetUnTakenTopJobsByClientsID(clientId);

        //            localJobManager.ExecuteTestJobs(clientId, jobGroup);

        //            jobChannel.UpdateJobStatus(1, JobStatus.Pass);
        //        }
        //    }
        //}
    }
}
