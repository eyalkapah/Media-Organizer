namespace MediaOrganizer.Core.Interfaces
{
    public interface IBackgroundTasksService
    {
        bool IsBackgroundTaskRegistered(string taskName);

        bool RegisterMediaFilesScanTask(int minuteIncrement);

        bool UnregisterBackgroundTask(string name);
    }
}
