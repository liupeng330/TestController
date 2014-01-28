using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcApplication.Models;
using Utils;
using WebGrease.Css.Extensions;

namespace MvcApplication.Controllers
{
    public class TaskAssignmentController : Controller
    {
        private TestJobDBEntities db = new TestJobDBEntities();
        //
        // GET: /TaskAssignment/

        public ActionResult Index()
        {
            List<TaskAssignmentModel> taskAssignmentModes = new List<TaskAssignmentModel>();

            foreach (var assignment in db.Client_JobGroup.ToList())
            {
                TaskAssignmentModel model = new TaskAssignmentModel();
                model.AssignmentID = assignment.AssignmentID;
                model.ClientID = assignment.ClientID;
                model.EndTime = assignment.EndTime;
                model.JobGroupID = assignment.JobGroupID;
                model.Owner = assignment.Owner;
                model.Result = assignment.Result.HasValue
                    ? EnumMapping.ToAssigmentResult(assignment.Result.Value)
                    : string.Empty;
                model.StartTime = assignment.StartTime;
                model.Status = EnumMapping.ToAssignmentStatus(assignment.Status);

                taskAssignmentModes.Add(model);
            }

            return View(taskAssignmentModes);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}
