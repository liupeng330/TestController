using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace TestTechnology.Controller.DTO
{
    [DataContract]
    public class ClientMachineInfo
    {
        [DataMember]
        public string ClientID { get; set; }

        [DataMember]
        public string OS { get; set; }

        [DataMember]
        public string SystemType { get; set; }

        [DataMember]
        public string MachineName { get; set; }
    }
}
