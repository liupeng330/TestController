using System;
using System.Collections.Specialized;
using System.Configuration;
using System.ServiceModel;
using System.Threading;
using TestTechnology.Controller.DTO;
using TestTechnology.Controller.Interface;

namespace TestTechnology.TestClient
{
    class Program
    {
        private readonly static LocalJobManager LocalJobManager = new LocalJobManager();
        private readonly static InstanceContext Callback = new InstanceContext(new LocalJobManager());
        public static readonly DuplexChannelFactory<IJobService> ChannelFactory = new DuplexChannelFactory<IJobService>(Callback, "JobService");

        private static void Main()
        {
            var connectionManagerDatabaseServers = ConfigurationManager.GetSection("JobReturnValues") as NameValueCollection;
            if (connectionManagerDatabaseServers != null)
            {
                Console.WriteLine(connectionManagerDatabaseServers["copy"]);
            }

            try
            {
                string clientId = ConfigurationManager.AppSettings.Get("ClientId");
                if (String.IsNullOrEmpty(clientId))
                {
                    throw new Exception("The client id is empty!!");
                }

                while (true)
                {
                    IJobService jobChannel = ChannelFactory.CreateChannel();
                    if (LocalJobManager.HasJobRunning)
                    {
                        Console.WriteLine("There is job running on this client!!");
                        Thread.Sleep(5000);
                        continue;
                    }
                    Console.WriteLine("Client Thread ID:" + Thread.CurrentThread.ManagedThreadId);
                    Console.WriteLine(clientId + " is calling JobService");
                    JobGroup jobGroup = jobChannel.GetUnTakenTopJobsByClientsID(clientId);

                    LocalJobManager.ExecuteTestJobs(clientId, jobGroup);
                    Thread.Sleep(5000);
                }
            }
            finally
            {
                ChannelFactory.Close();
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
        //        DuplexChannelFactory<IJobService> ChannelFactory = new DuplexChannelFactory<IJobService>(callback,
        //            "JobService"))
        //    {
        //        IJobService jobChannel = ChannelFactory.CreateChannel();

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
