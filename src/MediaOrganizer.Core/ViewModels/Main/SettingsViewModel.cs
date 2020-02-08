using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using MediaOrganizer.Core.Enums;
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

        private ScanStatus _scanStatus;

        public ScanStatus ScanStatus
        {
            get => _scanStatus;
            set => SetProperty(ref _scanStatus, value);
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
            ScanMediaCommand = new MvxCommand(ScanMediaAsync);

            _settingsService = settingsService;
            _backgroundTasksService = backgroundTasksService;

            MediaScanIntervals = new List<MediaInterval>
            {
                new MediaInterval { Interval = 30, Description = "30 minutes"},
                new MediaInterval { Interval = 60, Description = "1 hour"},
                new MediaInterval { Interval = 360, Description = "6 hours"},
                new MediaInterval { Interval = 1440, Description = "1 day"}
            };

            if (backgroundTasksService != null)
                backgroundTasksService.MediaFilesScanTaskCompleted += MediaFilesScanTaskCompleted;
        }

        public async override Task Initialize()
        {
            var interval = _settingsService.Instance.ActivationSettings?.ServiceScanIntervalInMinutes ?? 30;

            SelectedMediaScanInterval = MediaScanIntervals.FirstOrDefault(i => i.Interval.Equals(interval));

            IsServiceEnabled = _backgroundTasksService.IsBackgroundTaskRegistered(Constants.MediaFilesScanBackgroundTaskName);

            var lastScanned = _backgroundTasksService.GetLastScan();

            if (await _backgroundTasksService.IsMediaAvailableAsync().ConfigureAwait(false))
            {
                ScanStatus = ScanStatus.MediaFileExist;
            }
        }

        private void MediaFilesScanTaskCompleted(object sender, EventArgs e)
        {
            FinishScan(ScanStatus.Idle);
        }

        private async void ScanMediaAsync()
        {
            ScanStatus = ScanStatus.Scanning;

            await Task.Run(() => Task.Delay(3000)).ConfigureAwait(false);

            _backgroundTasksService.RunMediaScanTask();

            FinishScan(ScanStatus.Idle);
        }

        private void FinishScan(ScanStatus status)
        {
            ScanStatus = status;

            _backgroundTasksService.SetLastScan();
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
