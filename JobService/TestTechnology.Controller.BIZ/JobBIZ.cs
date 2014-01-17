using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Proxies;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using AutoMapper;
using AutomapperHelper;
using TestTechnology.Controller.DTO;
using TestTechnology.DAL;
using TestTechnology.Shared.DTO;

namespace TestTechnology.Controller.BIZ
{
    //TODO: Adding log for every methods
    public class JobBIZ : IJobBIZ
    {
        private readonly IJobDataRepository _jobDataRepository;

        public JobBIZ()
        {
            this._jobDataRepository = new JobDataRepository();
        }

        //Get untaken and top jobs, and set it to running, if there is a job that is running on that client, empty array will be returned
        public JobGroup GetUnTakenTopJobsByClientsID(string clientId)
        {
            //Get running jobs in DB
            if (_jobDataRepository.GetJobGroupsByStatus(clientId, JobAssignmentStatus.Running).Any())
            {
                //There are jobs running in client, so do not return any jobs to client
                return new JobGroup();
            }

            //If there is no running jobs in this client, start to query untaken job
            var client_job = _jobDataRepository.GetJobGroupsByStatus(clientId, JobAssignmentStatus.UnTaken).FirstOrDefault();
            if (client_job == null)
            {
                return new JobGroup();
            }

            //Get jobgroup from DB
            Mapper.CreateMap<TestTechnology.DAL.Models.JobGroup, JobGroup>().ForMember(i => i.Jobs, o => o.Ignore());
            Mapper.CreateMap<TestTechnology.DAL.Models.TaskGroup, JobGroup>();

            var jobGroupInDB = _jobDataRepository.GetJobGroup(client_job.JobGroupID);
            var taskGroupInDB = _jobDataRepository.GetTaskGroupByJobGroupID(client_job.JobGroupID);

            JobGroup jobGroup = EntityMapper.Map<JobGroup>(jobGroupInDB, taskGroupInDB);

            //Get all realted jobs from DB
            List<Job> jobs = new List<Job>();
            try
            {
                Mapper.CreateMap<TestTechnology.DAL.Models.Job, Job>();
                Mapper.CreateMap<TestTechnology.DAL.Models.Task, Job>();

                var jobsInDB = _jobDataRepository.GetAllJobs(client_job.JobGroupID);
                var tasksInDB = _jobDataRepository.GetAllTasksByJobGroupID(client_job.JobGroupID);

                foreach (var jobInDB in jobsInDB)
                {
                    var taskInDB = (from i in tasksInDB where i.TaskID == jobInDB.TaskID select i).SingleOrDefault();
                    var jobForDTO = EntityMapper.Map<Job>(jobInDB, taskInDB);
                    jobs.Add(jobForDTO);
                }

                //Client will be responseible to set assigment status to running or completed
                ////Set it to running
                //jobDataRepository.UpdateJobAssignmentStatus(client_job.AssignmentID, JobAssignmentStatus.Running);
            }
            catch (Exception)
            {
                ////If exception happened, set it back to untaken
                //jobDataRepository.UpdateJobAssignmentStatus(client_job.AssignmentID, JobAssignmentStatus.UnTaken);
                throw;
            }

            jobGroup.Jobs = jobs;
            return jobGroup;
        }

        public void UpdateJobStatus(int jobID, JobStatus status)
        {
            _jobDataRepository.UpdateJobStatus(jobID, status);
        }

        public void UploadJobResult(int jobID, string jobResult)
        {
            _jobDataRepository.UploadJobResult(jobID, jobResult);
        }

        public void UpdateJobGroupStatus(int jobgroupID, JobStatus updateStatus)
        {
            _jobDataRepository.UpdateJobGroupStatus(jobgroupID, updateStatus);
        }
    }
}
