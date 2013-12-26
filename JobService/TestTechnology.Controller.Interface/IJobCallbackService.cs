using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using TestTechnology.Controller.DTO;

namespace TestTechnology.Controller.Interface
{
    public interface IJobCallbackService
    {
        [OperationContract]
        void DoTestJobs(IEnumerable<Job> jobs);
    }
}
