using System;
using System.Collections.Generic;

namespace TestTechnology.DAL.Models
{
    public partial class JobAssigment
    {
        public JobAssigment()
        {
            this.Client_Job_Assignment = new List<Client_Job_Assignment>();
        }

        public int AssignmentID { get; set; }
        public int Status { get; set; }
        public Nullable<int> Result { get; set; }
        public Nullable<System.DateTime> StartTime { get; set; }
        public Nullable<System.DateTime> EndTime { get; set; }
        public string Owner { get; set; }
        public virtual ICollection<Client_Job_Assignment> Client_Job_Assignment { get; set; }
    }
}
