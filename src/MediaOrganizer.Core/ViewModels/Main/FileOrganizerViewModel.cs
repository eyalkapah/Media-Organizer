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
        private string _saveErrorMessage;
        private string _selectedFileAction;
        private PatternViewModel _selectedPattern;
        private string _sourceFolder;

        public ICommand AddPatternCommand { get; set; }
        public ICommand DeletePatternCommand { get; set; }

        public string DestinationFolder
        {
            get => _destinationFolder;
            set => SetProperty(ref _destinationFolder, value);
        }

        public List<string> FileActions { get; set; }
        public MvxObservableCollection<PatternViewModel> Patterns { get; set; }
        public ICommand SaveAllPatternsCommand { get; set; }

        public string SaveErrorMessage
        {
            get => _saveErrorMessage;
            set => SetProperty(ref _saveErrorMessage, value);
        }

        public ICommand SelectDestinationFolderCommand { get; set; }

        public string SelectedFileAction
        {
            get => _selectedFileAction;
            set => SetProperty(ref _selectedFileAction, value, () => OnFileActionChanged(value));
        }

        public PatternViewModel SelectedPattern
        {
            get => _selectedPattern;
            set => SetProperty(ref _selectedPattern, value);
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
            Patterns = new MvxObservableCollection<PatternViewModel>();

            SelectSourceFolderCommand = new MvxCommand(SelectSourceFolderAsync);
            SelectDestinationFolderCommand = new MvxCommand(SelectDestinationFolderAsync);
            AddPatternCommand = new MvxCommand(AddPatternAsync);
            SaveAllPatternsCommand = new MvxCommand(SaveAllPatterns);
            DeletePatternCommand = new MvxCommand(DeletePattern);

            _settingsService = settingsService;

            FileActions = new List<string> { "Copy", "Move" };
        }

        public async void AddPatternAsync()
        {
            var result = await NavigationService.Navigate<AddPatternDialogViewModel, RegexPattern>().ConfigureAwait(true);

            if (result != null)
            {
                Patterns.Add(new PatternViewModel(result));
            }
        }

        public void SaveAllPatterns()
        {
            if (Patterns.Any(p => string.IsNullOrEmpty(p.Word) || string.IsNullOrEmpty(p.Pattern)))
            {
                SaveErrorMessage = "Word and Pattern must be specified.";
            }
            else
            {
                _settingsService.Instance.FolderSettings.Patterns = Patterns.Select(p => new RegexPattern(p.Word, p.Pattern, p.ReplaceString)).ToList();

                SaveErrorMessage = null;
            }
        }

        protected override void InitFromBundle(IMvxBundle parameters)
        {
            base.InitFromBundle(parameters);

            var folderSettings = _settingsService.Instance.FolderSettings;
            SourceFolder = folderSettings.SourceFolder;
            DestinationFolder = folderSettings.DestinationFolder;
            folderSettings.Patterns.ForEach(p => Patterns.Add(new PatternViewModel(p)));
            SelectedFileAction = folderSettings.FileAction;
        }

        private void DeletePattern()
        {
            Patterns.Remove(Patterns.First(p => p.Guid != SelectedPattern.Guid));

            SelectedPattern = null;
        }

        private void OnFileActionChanged(string action)
        {
            _settingsService.Instance.FolderSettings.FileAction = action;
        }

        private async void SelectDestinationFolderAsync()
        {
            var pickerService = Mvx.IoCProvider.Resolve<IPickerService>();

            DestinationFolder = await pickerService.SelectFolderAsync().ConfigureAwait(true);

            _settingsService.Instance.FolderSettings.DestinationFolder = DestinationFolder;
        }

        private async void SelectSourceFolderAsync()
        {
            var pickerService = Mvx.IoCProvider.Resolve<IPickerService>();

            SourceFolder = await pickerService.SelectFolderAsync().ConfigureAwait(true);

            _settingsService.Instance.FolderSettings.SourceFolder = SourceFolder;
        }
    }
}
