using System;
using MediaOrganizer.Core.Interfaces;
using MediaOrganizer.UWP.Services;
using MvvmCross;
using MvvmCross.IoC;
using MvvmCross.Platforms.Uap.Core;
using MvvmCross.Platforms.Uap.Views;
using Windows.ApplicationModel.Activation;

namespace MediaOrganizer.UWP
{
    public abstract class MediaOrganizerApp : MvxApplication<Setup, Core.App>
    {
        protected override void OnLaunched(LaunchActivatedEventArgs activationArgs)
        {
            base.OnLaunched(activationArgs);

            Mvx.IoCProvider.ConstructAndRegisterSingleton<IPickerService, PickerService>();

            ReadSettings();
        }

        private void ReadSettings()
        {
            var localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
            var localFolder = Windows.Storage.ApplicationData.Current.LocalFolder;

            // Setting in a container
            var container = localSettings.CreateContainer("FoldersContainer", Windows.Storage.ApplicationDataCreateDisposition.Always);

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
