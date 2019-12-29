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
using MvvmCross.ViewModels;

namespace MediaOrganizer.Core.ViewModels.Main
{
    public class FileOrganizerViewModel : BaseViewModel
    {
        private readonly ISettingsService _settingsService;
        private string _destinationFolder;
        private string _sourceFolder;

        public string DestinationFolder
        {
            get => _destinationFolder;
            set => SetProperty(ref _destinationFolder, value);
        }

        public ICommand SelectDestinationFolderCommand { get; set; }
        public ICommand SelectSourceFolderCommand { get; set; }

        public string SourceFolder
        {
            get => _sourceFolder;
            set => SetProperty(ref _sourceFolder, value);
        }

        private List<RegexPattern> _patterns;

        public List<RegexPattern> Patterns
        {
            get => _patterns;
            set => SetProperty(ref _patterns, value);
        }

        // C'tor
        //
        public FileOrganizerViewModel(ISettingsService settingsService)
        {
            SelectSourceFolderCommand = new MvxCommand(SelectSourceFolderAsync);
            SelectDestinationFolderCommand = new MvxCommand(SelectDestinationFolderAsync);

            _settingsService = settingsService;
        }

        protected override void InitFromBundle(IMvxBundle parameters)
        {
            base.InitFromBundle(parameters);

            SourceFolder = _settingsService.FolderSettings.SourceFolder;
            DestinationFolder = _settingsService.FolderSettings.DestinationFolder;

            Patterns = new List<RegexPattern>
            {
                new RegexPattern("[](נתי מדיה[]"),
                new RegexPattern("1080P"),
                new RegexPattern("BluRay")
            };
        }

        private async void SelectDestinationFolderAsync()
        {
            var pickerService = Mvx.IoCProvider.Resolve<IPickerService>();

            DestinationFolder = await pickerService.SelectFolderAsync();

            _settingsService.FolderSettings.DestinationFolder = DestinationFolder;
        }

        private async void SelectSourceFolderAsync()
        {
            var pickerService = Mvx.IoCProvider.Resolve<IPickerService>();

            SourceFolder = await pickerService.SelectFolderAsync();

            _settingsService.FolderSettings.SourceFolder = SourceFolder;
        }
    }
}
