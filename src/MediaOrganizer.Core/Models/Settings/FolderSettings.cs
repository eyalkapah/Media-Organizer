using System.Collections.Generic;
using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace MediaOrganizer.Core.Models.Settings
{
    public class FolderSettings
    {
        [JsonProperty("destination_folder")]
        public string DestinationFolder { get; set; }

        [JsonProperty("file_action")]
        public string FileAction { get; set; }

        [JsonProperty("patterns")]
        public List<RegexPattern> Patterns { get; set; }

        [JsonProperty("source_folder")]
        public string SourceFolder { get; set; }
    }
}
