﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MvcApplication.Models;

namespace MvcApplication.Models
{
    public class TaskScheduleModel
    {
        public int TaskGroupID { get; set; }
        public IEnumerable<ClientMachineInfo>  MachinInfos { get; set; }
    }
}