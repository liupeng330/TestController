using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using TestTechnology.Controller.DTO;
using TestTechnology.Shared.DTO;

namespace TestTechnology.Controller.Interface
{
    [ServiceContract(Name = "JobService", Namespace = "http://pengliu.me/", CallbackContract = typeof(IJobCallbackService))]
    public interface IJobService
    {
        [OperationContract]
        void IsAlive(string clientID);

        [OperationContract]
        JobGroup GetUnTakenTopJobsByClientsID(string clientID);

        [OperationContract]
        void UpdateJobStatus(int jobID, JobStatus updateStatus);

        [OperationContract]
        void UploadJobResult(int jobID, string jobResult);
    }
}
