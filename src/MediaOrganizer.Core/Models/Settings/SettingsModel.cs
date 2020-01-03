using Newtonsoft.Json;

namespace MediaOrganizer.Core.Models.Settings
{
    public class SettingsModel
    {
        [JsonProperty("folder_settings")]
        public FolderSettings FolderSettings { get; set; }
    }
}
