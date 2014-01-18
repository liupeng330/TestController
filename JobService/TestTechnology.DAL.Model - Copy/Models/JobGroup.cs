using System;
using System.Collections.Generic;

namespace TestTechnology.DAL.Models
{
    public partial class JobGroup
    {
        public JobGroup()
        {
            this.Client_JobGroup = new List<Client_JobGroup>();
            this.Jobs = new List<Job>();
        }

        public int JobGroupID { get; set; }
        public Nullable<System.DateTime> StartTime { get; set; }
        public Nullable<System.DateTime> EndTime { get; set; }
        public int Status { get; set; }
        public int TaskGroupID { get; set; }
        public virtual ICollection<Client_JobGroup> Client_JobGroup { get; set; }
        public virtual ICollection<Job> Jobs { get; set; }
        public virtual TaskGroup TaskGroup { get; set; }
    }
}
