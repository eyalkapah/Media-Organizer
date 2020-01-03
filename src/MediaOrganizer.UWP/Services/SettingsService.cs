using System.Text.Json;
using MediaOrganizer.Core.Models.Settings;
using Windows.Storage;
using System;
using System.Threading.Tasks;
using System.IO;
using System.Text;
using Newtonsoft.Json;

namespace MediaOrganizer.UWP.Services
{
    public class SettingsService : ISettingsService
    {
        private const string Filename = "settings.json";

        public SettingsModel Instance { get; private set; }

        public SettingsService()
        {
            Instance = new SettingsModel();

            //FolderSettings = new FolderSettings(_rootSettingsContainer.GetOrCreateContainer(FoldersContainerName));
            ReadSettingsAsync();
        }

        public async void ReadSettingsAsync()
        {
            //var localFolder = Package.Current.InstalledLocation;
            var localFolder = ApplicationData.Current.LocalFolder;

            if (File.Exists(Path.Combine(localFolder.Path, Filename)))
            {
                var file = await localFolder.GetFileAsync(Filename);

                var text = await FileIO.ReadTextAsync(file);

                Instance = JsonConvert.DeserializeObject<SettingsModel>(text);
            }
        }

        public async Task SaveAsync()
        {
            var localFolder = ApplicationData.Current.LocalFolder;

            var json = JsonConvert.SerializeObject(Instance);

            var file = !File.Exists(Path.Combine(localFolder.Path, Filename))
                ? await localFolder.CreateFileAsync(Filename)
                : await localFolder.GetFileAsync(Filename);

            await FileIO.WriteTextAsync(file, json);
        }
    }
}
