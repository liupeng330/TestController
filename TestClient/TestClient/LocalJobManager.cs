using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TestTechnology.Controller.DTO;
using TestTechnology.Controller.Interface;

namespace TestTechnology.TestClient
{
    public class LocalJobManager : IJobCallbackService
    {
        public void DoTestJobs(IEnumerable<Job> jobs)
        {
            Console.WriteLine("Get job list, " + Thread.CurrentThread.ManagedThreadId);
        }
    }
}
