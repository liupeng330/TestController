using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Configuration;
using TestTechnology.TestClient;
using TestTechnology.Controller.Interface;

namespace TestClient
{
    class Program
    {
        private static void Main(string[] args)
        {
            string clientId = ConfigurationManager.AppSettings.Get("ClientId");
            if (String.IsNullOrEmpty(clientId))
            {
                throw new Exception("The client id is empty!!");
            }

            InstanceContext callback = new InstanceContext(new LocalJobManager());
            using (
                DuplexChannelFactory<IJobService> channelFactory = new DuplexChannelFactory<IJobService>(callback,
                    "JobService"))
            {
                IJobService jobChannel = channelFactory.CreateChannel();

                while (true)
                {
                    Console.WriteLine("Client Thread ID:" + Thread.CurrentThread.ManagedThreadId);
                    Console.WriteLine(clientId + " is calling JobService");
                    jobChannel.IsAlive(clientId.ToString());
                    Thread.Sleep(5000);
                }
            }
        }

    }
}
