using System;
using System.Collections.Generic;

namespace TestTechnology.DAL.Models
{
    public partial class Job
    {
        public int JobID { get; set; }
        public string JobName { get; set; }
        public System.DateTime StartTime { get; set; }
        public Nullable<System.DateTime> EndTime { get; set; }
        public string ExecutePath { get; set; }
        public string ExecuteArgs { get; set; }
        public int JobGroupID { get; set; }
        public int JobOrder { get; set; }
        public int JobResult { get; set; }
        public string JobDetailResult { get; set; }
        public virtual JobGroup JobGroup { get; set; }
    }
}
