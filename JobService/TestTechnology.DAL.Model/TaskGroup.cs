//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TestTechnology.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class TaskGroup
    {
        public TaskGroup()
        {
            this.JobGroups = new HashSet<JobGroup>();
            this.Task_TaskGroup = new HashSet<Task_TaskGroup>();
        }
    
        public int TaskGroupID { get; set; }
        public string TaskGroupName { get; set; }
    
        public virtual ICollection<JobGroup> JobGroups { get; set; }
        public virtual ICollection<Task_TaskGroup> Task_TaskGroup { get; set; }
    }
}