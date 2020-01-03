using System.Collections.Generic;
using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace MediaOrganizer.Core.Models.Settings
{
    public class FolderSettings
    {
        private readonly ISettingsContainer _container;

        [JsonProperty("destination_folder")]
        public string DestinationFolder { get; set; }

        [JsonProperty("file_action")]
        public string FileAction { get; set; }

        [JsonProperty("patterns")]
        public List<RegexPattern> Patterns { get; set; }

        [JsonProperty("source_folder")]
        public string SourceFolder { get; set; }

        //public string DestinationFolder
        //{
        //    get => _container.GetValueOrDefault(nameof(DestinationFolder), GlobalSettings.DestinationFolder);
        //    set => _container.SetValue(nameof(DestinationFolder), value);
        //}

        //public string FileAction
        //{
        //    get => _container.GetValueOrDefault(nameof(FileAction), GlobalSettings.FileAction);
        //    set => _container.SetValue(nameof(FileAction), value);
        //}

        //public List<RegexPattern> Patterns
        //{
        //    get => _container.GetValueOrDefault(nameof(Patterns), GlobalSettings.Patterns);
        //    set => _container.SetValue(nameof(Patterns), value);
        //}

        //public string SourceFolder
        //{
        //    get => _container.GetValueOrDefault(nameof(SourceFolder), GlobalSettings.SourceFolder);
        //    set => _container.SetValue(nameof(SourceFolder), value);
        //}

        //public FolderSettings(ISettingsContainer container)
        //{
        //    _container = container;
        //}
    }
}
