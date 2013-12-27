using System;
using System.Collections.Generic;

namespace TestTechnology.DAL.Models
{
    public partial class TaskGroup
    {
        public TaskGroup()
        {
            this.JobGroups = new List<JobGroup>();
            this.Task_TaskGroup = new List<Task_TaskGroup>();
        }

        public int TaskGroupID { get; set; }
        public string TaskGroupName { get; set; }
        public virtual ICollection<JobGroup> JobGroups { get; set; }
        public virtual ICollection<Task_TaskGroup> Task_TaskGroup { get; set; }
    }
}
