using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace TestTechnology.Controller.DTO
{
    [DataContract]
    public class Job
    {
        [DataMember]
        public int JobID { get; set; }

        [DataMember]
        public Nullable<System.DateTime> StartTime { get; set; }

        [DataMember]
        public Nullable<System.DateTime> EndTime { get; set; }

        [DataMember]
        public int Status { get; set; }

        [DataMember]
        public string ResultInfo { get; set; }

        [DataMember]
        public int TaskID { get; set; }

        [DataMember]
        public string TaskName { get; set; }

        [DataMember]
        public string TaskExecuteFilePath { get; set; }

        [DataMember]
        public string TaskArgs { get; set; }

        public override string ToString()
        {
            return
                string.Format(
                    "JobID: {0}\nStartTime: {1}\nEndTime: {2}\nStatus: {3}\nResultInfo: {4}\nTaskID: {5}\nTaskName: {6}\nTaskExecuteFilePath: {7}\nTaskArgs: {8}\n",
                    this.JobID,
                    this.StartTime,
                    this.EndTime,
                    this.Status,
                    this.ResultInfo,
                    this.TaskID,
                    this.TaskName,
                    this.TaskExecuteFilePath,
                    this.TaskArgs
                    );
        }
    }
}
