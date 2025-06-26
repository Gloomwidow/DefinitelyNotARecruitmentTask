using CoreWCF;
using SoapService.Enums;
using System.Collections.Concurrent;
using System.Security.Cryptography.X509Certificates;

namespace SoapService.Services
{
    public class LongCreationTimeService : ILongCreationTimeService
    {
        // To simulate delay time
        private const int DelayTime = 30 * 1000; // 30 seconds

        private readonly ConcurrentDictionary<string, CustomTaskStatus> CurrentTasks = new ConcurrentDictionary<string, CustomTaskStatus>();

        public string RequestSomeData()
        {
            var taskGuid = Guid.NewGuid().ToString();

            if (CurrentTasks.ContainsKey(taskGuid))
            {
                throw new FaultException("Generated already existing GUID for task");
            }

            CurrentTasks.TryAdd(taskGuid, CustomTaskStatus.InProgress);
            DoTask(taskGuid);

            return taskGuid;
        }

        public CustomTaskStatus RequestStatus(string taskId)
        {
            if (!CurrentTasks.ContainsKey(taskId))
            {
                throw new FaultException($"Task with id {taskId} not found");
            }

            var taskStatus = CurrentTasks[taskId];

            return taskStatus;
        }

        public async void DoTask(string taskId)
        {
            Task.Delay(DelayTime).Wait();
            CurrentTasks.TryUpdate(taskId, CustomTaskStatus.Completed, CustomTaskStatus.InProgress);
        }
    }
}
