using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcApplication.Models
{
    public class TaskGroupAndAllTasksModel
    {
        public int TaskGroupID { get; set; }
        public string TaskGroupName { get; set; }
        public IEnumerable<Task_TaskGroupIDWithTaskInfoModel> TaskGroupRelatedTasks { get; set; }
        public IEnumerable<Task> AllTasks { get; set; }
    }
}