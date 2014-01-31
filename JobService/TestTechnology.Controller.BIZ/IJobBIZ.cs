using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestTechnology.Controller.DTO;
using TestTechnology.Shared.DTO;

namespace TestTechnology.Controller.BIZ
{
    public interface IJobBIZ
    {
        bool GetUnTakenTopJobsByClientsID(string clientId, out JobGroup jobGroup, out int assignmentId);
        void UpdateJobStatus(int jobId, JobStatus updateStatus);
        void UpdateJobStartTime(int jobId, DateTime startTime);
        void UpdateJobEndTime(int jobId, DateTime endTime);
        void UploadJobResult(int jobId, string jobResult);
        void UpdateJobGroupStatus(int jobgroupId, JobStatus updateStatus);
        void UpdateJobAssignmentStatus(int assignmentId, JobAssignmentStatus updateStatus);
        void UpdateJobAssignmentResult(int assignmentId, JobAssignmentResult updateResult);
        void UpdateClientMachineInfo(string clientID, ClientMachineInfo machineInfo);
        void UpdateJobGroupStartTime(int jobGroupId, DateTime startTime);
        void UpdateJobGroupEndTime(int jobGroupId, DateTime endTime);
    }
}
