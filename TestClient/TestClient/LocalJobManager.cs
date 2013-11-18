using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using JobServiceInterface;

namespace TestClient
{
    public class LocalJobManager : IJobCallbackService
    {
        public void DoTestJobs(IEnumerable<Job> jobs)
        {
            Console.WriteLine("Get job list, " + Thread.CurrentThread.ManagedThreadId);
        }
    }
}
