using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using MediaOrganizer.BackgroundTasks.Models;
using MediaOrganizer.Core.Models.Settings;
using MediaOrganizer.UWP.Services;
using Windows.ApplicationModel.Background;
using Windows.Storage;

namespace MediaOrganizer.BackgroundTasks
{
    public sealed class MediaFilesScanTask : IBackgroundTask
    {
        private const string MediaFileTypesPattern = "(.((mkv)|(mp4)))";

        public async void Run(IBackgroundTaskInstance taskInstance)
        {
            taskInstance.Canceled += OnCanceled;

            var defferal = taskInstance.GetDeferral();

            try
            {
                var folderSettings = new SettingsService(ApplicationData.Current.LocalFolder.Path).Instance.FolderSettings;

                var sourceFolder = await StorageFolder.GetFolderFromPathAsync(folderSettings.SourceFolder);

                var files = await sourceFolder.GetFilesAsync();

                var mediaFiles = files.Where(f => Regex.IsMatch(f.FileType, MediaFileTypesPattern));

                var updatedFiles = Rename(folderSettings, mediaFiles);

                if (!updatedFiles.Any())
                    return;

                var destinationFolder = await StorageFolder.GetFolderFromPathAsync(folderSettings.DestinationFolder);

                updatedFiles.ToList().ForEach(async f => await f.File.MoveAsync(destinationFolder, f.Filename));
            }
            catch (Exception ex)
            {
                Debug.Fail(ex.Message);
            }
            finally
            {
                defferal.Complete();
            }
        }

        private void OnCanceled(IBackgroundTaskInstance sender, BackgroundTaskCancellationReason reason)
        {
        }

        private static IEnumerable<UpdatedFile> Rename(FolderSettings folderSettings, IEnumerable<StorageFile> mediaFiles)
        {
            foreach (var file in mediaFiles)
            {
                var filename = file.Name;

                foreach (var pattern in folderSettings.Patterns)
                {
                    if (!Regex.IsMatch(filename, pattern.Pattern))
                        continue;

                    filename = Regex.Replace(filename, pattern.Pattern, pattern.ReplaceString);
                }

                yield return new UpdatedFile(file, filename);
            }

            yield return null;
        }
    }
}
