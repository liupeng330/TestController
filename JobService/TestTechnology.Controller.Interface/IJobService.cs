using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using TestTechnology.Controller.DTO;
using TestTechnology.Shared.DTO;

namespace TestTechnology.Controller.Interface
{
    [ServiceContract(Name = "JobService", Namespace = "http://pengliu.me/", CallbackContract = typeof(IJobCallbackService))]
    public interface IJobService
    {
        [OperationContract]
        void IsAlive(string clientId);

        [OperationContract]
        bool GetUnTakenTopJobsByClientsId(string clientId, out JobGroup jobGroup, out int assignmentId);

        [OperationContract]
        void UpdateJobStatus(int jobId, JobStatus updateStatus);

        [OperationContract]
        void UploadJobResult(int jobId, string jobResult);

        [OperationContract]
        void UpdateJobGroupStatus(int jobGroupId, Shared.DTO.JobStatus updateStatus);

        [OperationContract]
        void UpdateJobAssignmentStatus(int assignmentId, JobAssignmentStatus updateStatus);

        [OperationContract]
        void UpdateJobAssignmentResult(int assignmentId, JobAssignmentResult updateResult);

        [OperationContract]
        void UpdateJobEndTime(int jobId, DateTime endTime);

        [OperationContract]
        void UpdateJobStartTime(int jobId, DateTime startTime);

        [OperationContract]
        void UpdateClientMachineInfo(string clientID, ClientMachineInfo machineInfo);

        [OperationContract]
        void UpdateJobGroupEndTime(int jobGroupId, DateTime endTime);

        [OperationContract]
        void UpdateJobGroupStartTime(int jobGroupId, DateTime startTime);
    }
}
