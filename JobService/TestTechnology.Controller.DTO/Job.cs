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
        public Guid ID { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string ExecutePath { get; set; }

        [DataMember]
        public string ExecuteArguments { get; set; }

        [DataMember]
        public bool Result { get; set; }
    }
}
