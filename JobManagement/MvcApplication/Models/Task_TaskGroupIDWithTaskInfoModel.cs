using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcApplication.Models
{
    public class Task_TaskGroupIDWithTaskInfoModel
    {
        public int Task_TaskGroupID { get; set; }
        public Task RelatedTask { get; set; }
    }
}