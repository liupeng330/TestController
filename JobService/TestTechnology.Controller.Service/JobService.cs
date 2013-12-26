using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TestTechnology.Controller.DTO;
using TestTechnology.Controller.Interface;

namespace TestTechnology.Controller.Service
{
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Reentrant)]
    public class JobService : IJobService
    {
        public void IsAlive(string ipAddress)
        {
            Console.WriteLine(Thread.CurrentThread.ManagedThreadId);
            Console.WriteLine(ipAddress+" is calling.");
            Console.WriteLine();

            Console.WriteLine("Calling " + ipAddress + " to do the job");
            IJobCallbackService callback = OperationContext.Current.GetCallbackChannel<IJobCallbackService>();
            callback.DoTestJobs(null);
        }
    }
}
