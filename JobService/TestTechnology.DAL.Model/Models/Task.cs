using System;
using System.Collections.Generic;

namespace TestTechnology.DAL.Models
{
    public partial class Task
    {
        public Task()
        {
            this.Jobs = new List<Job>();
            this.Task_TaskGroup = new List<Task_TaskGroup>();
        }

        public int TaskID { get; set; }
        public string TaskName { get; set; }
        public string TaskExecuteFilePath { get; set; }
        public string TaskArgs { get; set; }
        public virtual ICollection<Job> Jobs { get; set; }
        public virtual ICollection<Task_TaskGroup> Task_TaskGroup { get; set; }
    }
}
