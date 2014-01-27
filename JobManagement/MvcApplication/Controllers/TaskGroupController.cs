using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using MvcApplication.Models;

namespace MvcApplication.Controllers
{
    public class TaskGroupController : Controller
    {
        private TestJobDBEntities db = new TestJobDBEntities();

        //
        // GET: /TaskGroup/

        public ActionResult Index()
        {
            return View(db.TaskGroups.ToList());
        }

        //
        // GET: /TaskGroup/Details/5

        public ActionResult Details(int id = 0)
        {
            TaskGroup taskgroup = db.TaskGroups.Find(id);
            if (taskgroup == null)
            {
                return HttpNotFound();
            }
            return View(taskgroup);
        }

        //
        // GET: /TaskGroup/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /TaskGroup/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TaskGroup taskgroup)
        {
            if (ModelState.IsValid)
            {
                db.TaskGroups.Add(taskgroup);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(taskgroup);
        }

        //
        // GET: /TaskGroup/Edit/5

        public ActionResult Edit(int id = 0)
        {
            TaskGroup taskgroup = db.TaskGroups.Find(id);
            if (taskgroup == null)
            {
                return HttpNotFound();
            }

            //Get all related tasks in this taskgroup
            var tasks = db.Task_TaskGroup.Where(i => i.TaskGroupID == id).OrderBy(i => i.TaskOrder).Select(i => i.Task);
            TaskGroupAndAllTasksModel taskGroupAndAllTasksModel = new TaskGroupAndAllTasksModel
            {
                TaskGroupID =  id,
                TaskGroupName =  taskgroup.TaskGroupName,
                TaskGroupRelatedTasks = tasks,
                AllTasks = db.Tasks.ToList(),
            };

            return View(taskGroupAndAllTasksModel);
        }

        //
        // POST: /TaskGroup/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(TaskGroup taskgroup)
        {
            if (ModelState.IsValid)
            {
                db.Entry(taskgroup).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(taskgroup);
        }

        //
        // GET: /TaskGroup/Delete/5

        public ActionResult Delete(int id = 0)
        {
            TaskGroup taskgroup = db.TaskGroups.Find(id);
            if (taskgroup == null)
            {
                return HttpNotFound();
            }
            return View(taskgroup);
        }

        //
        // POST: /TaskGroup/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TaskGroup taskgroup = db.TaskGroups.Find(id);
            db.TaskGroups.Remove(taskgroup);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Add(int id, int groupId)
        {
            var orderByTaskOrder =
                db.Task_TaskGroup.Where(i => i.TaskGroupID == groupId).OrderByDescending(i => i.TaskOrder);
            int order;
            if(orderByTaskOrder.Any())
            {
                order = orderByTaskOrder.ToArray()[0].TaskOrder + 1;
            }
            else
            {
                order = 1;
            }

            var task_taskGroup = new Task_TaskGroup
            {
                TaskGroupID = groupId,
                TaskID = id,
                TaskOrder = order,
            };

            db.Task_TaskGroup.Add(task_taskGroup);
            db.SaveChanges();

            //refresh Edit page for this added task into taskgroup
            return RedirectToAction("Edit", new {id = groupId});
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}