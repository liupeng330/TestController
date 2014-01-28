using System;

namespace MvcApplication.Models
{
    public class TaskAssignmentModel
    {
        public int AssignmentID { get; set; }
        public string ClientID { get; set; }
        public int JobGroupID { get; set; }
        public string Status { get; set; }
        public string Result { get; set; }
        public Nullable<System.DateTime> StartTime { get; set; }
        public Nullable<System.DateTime> EndTime { get; set; }
        public string Owner { get; set; }
    }
}