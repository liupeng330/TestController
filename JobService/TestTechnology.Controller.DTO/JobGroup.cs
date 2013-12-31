using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace TestTechnology.Controller.DTO
{
    [DataContract]
    public class JobGroup
    {
        public JobGroup()
        {
            Jobs = new List<Job>();
        }

        [DataMember]
        public int JobGroupID { get; set; }

        [DataMember]
        public Nullable<System.DateTime> StartTime { get; set; }

        [DataMember]
        public Nullable<System.DateTime> EndTime { get; set; }

        [DataMember]
        public int Status { get; set; }

        [DataMember]
        public int TaskGroupID { get; set; }

        [DataMember]
        public string TaskGroupName { get; set; }

        [DataMember]
        public IEnumerable<Job> Jobs { get; set; }

        public override string ToString()
        {
            return string.Format(
                "JobGroupID: {0}\nStartTime: {1}\nEndTime: {2}\nStatus: {3}\nTaskGroupID: {4}\nTaskGroupName: {5}",
                this.JobGroupID,
                this.StartTime,
                this.EndTime,
                this.Status,
                this.TaskGroupID,
                this.TaskGroupName
                );
        }
    }
}
