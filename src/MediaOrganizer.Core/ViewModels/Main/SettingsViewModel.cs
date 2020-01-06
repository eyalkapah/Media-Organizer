using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using MediaOrganizer.Core.Models;
using MediaOrganizer.Core.Models.Settings;
using MvvmCross.Commands;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace MediaOrganizer.Core.ViewModels.Main
{
    public class SettingsViewModel : MvxNavigationViewModel
    {
        public ICommand ScanMediaCommand { get; set; }

        private bool _isServiceEnabled;
        private readonly ISettingsService _settingsService;

        public bool IsServiceEnabled
        {
            get => _isServiceEnabled;
            set => SetProperty(ref _isServiceEnabled, value);
        }

        public List<MediaInterval> MediaScanIntervals { get; set; }

        private MediaInterval _selectedMediaScanInterval;

        public MediaInterval SelectedMediaScanInterval
        {
            get => _selectedMediaScanInterval;
            set => SetProperty(ref _selectedMediaScanInterval, value);
        }

        public SettingsViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService, ISettingsService settingsService) : base(logProvider, navigationService)
        {
            ScanMediaCommand = new MvxCommand(ScanMedia);
            _settingsService = settingsService;

            MediaScanIntervals = new List<MediaInterval>
            {
                new MediaInterval { Interval = 30, Description = "30 minutes"},
                new MediaInterval { Interval = 60, Description = "1 hour"},
                new MediaInterval { Interval = 360, Description = "6 hours"},
                new MediaInterval { Interval = 1440, Description = "1 day"}
            };
        }

        protected override void InitFromBundle(IMvxBundle parameters)
        {
            base.InitFromBundle(parameters);

            var interval = _settingsService.Instance.ActivationSettings?.ServiceScanIntervalInMinutes ?? 30;

            SelectedMediaScanInterval = MediaScanIntervals.FirstOrDefault(i => i.Interval.Equals(interval));
        }

        private void ScanMedia()
        {
        }
    }
}
