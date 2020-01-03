using System.Threading.Tasks;
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

        protected async override Task EnteringBackground(IMvxSuspensionManager suspensionManager)
        {
            await _settingsService.SaveAsync();
        }

        protected override void OnLaunched(LaunchActivatedEventArgs activationArgs)
        {
            base.OnLaunched(activationArgs);

            Mvx.IoCProvider.ConstructAndRegisterSingleton<IPickerService, PickerService>();

            _settingsService = new SettingsService();

            Mvx.IoCProvider.RegisterSingleton(typeof(ISettingsService), _settingsService);
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
