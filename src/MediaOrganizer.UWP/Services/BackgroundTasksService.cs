using System;
using System.Diagnostics;
using System.Linq;
using MediaOrganizer.BackgroundTasks;
using MediaOrganizer.Core;
using MediaOrganizer.Core.Interfaces;
using MediaOrganizer.Core.Models.Settings;
using Windows.ApplicationModel.Background;
using Windows.Storage;

namespace MediaOrganizer.UWP.Services
{
    public class BackgroundTasksService : IBackgroundTasksService
    {
        public event EventHandler MediaFilesScanTaskCompleted = delegate
        { };

        public DateTime? GetLastScan()
        {
            var localSettings = ApplicationData.Current.LocalSettings;

            var lastScan = localSettings.Values[Constants.LastScannedSettings];

            if (lastScan == null)
                return null;

            return DateTime.Parse(lastScan.ToString());
        }

        public bool IsBackgroundTaskRegistered(string taskName)
        {
            return BackgroundTaskRegistration.AllTasks.Any(b => b.Value.Name.Equals(taskName));
        }

        public bool RegisterMediaFilesScanTask(int minuteIncrement)
        {
            try
            {
                var allowed = BackgroundExecutionManager.RequestAccessAsync();

                var taskBuilder = new BackgroundTaskBuilder
                {
                    Name = Constants.MediaFilesScanBackgroundTaskName,
                    TaskEntryPoint = Constants.MediaFilesScanBackgroundEntryPoint
                };

                var allTasks = BackgroundTaskRegistration.AllTasks;

                var backgroundTasks = allTasks.Where(t => t.Value.Name.Equals(taskBuilder.Name));

                if (!backgroundTasks.Any())
                {
                    var trigger = new TimeTrigger((uint)minuteIncrement, false);

                    taskBuilder.SetTrigger(trigger);

                    var task = taskBuilder.Register();

                    task.Completed += (s, e) => MediaFilesScanTaskCompleted.Invoke(s, null);

                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                Debug.Fail($"Failed to register background task: {ex.Message}");

                return false;
            }
        }

        public async void RunMediaScanTask()
        {
            try
            {
                var task = new MediaFilesScanTask();
                await task.RunTaskAsync(null);
            }
            catch (Exception ex)
            {
            }

            //MediaFilesScanTask.RunTaskAsync(_settingsService.Instance.FolderSettings);
        }

        public void SetLastScan()
        {
            var localSettings = ApplicationData.Current.LocalSettings;

            localSettings.AddOrUpdate(Constants.LastScannedSettings, DateTime.Now.ToString());
        }

        public bool UnregisterBackgroundTask(string name)
        {
            foreach (var task in
                //
                from cur in BackgroundTaskRegistration.AllTasks
                where cur.Value.Name == name
                select cur)
            {
                task.Value.Unregister(true);
                return true;
            }

            return false;
        }
    }
}
