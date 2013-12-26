using System;
using System.Collections.Generic;

namespace TestTechnology.DAL.Models
{
    public partial class JobGroup
    {
        public JobGroup()
        {
            this.Jobs = new List<Job>();
            this.JobAssignments = new List<JobAssignment>();
        }

        public int JobGroupId { get; set; }
        public string JobGroupName { get; set; }
        public Nullable<int> JobGroupResult { get; set; }
        public virtual ICollection<Job> Jobs { get; set; }
        public virtual ICollection<JobAssignment> JobAssignments { get; set; }
    }
}
