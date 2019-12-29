using MediaOrganizer.Core.Interfaces;
using MediaOrganizer.Core.Models.Settings;
using MediaOrganizer.UWP.Services;
using MvvmCross;
using MvvmCross.IoC;
using MvvmCross.Platforms.Uap.Views;
using Windows.ApplicationModel.Activation;
using Windows.Storage;

namespace MediaOrganizer.UWP
{
    public abstract class MediaOrganizerApp : MvxApplication<Setup, Core.App>
    {
        protected override void OnLaunched(LaunchActivatedEventArgs activationArgs)
        {
            base.OnLaunched(activationArgs);

            Mvx.IoCProvider.ConstructAndRegisterSingleton<IPickerService, PickerService>();
            Mvx.IoCProvider.ConstructAndRegisterSingleton<ISettingsService, SettingsService>();

            //ReadSettings();
        }

        private void ReadSettings()
        {
            var localSettings = ApplicationData.Current.LocalSettings;
            var localFolder = ApplicationData.Current.LocalFolder;

            // Setting in a container
            var container = localSettings.CreateContainer("FoldersContainer", ApplicationDataCreateDisposition.Always);

            if (localSettings.Containers.ContainsKey("FoldersContainer"))
            {
                localSettings.Containers["FoldersContainer"].Values["SourceFolder"] = "D:\\Downloads\\Telegram Desktop";
                localSettings.Containers["FoldersContainer"].Values["DestinationFolder"] = "E:\\Plex\\Movies";
            }
        }
    }

    sealed partial class App
    {
        public App()
        {
            InitializeComponent();
        }
    }
}
