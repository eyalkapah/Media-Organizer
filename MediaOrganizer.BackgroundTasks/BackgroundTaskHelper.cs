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
        public static async Task<BackgroundTaskRegistration> RegisterBackgroundTaskAsync(string name, string entryPoint)
        {
            try
            {
                var allowed = await BackgroundExecutionManager.RequestAccessAsync();

                var taskBuilder = new BackgroundTaskBuilder
                {
                    Name = name,
                    TaskEntryPoint = entryPoint
                };
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
