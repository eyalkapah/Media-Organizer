using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using MediaOrganizer.Core.Interfaces;
using MediaOrganizer.Core.Models;
using MediaOrganizer.Core.Models.Settings;
using Microsoft.Extensions.Configuration;
using MvvmCross;
using MvvmCross.Commands;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace MediaOrganizer.Core.ViewModels.Main
{
    public class FileOrganizerViewModel : MvxNavigationViewModel
    {
        private readonly ISettingsService _settingsService;
        private string _destinationFolder;
        private List<RegexPattern> _patterns;
        private string _sourceFolder;

        // C'tor
        //
        public FileOrganizerViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService, ISettingsService settingsService) : base(logProvider, navigationService)
        {
            SelectSourceFolderCommand = new MvxCommand(SelectSourceFolderAsync);
            SelectDestinationFolderCommand = new MvxCommand(SelectDestinationFolderAsync);
            SavePatternsCommand = new MvxCommand(SavePatterns);
            AddPatternCommand = new MvxCommand(AddPattern);

            _settingsService = settingsService;
        }

        public string DestinationFolder
        {
            get => _destinationFolder;
            set => SetProperty(ref _destinationFolder, value);
        }

        public List<RegexPattern> Patterns
        {
            get => _patterns;
            set => SetProperty(ref _patterns, value);
        }

        public ICommand SavePatternsCommand { get; set; }
        public ICommand SelectDestinationFolderCommand { get; set; }
        public ICommand SelectSourceFolderCommand { get; set; }
        public ICommand AddPatternCommand { get; set; }

        public string SourceFolder
        {
            get => _sourceFolder;
            set => SetProperty(ref _sourceFolder, value);
        }

        public void AddPattern()
        {
            NavigationService.Navigate<AddPatternDialogViewModel>();
        }

        protected override void InitFromBundle(IMvxBundle parameters)
        {
            base.InitFromBundle(parameters);

            SourceFolder = _settingsService.FolderSettings.SourceFolder;
            DestinationFolder = _settingsService.FolderSettings.DestinationFolder;
            Patterns = _settingsService.FolderSettings.Patterns;
        }

        private void SavePatterns()
        {
            _settingsService.FolderSettings.Patterns = Patterns;
        }

        private async void SelectDestinationFolderAsync()
        {
            var pickerService = Mvx.IoCProvider.Resolve<IPickerService>();

            DestinationFolder = await pickerService.SelectFolderAsync().ConfigureAwait(true);

            _settingsService.FolderSettings.DestinationFolder = DestinationFolder;
        }

        private async void SelectSourceFolderAsync()
        {
            var pickerService = Mvx.IoCProvider.Resolve<IPickerService>();

            SourceFolder = await pickerService.SelectFolderAsync().ConfigureAwait(true);

            _settingsService.FolderSettings.SourceFolder = SourceFolder;
        }
    }
}
