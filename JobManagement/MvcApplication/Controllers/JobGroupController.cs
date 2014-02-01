using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcApplication.Models;

namespace MvcApplication.Controllers
{
    public class JobGroupController : Controller
    {
        private TestJobDBEntities db = new TestJobDBEntities();
        //
        // GET: /JobGroup/

        public ActionResult Index(int jobGroupID)
        {
            var jobGroup = db.JobGroups.Find(jobGroupID);
            if (jobGroup == null)
            {
                return HttpNotFound();
            }

            return View(jobGroup);
        }
    }
}
