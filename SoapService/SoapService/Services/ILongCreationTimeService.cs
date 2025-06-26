using CoreWCF;
using SoapService.Enums;

namespace SoapService.Services
{
    [ServiceContract]
    public interface ILongCreationTimeService
    {
        [OperationContract]
        public string RequestSomeData();

        [OperationContract]
        public CustomTaskStatus RequestStatus(string taskId);
    }
}
