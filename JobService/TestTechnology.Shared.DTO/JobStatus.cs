using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace TestTechnology.Shared.DTO
{
    [DataContract]
    public enum JobStatus
    {
        [EnumMember]
        Running = 1,

        [EnumMember]
        Pass,

        [EnumMember]
        Fail
    }
}
