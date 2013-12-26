using System;
using System.Collections.Generic;

namespace TestTechnology.DAL.Models
{
    public partial class JobAssignment
    {
        public string ClientID { get; set; }
        public int JobGroupID { get; set; }
        public System.DateTime JobAssignmentDateTime { get; set; }
        public virtual JobGroup JobGroup { get; set; }
    }
}
