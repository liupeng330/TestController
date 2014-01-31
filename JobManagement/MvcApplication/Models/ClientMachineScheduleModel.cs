using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcApplication.Models
{
    public class ClientMachineScheduleModel
    {
        public string ClientID { get; set; }
        public string OS { get; set; }
        public string SystemType { get; set; }
        public string MachineName { get; set; }
        public bool NeedToSchedule { get; set; }
    }
}