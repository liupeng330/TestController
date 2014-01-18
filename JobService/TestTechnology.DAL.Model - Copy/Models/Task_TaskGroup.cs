using System;
using System.Collections.Generic;

namespace TestTechnology.DAL.Models
{
    public partial class Task_TaskGroup
    {
        public int TaskID { get; set; }
        public int TaskGroupID { get; set; }
        public int TaskOrder { get; set; }
        public virtual Task Task { get; set; }
        public virtual TaskGroup TaskGroup { get; set; }
    }
}
