using System;
using System.Collections.Generic;

namespace TestTechnology.DAL.Models
{
    public partial class Client_JobGroup
    {
        public int AssignmentID { get; set; }
        public string ClientID { get; set; }
        public int JobGroupID { get; set; }
        public int Status { get; set; }
        public Nullable<int> Result { get; set; }
        public Nullable<System.DateTime> StartTime { get; set; }
        public Nullable<System.DateTime> EndTime { get; set; }
        public string Owner { get; set; }
        public virtual JobGroup JobGroup { get; set; }
    }
}
