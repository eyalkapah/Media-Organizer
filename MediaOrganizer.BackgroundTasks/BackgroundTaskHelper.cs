using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Background;

namespace MediaOrganizer.BackgroundTasks
{
    public static class BackgroundTaskHelper
    {
        public static BackgroundTaskRegistration RegisterBackgroundTask(string name, string entryPoint, int minuteIncrement)
        {
            try
            {
                var allowed = BackgroundExecutionManager.RequestAccessAsync();

                var taskBuilder = new BackgroundTaskBuilder
                {
                    Name = name,
                    TaskEntryPoint = entryPoint
                };

                var allTasks = BackgroundTaskRegistration.AllTasks;

                var backgroundTasks = allTasks.Where(t => t.Value.Name.Equals(name));

                if (!backgroundTasks.Any())
                {
                    var trigger = new TimeTrigger((uint)minuteIncrement, false);

                    taskBuilder.SetTrigger(trigger);

                    var task = taskBuilder.Register();

                    return task;
                }

                return null;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
