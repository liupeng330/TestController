using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace JobServiceInterface
{
    public interface IJobCallbackService
    {
        [OperationContract]
        void DoTestJobs(IEnumerable<Job> jobs);
    }
}
