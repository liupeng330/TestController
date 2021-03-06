﻿using System;
using System.Collections.Generic;
using System.Linq;
using TestTechnology.Shared.DTO;

namespace TestTechnology.DAL
{
    public class JobDataRepository : IJobDataRepository
    {
        public IEnumerable<Client_JobGroup> GetJobGroupsByStatus(string clientId, JobAssignmentStatus status)
        {
            using (var db = new TestJobDBEntities())
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
            using (var db = new TestJobDBEntities())
            {
                var ret = from i
                    in db.Jobs
                          where i.JobGroupID == jobGroupID
                          select i;

                return ret.ToArray();
            }
        }

        public JobGroup GetJobGroup(int jobGroupID)
        {
            using (var db = new TestJobDBEntities())
            {
                return db.JobGroups.SingleOrDefault(i => i.JobGroupID == jobGroupID);
            }
        }

        public IEnumerable<TestTechnology.DAL.Task> GetAllTasksByJobGroupID(int jobGroupID)
        {
            using (var db = new TestJobDBEntities())
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
                    return new List<Task>();
                }
            }
        }

        public IEnumerable<TestTechnology.DAL.Task> GetAllTasksByTaskGroupID(int taskGroupID)
        {
            using (var db = new TestJobDBEntities())
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
            using (var db = new TestJobDBEntities())
            {
                return db.JobGroups.Where(i => i.JobGroupID == jobGroupID).Select(j => j.TaskGroup).SingleOrDefault();
            }
        }

        public void UpdateJobAssignmentStatus(int assignmentID, JobAssignmentStatus updateStatus)
        {
            using (var db = new TestJobDBEntities())
            {
                var jobAssignment = db.Client_JobGroup.SingleOrDefault(i => i.AssignmentID == assignmentID);
                if (jobAssignment == null)
                {
                    throw new ArgumentNullException("jobAssignment");
                }
                jobAssignment.Status = (int) updateStatus;

                db.SaveChanges();
            }
        }

        public void UpdateJobAssignmentResult(int assignmentID, JobAssignmentResult updateResult)
        {
            using (var db = new TestJobDBEntities())
            {
                var jobAssignment = db.Client_JobGroup.SingleOrDefault(i => i.AssignmentID == assignmentID);
                if (jobAssignment == null)
                {
                    throw new ArgumentNullException("jobAssignment");
                }
                jobAssignment.Result = (int) updateResult;

                db.SaveChanges();
            }
            
        }

        public void UpdateJobStatus(int jobID, JobStatus updateStatus)
        {
            using (var db = new TestJobDBEntities())
            {
                var job = db.Jobs.SingleOrDefault(i => i.JobID == jobID);
                if (job == null)
                {
                    throw new ArgumentNullException("job");
                }

                job.Status = (int) updateStatus;
                db.SaveChanges();
            }
        }

        public void UpdateJobGroupStatus(int jobGroupID, JobStatus updateStatus)
        {
            using (var db = new TestJobDBEntities())
            {
                var jobGroup = db.JobGroups.SingleOrDefault(i => i.JobGroupID == jobGroupID);
                if (jobGroup == null)
                {
                    throw new ArgumentNullException("jobGroup");
                }

                jobGroup.Status = (int) updateStatus;
                db.SaveChanges();
            }
        }

        public void UploadJobResult(int jobID, string jobResult)
        {
            using (var db = new TestJobDBEntities())
            {
                var job = db.Jobs.SingleOrDefault(i => i.JobID == jobID);
                if (job == null)
                {
                    throw new ArgumentNullException("job");
                }
                job.ResultInfo = jobResult;
                db.SaveChanges();
            }
        }

        public void UpdateJobEndTime(int jobID, DateTime endTime)
        {
            using (var db = new TestJobDBEntities())
            {
                var job = db.Jobs.SingleOrDefault(i => i.JobID == jobID);
                if (job == null)
                {
                    throw new ArgumentNullException("job");
                }

                job.EndTime = endTime;
                db.SaveChanges();
            }
        }

        public void UpdateClientMachineInfo(string clientID, ClientMachineInfo machineInfo)
        {
            using (var db = new TestJobDBEntities())
            {
                var machineInfoFromDB =
                    db.ClientMachineInfoes.SingleOrDefault(
                        i => i.ClientID.Equals(clientID, StringComparison.OrdinalIgnoreCase));
                if (machineInfoFromDB == null)
                {
                    //Insert machin info 
                    machineInfo.ClientID = clientID;
                    db.ClientMachineInfoes.Add(machineInfo);
                    db.SaveChanges();
                    return;
                }

                machineInfoFromDB.OS = machineInfo.OS;
                machineInfoFromDB.SystemType = machineInfo.SystemType;
                machineInfoFromDB.MachineName = machineInfo.MachineName;
                db.SaveChanges();
            }
        }


        public void UpdateJobStartTime(int jobID, DateTime startTime)
        {
            using (var db = new TestJobDBEntities())
            {
                var job = db.Jobs.SingleOrDefault(i => i.JobID == jobID);
                if (job == null)
                {
                    throw new ArgumentNullException("job");
                }

                job.StartTime = startTime;
                db.SaveChanges();
            }
        }

        public void UpdateJobGroupStartTime(int jobGroupID, DateTime startTime)
        {
            using (var db = new TestJobDBEntities())
            {
                var job = db.JobGroups.SingleOrDefault(i => i.JobGroupID == jobGroupID);
                if (job == null)
                {
                    throw new ArgumentNullException("jobgroup");
                }

                job.StartTime = startTime;
                db.SaveChanges();
            }
        }

        public void UpdateJobGroupEndTime(int jobGroupID, DateTime endTime)
        {
            using (var db = new TestJobDBEntities())
            {
                var job = db.JobGroups.SingleOrDefault(i => i.JobGroupID == jobGroupID);
                if (job == null)
                {
                    throw new ArgumentNullException("jobgroup");
                }

                job.EndTime = endTime;
                db.SaveChanges();
            }
        }
    }
}
