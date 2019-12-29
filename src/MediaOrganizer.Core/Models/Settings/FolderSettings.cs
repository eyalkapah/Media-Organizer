namespace MediaOrganizer.Core.Models.Settings
{
    public class FolderSettings
    {
        private readonly ISettingsContainer _container;

        public string SourceFolder
        {
            get => _container.GetValueOrDefault(nameof(SourceFolder), GlobalSettings.SourceFolder);
            set => _container.SetValue(nameof(SourceFolder), value);
        }

        public string DestinationFolder
        {
            get => _container.GetValueOrDefault(nameof(DestinationFolder), GlobalSettings.DestinationFolder);
            set => _container.SetValue(nameof(DestinationFolder), value);
        }

        public FolderSettings(ISettingsContainer container)
        {
            _container = container;
        }
    }
}
