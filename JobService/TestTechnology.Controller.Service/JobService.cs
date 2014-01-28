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

        public void IsAlive(string clientId)
        {
        }

        public void UpdateJobStatus(int jobId, Shared.DTO.JobStatus updateStatus)
        {
            _jobBiz.UpdateJobStatus(jobId, updateStatus);
        }

        public void UploadJobResult(int jobId, string jobResult)
        {
            _jobBiz.UploadJobResult(jobId, jobResult);
        }

        public void UpdateJobGroupStatus(int jobGroupId, Shared.DTO.JobStatus updateStatus)
        {
            _jobBiz.UpdateJobGroupStatus(jobGroupId, updateStatus);
        }

        public void UpdateJobAssignmentStatus(int assignmentId, Shared.DTO.JobAssignmentStatus updateStatus)
        {
            _jobBiz.UpdateJobAssignmentStatus(assignmentId, updateStatus);
        }

        public void UpdateJobAssignmentResult(int assignmentId, Shared.DTO.JobAssignmentResult updateResult)
        {
            _jobBiz.UpdateJobAssignmentResult(assignmentId, updateResult);
        }

        public bool GetUnTakenTopJobsByClientsId(string clientId, out JobGroup jobGroup, out int assignmentId)
        {
            Console.WriteLine(Thread.CurrentThread.ManagedThreadId);
            Console.WriteLine(clientId+" is calling.");
            Console.WriteLine();

            Console.WriteLine("Getting job from DB");
            return _jobBiz.GetUnTakenTopJobsByClientsID(clientId,out jobGroup, out assignmentId);
        }

        public void UpdateJobEndTime(int jobId, DateTime endTime)
        {
            _jobBiz.UpdateJobEndTime(jobId, endTime);
        }

        public void UpdateClientMachineInfo(string clientID, ClientMachineInfo machineInfo)
        {
            _jobBiz.UpdateClientMachineInfo(clientID, machineInfo);
        }
    }
}
