using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestTechnology.Controller.DTO;
using TestTechnology.Shared.DTO;

namespace TestTechnology.Controller.BIZ
{
    public interface IJobBIZ
    {
        JobGroup GetUnTakenTopJobsByClientsID(string clientId);
        void UpdateJobStatus(int jobID, JobStatus updateStatus);
        void UploadJobResult(int jobID, string jobResult);
    }
}
