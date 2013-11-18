using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace JobServiceInterface
{
    [ServiceContract(Name = "JobService", Namespace = "http://pengliu.me/", CallbackContract = typeof(IJobCallbackService))]
    public interface IJobService
    {
        [OperationContract]
        void IsAlive(string ipAddress);
    }
}
