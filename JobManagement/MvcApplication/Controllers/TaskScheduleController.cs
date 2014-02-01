using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcApplication.Models;
using TestTechnology.Shared.DTO;

namespace MvcApplication.Controllers
{
    public class TaskScheduleController : Controller
    {
        private TestJobDBEntities db = new TestJobDBEntities();
        //
        // GET: /TaskSchedule/

        public ActionResult Index()
        {
            var clientMachineInfo = db.ClientMachineInfoes.ToList();

            return View();
        }

        public ActionResult Schedule(int taskGroupID)
        {
            var clientMachineScheduleModels = db.ClientMachineInfoes.Select(i => new ClientMachineScheduleModel
            {
                ClientID = i.ClientID,
                MachineName = i.MachineName,
                OS = i.OS,
                SystemType = i.SystemType,
            }).ToList();

            TaskScheduleModel model = new TaskScheduleModel();
            model.TaskGroupID = taskGroupID;
            model.MachineScheduleModels = clientMachineScheduleModels;

            return View(model);
        }


        //1. Create jobgroup entry related to taskgroup id in DB
        //2. Create all jobs entries realted to all tasks in DB
        //3. Add entry in to Client_JobGroup table
        [HttpPost]
        public ActionResult Schedule(TaskScheduleModel model)
        {
            //Verify if client id exist in DB
            if (db.ClientMachineInfoes.Find(model.SelectedClientID) == null)
            {
                return HttpNotFound();
            }

            //1. Create jobgroup entry related to taskgroup id in DB
            JobGroup jobGroup = new JobGroup();
            jobGroup.Status = (int)JobStatus.NotRun;
            jobGroup.TaskGroupID = model.TaskGroupID;

            db.JobGroups.Add(jobGroup);

            //2. Create all jobs entries realted to all tasks in DB
            var tasks =
                db.Task_TaskGroup.Where(i => i.TaskGroupID == model.TaskGroupID).OrderBy(j => j.TaskOrder).Select(k => k.Task);
            foreach (var task in tasks)
            {
                Job job = new Job();
                job.JobGroup = jobGroup;
                job.Task = task;
                job.Status = (int)JobStatus.NotRun;

                db.Jobs.Add(job);
            }

            //3. Add entry in to Client_JobGroup table
            Client_JobGroup assignment = new Client_JobGroup();
            assignment.ClientID = model.SelectedClientID;
            assignment.JobGroup = jobGroup;
            assignment.Status = (int)JobAssignmentStatus.UnTaken;
            //This start time stands for the time when this jobgroup enters into queue
            assignment.StartTime = DateTime.Now;
            assignment.Owner = model.Owner;

            db.Client_JobGroup.Add(assignment);

            db.SaveChanges();

            return RedirectToAction("Index", "TaskAssignment");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

    }
}
