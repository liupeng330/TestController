using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcApplication.Models;

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

        //1. Create jobgroup entry related to taskgroup id in DB
        //2. Create all jobs entries realted to all tasks in DB
        //3. Add entry in to Client_JobGroup table
        public ActionResult Schedule(int taskGroupID, string clientID)
        {

            return View();
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

    }
}
