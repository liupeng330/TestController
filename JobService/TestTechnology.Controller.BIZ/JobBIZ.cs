using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Proxies;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using AutomapperHelper;
using TestTechnology.Controller.DTO;
using TestTechnology.DAL;
using TestTechnology.Shared.DTO;

namespace TestTechnology.Controller.BIZ
{
    public class JobBIZ : IJobBIZ
    {
        private readonly IJobDataRepository jobDataRepository;

        public JobBIZ()
        {
            this.jobDataRepository = new JobDataRepository();
        }

        //Get untaken and top jobs, and set it to running
        public IEnumerable<Job> GetUnTakenTopJobsByClientsID(string clientId)
        {
            //Get running jobs in DB
            if (jobDataRepository.GetJobGroupsByStatus(clientId, JobAssignmentStatus.Running).Any())
            {
                //There are jobs running in client, so do not return any jobs to client
                return new List<Job>();
            }

            //If there is no running jobs in this client, start to query untaken job
            var client_job = jobDataRepository.GetJobGroupsByStatus(clientId, JobAssignmentStatus.UnTaken).FirstOrDefault();
            List<Job> jobs = new List<Job>();
            try
            {
                if (client_job == null)
                {
                    return jobs;
                }

                Mapper.CreateMap<TestTechnology.DAL.Models.Job, Job>();
                Mapper.CreateMap<TestTechnology.DAL.Models.Task, Job>();

                var jobsInDB = jobDataRepository.GetAllJobs(client_job.JobGroupID);
                var tasksInDB = jobDataRepository.GetAllTasksByJobGroupID(client_job.JobGroupID);

                foreach (var jobInDB in jobsInDB)
                {
                    var taskInDB = (from i in tasksInDB where i.TaskID == jobInDB.TaskID select i).SingleOrDefault();
                    var jobForDTO = EntityMapper.Map<Job>(jobInDB, taskInDB);
                    jobs.Add(jobForDTO);
                }
                //Set it to running
                jobDataRepository.UpdateJobAssignmentStatus(client_job.AssignmentID, JobAssignmentStatus.Running);
            }
            catch (Exception)
            {
                //If exception happened, set it back to untaken
                jobDataRepository.UpdateJobAssignmentStatus(client_job.AssignmentID, JobAssignmentStatus.UnTaken);
                throw;
            }
            return jobs;
        }
    }
}
