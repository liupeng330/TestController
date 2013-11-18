using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using JobServiceInterface;

namespace TestClient
{
    class Program
    {
        private static readonly Guid clientId = Guid.NewGuid();

        private static void Main(string[] args)
        {
            InstanceContext callback = new InstanceContext(new LocalJobManager());
            using (
                DuplexChannelFactory<IJobService> channelFactory = new DuplexChannelFactory<IJobService>(callback,
                    "JobService"))
            {
                IJobService jobChannel = channelFactory.CreateChannel();

                while (true)
                {
                    Console.WriteLine(clientId + " is calling JobService");
                    jobChannel.IsAlive(clientId.ToString());
                    Thread.Sleep(5000);
                }
            }
        }

    }
}
