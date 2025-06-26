using CoreWCF;
using SoapService.Enums;
using System.Collections.Concurrent;

namespace SoapService.Services
{
    public class LongCreationTimeService : ILongCreationTimeService
    {
        // To simulate delay time
        private const int DelayTime = 30 * 1000; // 30 seconds

        // This service creates task with GUID.
        // It returns GUID of created task so app can request status by other method
        // Event bus can also be used here

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
