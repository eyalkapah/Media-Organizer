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
        private string _selectedFileAction;
        private string _sourceFolder;

        public string DestinationFolder
        {
            get => _destinationFolder;
            set => SetProperty(ref _destinationFolder, value);
        }

        public List<string> FileActions { get; set; }

        public List<RegexPattern> Patterns
        {
            get => _patterns;
            set => SetProperty(ref _patterns, value);
        }

        public ICommand SelectDestinationFolderCommand { get; set; }

        public string SelectedFileAction
        {
            get => _selectedFileAction;
            set => SetProperty(ref _selectedFileAction, value, () => OnFileActionChanged(value));
        }

        public ICommand SelectSourceFolderCommand { get; set; }

        public string SourceFolder
        {
            get => _sourceFolder;
            set => SetProperty(ref _sourceFolder, value);
        }

        // C'tor
        //
        public FileOrganizerViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService, ISettingsService settingsService) : base(logProvider, navigationService)
        {
            SelectSourceFolderCommand = new MvxCommand(SelectSourceFolderAsync);
            SelectDestinationFolderCommand = new MvxCommand(SelectDestinationFolderAsync);

            _settingsService = settingsService;

            FileActions = new List<string> { "Copy", "Move" };
        }

        public async void AddPatternAsync()
        {
            var result = await NavigationService.Navigate<AddPatternDialogViewModel, RegexPattern>().ConfigureAwait(true);
        }

        public void SavePatterns()
        {
            _settingsService.FolderSettings.Patterns = Patterns;
        }

        protected override void InitFromBundle(IMvxBundle parameters)
        {
            base.InitFromBundle(parameters);

            SourceFolder = _settingsService.FolderSettings.SourceFolder;
            DestinationFolder = _settingsService.FolderSettings.DestinationFolder;
            Patterns = _settingsService.FolderSettings.Patterns;
            SelectedFileAction = _settingsService.FolderSettings.FileAction;
        }

        private void OnFileActionChanged(string action)
        {
            _settingsService.FolderSettings.FileAction = action;
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
