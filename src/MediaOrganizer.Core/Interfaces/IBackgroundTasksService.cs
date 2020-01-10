using System;

namespace MediaOrganizer.Core.Interfaces
{
    public interface IBackgroundTasksService
    {
        event EventHandler MediaFilesScanTaskCompleted;

        bool IsBackgroundTaskRegistered(string taskName);

        bool RegisterMediaFilesScanTask(int minuteIncrement);

        bool UnregisterBackgroundTask(string name);
    }
}
