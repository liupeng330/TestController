//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MvcApplication.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Task
    {
        public Task()
        {
            this.Jobs = new HashSet<Job>();
            this.Task_TaskGroup = new HashSet<Task_TaskGroup>();
        }
    
        public int TaskID { get; set; }
        public string TaskName { get; set; }
        public string TaskExecuteFilePath { get; set; }
        public string TaskArgs { get; set; }
    
        public virtual ICollection<Job> Jobs { get; set; }
        public virtual ICollection<Task_TaskGroup> Task_TaskGroup { get; set; }
    }
}
