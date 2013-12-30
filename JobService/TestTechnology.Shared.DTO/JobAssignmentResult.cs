using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace TestTechnology.Shared.DTO
{
    [DataContract]
    public enum JobAssignmentResult
    {
        [EnumMember]
        Pass = 1,

        [EnumMember]
        Fail,

        [EnumMember]
        NotRun
    }
}
