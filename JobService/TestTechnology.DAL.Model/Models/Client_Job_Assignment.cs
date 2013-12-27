using System;
using System.Collections.Generic;

namespace TestTechnology.DAL.Models
{
    public partial class Client_Job_Assignment
    {
        public int AssignmentID { get; set; }
        public string ClientID { get; set; }
        public int JobGroupID { get; set; }
        public virtual JobAssigment JobAssigment { get; set; }
        public virtual JobGroup JobGroup { get; set; }
    }
}
