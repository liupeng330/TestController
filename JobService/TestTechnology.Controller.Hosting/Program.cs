using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using TestTechnology.Controller.BIZ;
using TestTechnology.Controller.Service;

namespace TestTechnology.Controller.Hosting
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            using (ServiceHost host = new ServiceHost(typeof(JobService)))
            {
                host.Opened += new EventHandler(host_Opened);
                host.Closing += host_Closing;
                host.Open();
                Console.Read();
            }
        }

        private static void host_Opened(object sender, EventArgs e)
        {
            Console.WriteLine("Started...");
            ClientStatusPolling.StartTimer();
        }

        static void host_Closing(object sender, EventArgs e)
        {
            Console.WriteLine("Close...");
            ClientStatusPolling.StopTimer();
        }
    }
}
