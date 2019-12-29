using MediaOrganizer.Core.Models.Settings;
using Windows.Storage;

namespace MediaOrganizer.UWP.Services
{
    public class SettingsService : ISettingsService
    {
        private const string AppSettingsKey = "AppSettings";
        private const string FoldersContainerName = "FoldersContainer";

        private readonly ISettingsContainer _rootSettingsContainer;

        public SettingsService()
        {
            _rootSettingsContainer = new SettingsContainer(ApplicationData.Current.LocalSettings);

            _rootSettingsContainer.CreateRoot(AppSettingsKey);

            FolderSettings = new FolderSettings(_rootSettingsContainer.GetOrCreateContainer(FoldersContainerName));
        }

        public FolderSettings FolderSettings { get; set; }
    }
}
