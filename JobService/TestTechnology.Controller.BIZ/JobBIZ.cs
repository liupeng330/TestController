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
        public bool GetUnTakenTopJobsByClientsID(string clientId, out TestTechnology.Controller.DTO.JobGroup jobGroup, out int assignmentId)
        {
            //Get running jobs in DB
            if (_jobDataRepository.GetJobGroupsByStatus(clientId, JobAssignmentStatus.Running).Any())
            {
                //There are jobs running in client, so do not return any jobs to client
                assignmentId = -1;
                jobGroup = new DTO.JobGroup();
                return false;
            }

            //If there is no running jobs in this client, start to query untaken job
            var client_job = _jobDataRepository.GetJobGroupsByStatus(clientId, JobAssignmentStatus.UnTaken).FirstOrDefault();
            if (client_job == null)
            {
                assignmentId = -1;
                jobGroup = new DTO.JobGroup();
                return false;

            }
            assignmentId = client_job.AssignmentID;

            //Get jobgroup from DB
            Mapper.CreateMap<TestTechnology.DAL.JobGroup, DTO.JobGroup>().ForMember(i => i.Jobs, o => o.Ignore());
            Mapper.CreateMap<TestTechnology.DAL.TaskGroup, DTO.JobGroup>();

            var jobGroupInDB = _jobDataRepository.GetJobGroup(client_job.JobGroupID);
            var taskGroupInDB = _jobDataRepository.GetTaskGroupByJobGroupID(client_job.JobGroupID);

            jobGroup = EntityMapper.Map<DTO.JobGroup>(jobGroupInDB, taskGroupInDB);

            //Get all realted jobs from DB
            List<DTO.Job> jobs = new List<DTO.Job>();
            try
            {
                Mapper.CreateMap<TestTechnology.DAL.Job, DTO.Job>();
                Mapper.CreateMap<TestTechnology.DAL.Task, DTO.Job>();

                var jobsInDB = _jobDataRepository.GetAllJobs(client_job.JobGroupID);
                var tasksInDB = _jobDataRepository.GetAllTasksByJobGroupID(client_job.JobGroupID);

                foreach (var jobInDB in jobsInDB)
                {
                    var taskInDB = (from i in tasksInDB where i.TaskID == jobInDB.TaskID select i).SingleOrDefault();
                    var jobForDTO = EntityMapper.Map<DTO.Job>(jobInDB, taskInDB);
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
            return true;
        }

        public void UpdateJobStatus(int jobId, JobStatus status)
        {
            _jobDataRepository.UpdateJobStatus(jobId, status);
        }

        public void UploadJobResult(int jobId, string jobResult)
        {
            _jobDataRepository.UploadJobResult(jobId, jobResult);
        }

        public void UpdateJobGroupStatus(int jobgroupId, JobStatus updateStatus)
        {
            _jobDataRepository.UpdateJobGroupStatus(jobgroupId, updateStatus);
        }

        public void UpdateJobAssignmentStatus(int assignmentId, JobAssignmentStatus updateStatus)
        {
            _jobDataRepository.UpdateJobAssignmentStatus(assignmentId, updateStatus);
        }

        public void UpdateJobAssignmentResult(int assignmentId, JobAssignmentResult updateResult)
        {
            _jobDataRepository.UpdateJobAssignmentResult(assignmentId, updateResult);
        }

        public void UpdateJobEndTime(int jobId, DateTime endTime)
        {
            _jobDataRepository.UpdateJobEndTime(jobId, endTime);
        }


        public void UpdateClientMachineInfo(string clientID, DTO.ClientMachineInfo machineInfo)
        {
            Mapper.CreateMap<DTO.ClientMachineInfo, DAL.ClientMachineInfo>();
            var machinInfoDAL = Mapper.Map<DAL.ClientMachineInfo>(machineInfo);
            _jobDataRepository.UpdateClientMachineInfo(clientID, machinInfoDAL);
        }

        public void UpdateJobStartTime(int jobId, DateTime startTime)
        {
            _jobDataRepository.UpdateJobStartTime(jobId, startTime);
        }

        public void UpdateJobGroupStartTime(int jobGroupId, DateTime startTime)
        {
            _jobDataRepository.UpdateJobGroupStartTime(jobGroupId, startTime);
        }

        public void UpdateJobGroupEndTime(int jobGroupId, DateTime endTime)
        {
            _jobDataRepository.UpdateJobGroupEndTime(jobGroupId, endTime);
        }
    }
}
