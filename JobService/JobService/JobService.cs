using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using JobServiceInterface;

namespace JobService
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
