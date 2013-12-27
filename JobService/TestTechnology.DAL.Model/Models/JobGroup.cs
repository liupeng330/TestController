using System;
using System.Collections.Generic;

namespace TestTechnology.DAL.Models
{
    public partial class JobGroup
    {
        public JobGroup()
        {
            this.Client_Job_Assignment = new List<Client_Job_Assignment>();
            this.Jobs = new List<Job>();
        }

        public int JobGroupID { get; set; }
        public Nullable<System.DateTime> StartTime { get; set; }
        public Nullable<System.DateTime> EndTime { get; set; }
        public int Status { get; set; }
        public int TaskGroupID { get; set; }
        public virtual ICollection<Client_Job_Assignment> Client_Job_Assignment { get; set; }
        public virtual ICollection<Job> Jobs { get; set; }
        public virtual TaskGroup TaskGroup { get; set; }
    }
}
