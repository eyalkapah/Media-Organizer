using System;
using System.Threading.Tasks;

namespace MediaOrganizer.Core.Interfaces
{
    public interface IBackgroundTasksService
    {
        event EventHandler MediaFilesScanTaskCompleted;

        bool IsBackgroundTaskRegistered(string taskName);

        bool RegisterMediaFilesScanTask(int minuteIncrement);

        void RunMediaScanTask();

        bool UnregisterBackgroundTask(string name);
        void SetLastScan();

        DateTime? GetLastScan();

        Task<bool> IsMediaAvailableAsync();
    }
}
