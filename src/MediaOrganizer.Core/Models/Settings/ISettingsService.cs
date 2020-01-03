namespace MediaOrganizer.Core.Models.Settings
{
    public interface ISettingsService
    {
        SettingsModel Instance { get; }

        void Save();
    }
}
