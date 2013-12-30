using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using TestTechnology.DAL.Models;
using TestTechnology.Shared.DTO;

namespace TestTechnology.DAL
{
    public class JobDataRepository : IJobDataRepository
    {
        public IEnumerable<Client_JobGroup> GetJobGroupsByStatus(string clientId, JobAssignmentStatus status)
        {
            using (var db = new TestJobDBContext())
            {
                var ret = from i
                            in db.Client_JobGroup
                          where i.ClientID.Equals(clientId, StringComparison.OrdinalIgnoreCase) && (int)status == i.Status
                          orderby i.StartTime
                          select i;
                return ret.ToArray();
            }
        }

        public IEnumerable<Job> GetAllJobs(int jobGroupID)
        {
            using (var db = new TestJobDBContext())
            {
                var ret = from i
                    in db.Jobs
                          where i.JobGroupID == jobGroupID
                          select i;

                return ret.ToArray();
            }
        }

        public IEnumerable<TestTechnology.DAL.Models.Task> GetAllTasksByJobGroupID(int jobGroupID)
        {
            using (var db = new TestJobDBContext())
            {
                var taskGroup = (from i
                    in db.JobGroups
                                 where i.JobGroupID == jobGroupID
                                 select i.TaskGroup).SingleOrDefault();

                if (taskGroup != null)
                {
                    return GetAllTasksByTaskGroupID(taskGroup.TaskGroupID);
                }
                else
                {
                    return new List<Models.Task>();
                }
            }
        }

        public IEnumerable<TestTechnology.DAL.Models.Task> GetAllTasksByTaskGroupID(int taskGroupID)
        {
            using (var db = new TestJobDBContext())
            {
                var tasks = from i in db.Task_TaskGroup
                            where i.TaskGroupID == taskGroupID
                            orderby i.TaskOrder
                            select i.Task;
                return tasks.ToArray();
            }
        }

        public TaskGroup GetTaskGroupByJobGroupID(int jobGroupID)
        {
            using (var db = new TestJobDBContext())
            {
                return db.JobGroups.Where(i => i.JobGroupID == jobGroupID).Select(j => j.TaskGroup).SingleOrDefault();
            }
        }

        public void UpdateJobAssignmentStatus(int assignmentID, JobAssignmentStatus updateStatus)
        {
            using (var db = new TestJobDBContext())
            {
                var jobAssignment = db.Client_JobGroup.Where(i => i.AssignmentID == assignmentID).SingleOrDefault();
                if (jobAssignment == null)
                {
                    throw new ArgumentNullException("jobAssignment");
                }

                jobAssignment.Status = (int) updateStatus;

                db.SaveChanges();
            }
        }
    }
}
