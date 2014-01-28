using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestTechnology.Shared.DTO;

namespace TestTechnology.DAL
{
    public interface IJobDataRepository
    {
        IEnumerable<Client_JobGroup> GetJobGroupsByStatus(string clientId, JobAssignmentStatus status);
        IEnumerable<TestTechnology.DAL.Task> GetAllTasksByJobGroupID(int jobGroupID);
        IEnumerable<TestTechnology.DAL.Task> GetAllTasksByTaskGroupID(int taskGroupID);
        IEnumerable<Job> GetAllJobs(int jobGroupID);
        JobGroup GetJobGroup(int jobGroupID);
        TaskGroup GetTaskGroupByJobGroupID(int jobGroupID);
        void UpdateJobAssignmentStatus(int assignmentID, JobAssignmentStatus updateStatus);
        void UpdateJobAssignmentResult(int assignmentID, JobAssignmentResult updateResult);
        void UpdateJobStatus(int jobID, JobStatus updateStatus);
        void UpdateJobEndTime(int jobID, DateTime endTime);
        void UpdateJobGroupStatus(int jobGroupID, JobStatus updateStatus);
        void UploadJobResult(int jobID, string jobResult);
        void UpdateClientMachineInfo(string clientID, ClientMachineInfo machineInfo);
    }
}
