using MediaOrganizer.Core.Models.Settings;
using System.IO;
using Newtonsoft.Json;

namespace MediaOrganizer.UWP.Services
{
    public class SettingsService : ISettingsService
    {
        private const string Filename = "settings.json";
        private readonly string _localFolder;

        public string FullPath => Path.Combine(_localFolder, Filename);

        public SettingsModel Instance { get; private set; }

        public SettingsService(string localFolder)
        {
            Instance = new SettingsModel();

            _localFolder = localFolder;

            ReadSettings();
        }

        public void ReadSettings()
        {
            var text = string.Empty;

            if (File.Exists(FullPath))
            {
                text = File.ReadAllText(FullPath);

                Instance = JsonConvert.DeserializeObject<SettingsModel>(text);

                if (Instance != null)
                    return;
            }

            text = File.ReadAllText("default-settings.json");

            Instance = JsonConvert.DeserializeObject<SettingsModel>(text);
        }

        public void Save()
        {
            var json = JsonConvert.SerializeObject(Instance);

            FileStream stream = null;

            if (!File.Exists(FullPath))
            {
                using (stream = File.Create(FullPath))
                {
                    File.WriteAllText(FullPath, json);
                }
            }
            else
                File.WriteAllText(FullPath, json);
        }
    }
}
