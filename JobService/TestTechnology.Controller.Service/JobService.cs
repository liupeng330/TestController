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
        private readonly IJobBIZ _jobBiz;

        public JobService()
        {
            this._jobBiz = new JobBIZ();
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
            return _jobBiz.GetUnTakenTopJobsByClientsID(clientID);
        }

        public void UpdateJobStatus(int jobID, Shared.DTO.JobStatus updateStatus)
        {
            _jobBiz.UpdateJobStatus(jobID, updateStatus);
        }

        public void UploadJobResult(int jobID, string jobResult)
        {
            _jobBiz.UploadJobResult(jobID, jobResult);
        }

        public void UpdateJobGroupStatus(int jobGroupID, Shared.DTO.JobStatus updateStatus)
        {
            _jobBiz.UpdateJobGroupStatus(jobGroupID, updateStatus);
        }
    }
}
