using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestTechnology.DAL.Models;
using TestTechnology.Shared.DTO;

namespace TestTechnology.DAL
{
    public interface IJobDataRepository
    {
        IEnumerable<Client_JobGroup> GetJobGroupsByStatus(string clientId, JobAssignmentStatus status);
        IEnumerable<TestTechnology.DAL.Models.Task> GetAllTasksByJobGroupID(int jobGroupID);
        IEnumerable<TestTechnology.DAL.Models.Task> GetAllTasksByTaskGroupID(int taskGroupID);
        IEnumerable<Job> GetAllJobs(int jobGroupID);
        TaskGroup GetTaskGroupByJobGroupID(int jobGroupID);
        void UpdateJobAssignmentStatus(int assignmentID, JobAssignmentStatus updateStatus);
    }
}
