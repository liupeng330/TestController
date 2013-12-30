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
    }
}
