using System.Collections.Generic;
using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace MediaOrganizer.Core.Models.Settings
{
    public class SettingsModel
    {
        [JsonProperty("folder_settings")]
        public FolderSettings FolderSettings { get; set; }

        public SettingsModel()
        {
            FolderSettings = new FolderSettings
            {
                DestinationFolder = "",

                FileAction = "Move",

                SourceFolder = "D:\\Downloads\\Telegram Desktop",

                Patterns = new List<RegexPattern>
                {
                    new RegexPattern("Nati Media", "נתי מדיה")
                }
            };
        }
    }
}
