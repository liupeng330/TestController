using System;
using System.Collections.Generic;

namespace TestTechnology.DAL.Models
{
    public partial class Job
    {
        public int JobID { get; set; }
        public Nullable<System.DateTime> StartTime { get; set; }
        public Nullable<System.DateTime> EndTime { get; set; }
        public int Status { get; set; }
        public string ResultInfo { get; set; }
        public int TaskID { get; set; }
        public int JobGroupID { get; set; }
        public virtual JobGroup JobGroup { get; set; }
        public virtual Task Task { get; set; }
    }
}
