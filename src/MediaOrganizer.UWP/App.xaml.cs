using System.Threading.Tasks;
using MediaOrganizer.BackgroundTasks;
using MediaOrganizer.Core.Helpers;
using MediaOrganizer.Core.Interfaces;
using MediaOrganizer.Core.Models.Settings;
using MediaOrganizer.UWP.Services;
using MvvmCross;
using MvvmCross.IoC;
using MvvmCross.Platforms.Uap.Views;
using MvvmCross.Platforms.Uap.Views.Suspension;
using Windows.ApplicationModel.Activation;
using Windows.Storage;

namespace MediaOrganizer.UWP
{
    public abstract class MediaOrganizerApp : MvxApplication<Setup, Core.App>
    {
        private SettingsService _settingsService;

        protected override Task EnteringBackground(IMvxSuspensionManager suspensionManager)
        {
            _settingsService.Save();

            return Task.CompletedTask;
        }

        protected override void OnLaunched(LaunchActivatedEventArgs activationArgs)
        {
            base.OnLaunched(activationArgs);

            _settingsService = new SettingsService(ApplicationData.Current.LocalFolder.Path);
            Mvx.IoCProvider.RegisterSingleton(typeof(ISettingsService), _settingsService);

            Mvx.IoCProvider.ConstructAndRegisterSingleton<IPickerService, PickerService>();
            Mvx.IoCProvider.ConstructAndRegisterSingleton<IBackgroundTasksService, BackgroundTasksService>();


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
