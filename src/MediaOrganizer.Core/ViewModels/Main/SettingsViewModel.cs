using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using MediaOrganizer.Core.Interfaces;
using MediaOrganizer.Core.Models;
using MediaOrganizer.Core.Models.Settings;
using MediaOrganizer.Core.ViewModels.Main.Dialogs;
using MvvmCross.Commands;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace MediaOrganizer.Core.ViewModels.Main
{
    public class SettingsViewModel : MvxNavigationViewModel
    {
        private readonly IBackgroundTasksService _backgroundTasksService;
        private readonly ISettingsService _settingsService;
        private bool? _isServiceEnabled;
        private MediaInterval _selectedMediaScanInterval;

        public bool IsServiceEnabled
        {
            get => _isServiceEnabled == true;
            set
            {
                if (_isServiceEnabled != value)
                {
                    if (_isServiceEnabled != null)
                        ServiceEnabledChangedAsync(value);

                    _isServiceEnabled = value;

                    RaisePropertyChanged();
                }
            }
        }

        public List<MediaInterval> MediaScanIntervals { get; set; }

        public ICommand ScanMediaCommand { get; set; }

        public MediaInterval SelectedMediaScanInterval
        {
            get => _selectedMediaScanInterval;
            set => SetProperty(ref _selectedMediaScanInterval, value);
        }

        public SettingsViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService, ISettingsService settingsService, IBackgroundTasksService backgroundTasksService) : base(logProvider, navigationService)
        {
            ScanMediaCommand = new MvxCommand(ScanMedia);

            _settingsService = settingsService;
            _backgroundTasksService = backgroundTasksService;

            MediaScanIntervals = new List<MediaInterval>
            {
                new MediaInterval { Interval = 30, Description = "30 minutes"},
                new MediaInterval { Interval = 60, Description = "1 hour"},
                new MediaInterval { Interval = 360, Description = "6 hours"},
                new MediaInterval { Interval = 1440, Description = "1 day"}
            };

            backgroundTasksService.MediaFilesScanTaskCompleted += MediaFilesScanTaskCompleted;
        }

        public override Task Initialize()
        {
            var interval = _settingsService.Instance.ActivationSettings?.ServiceScanIntervalInMinutes ?? 30;

            SelectedMediaScanInterval = MediaScanIntervals.FirstOrDefault(i => i.Interval.Equals(interval));

            IsServiceEnabled = _backgroundTasksService.IsBackgroundTaskRegistered(Constants.MediaFilesScanBackgroundTaskName);

            return base.Initialize();
        }

        private void MediaFilesScanTaskCompleted(object sender, EventArgs e)
        {
        }

        private void ScanMedia()
        {
            _backgroundTasksService.RunMediaScanTask();
        }

        private async void ServiceEnabledChangedAsync(bool isServiceEnabled)
        {
            var result = isServiceEnabled
                ? _backgroundTasksService.RegisterMediaFilesScanTask(SelectedMediaScanInterval.Interval)
                : _backgroundTasksService.UnregisterBackgroundTask(Constants.MediaFilesScanBackgroundTaskName);

            if (result == false)
            {
                await NavigationService.Navigate<SimpleTextDialogViewModel, SimpleTextDialogParametersModel>(new SimpleTextDialogParametersModel
                {
                    Title = "Error",
                    Text = isServiceEnabled
                    ? "Sorry, we failed to register the service."
                    : "Sorry, we failed to un-register the service"
                }).ConfigureAwait(false);
            }
        }
    }
}