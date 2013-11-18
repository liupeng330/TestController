using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Hosting
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            using (ServiceHost host = new ServiceHost(typeof (JobService.JobService)))
            {
                host.Opened += new EventHandler(host_Opened);
                host.Open();
                Console.Read();
            }
        }

        private static void host_Opened(object sender, EventArgs e)
        {
            Console.WriteLine("Started...");
        }
    }
}
