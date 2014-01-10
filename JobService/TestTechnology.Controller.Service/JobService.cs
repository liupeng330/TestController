using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using TestTechnology.Controller.BIZ;
using TestTechnology.Controller.DTO;
using TestTechnology.Controller.Interface;

namespace TestTechnology.Controller.Service
{
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Reentrant)]
    public class JobService : IJobService
    {
        private readonly IJobBIZ jobBiz;

        public JobService()
        {
            this.jobBiz = new JobBIZ();
        }

        public void IsAlive(string clientID)
        {
        }

        public JobGroup GetUnTakenTopJobsByClientsID(string clientID)
        {
            Console.WriteLine(Thread.CurrentThread.ManagedThreadId);
            Console.WriteLine(clientID+" is calling.");
            Console.WriteLine();

            Console.WriteLine("Getting job from DB");
            return jobBiz.GetUnTakenTopJobsByClientsID(clientID);
        }

        public void UpdateJobStatus(int jobID, Shared.DTO.JobStatus updateStatus)
        {
            jobBiz.UpdateJobStatus(jobID, updateStatus);
        }

        public void UploadJobResult(int jobID, string jobResult)
        {
            throw new NotImplementedException();
        }
    }
}
