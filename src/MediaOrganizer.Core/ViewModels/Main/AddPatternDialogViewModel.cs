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
    public class AddPatternDialogViewModel : MvxNavigationViewModelResult<RegexPattern>
    {
        private readonly ISettingsService _settingsService;
        private bool _isFormValid;
        private string _regex;
        private string _regexErrorMessage;
        private string _replaceString;
        private string _title;

        private string _word;
        private string _wordErrorMessage;

        public ICommand CancelCommand { get; set; }

        public bool IsFormValid
        {
            get => _isFormValid;
            set => SetProperty(ref _isFormValid, value);
        }

        public ICommand OkCommand { get; set; }
        public List<RegexPattern> Patterns { get; private set; }

        public string Regex
        {
            get => _regex;
            set => SetProperty(ref _regex, value, () => ValidateRegex(value));
        }

        public string RegexErrorMessage
        {
            get => _regexErrorMessage;
            set => SetProperty(ref _regexErrorMessage, value);
        }

        public string ReplaceString
        {
            get => _replaceString;
            set => SetProperty(ref _replaceString, value, () => SetFormValidation());
        }

        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }

        public string Word
        {
            get => _word;
            set => SetProperty(ref _word, value, () => ValidateWord(value));
        }

        public string WordErrorMessage
        {
            get => _wordErrorMessage;
            set => SetProperty(ref _wordErrorMessage, value);
        }

        public AddPatternDialogViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService, ISettingsService settingsService) : base(logProvider, navigationService)
        {
            Title = "Add Pattern";
            IsFormValid = false;
            OkCommand = new MvxCommand(Ok);
            CancelCommand = new MvxCommand(() => NavigationService.Close(this));
            _settingsService = settingsService;
        }

        public override Task Initialize()
        {
            Patterns = _settingsService.Instance.FolderSettings.Patterns;

            return Task.CompletedTask;
        }

        private void Ok()
        {
            NavigationService.Close(this, new RegexPattern(Word, Regex, ReplaceString));
        }

        private void SetFormValidation()
        {
            IsFormValid = !string.IsNullOrEmpty(Word) && !string.IsNullOrEmpty(Regex) && string.IsNullOrEmpty(WordErrorMessage);
        }

        private void ValidateRegex(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                RegexErrorMessage = "Regex cannot be empty.";
            }
            else
            {
                RegexErrorMessage = null;
            }

            SetFormValidation();
        }

        private void ValidateWord(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                WordErrorMessage = "Word cannot be empty.";
            }
            else if (Patterns != null && Patterns.Any(p => string.Equals(p.Word, value, StringComparison.CurrentCultureIgnoreCase)))
            {
                WordErrorMessage = "Word already exists.";
            }
            else
            {
                WordErrorMessage = null;
            }

            SetFormValidation();
        }
    }
}
