using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using MvvmCross.Commands;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace MediaOrganizer.Core.ViewModels.Main
{
    public class AddPatternDialogViewModel : MvxNavigationViewModel
    {
        private bool _isDirty;
        private string _regex;
        private string _replace;
        private string _title;

        private string _word;
        private string _wordErrorMessage;
        public ICommand CancelCommand { get; set; }

        public bool IsDirty
        {
            get => _isDirty;
            set => SetProperty(ref _isDirty, value);
        }

        public ICommand OkCommand { get; set; }

        public string Regex
        {
            get => _regex;
            set => SetProperty(ref _regex, value, () => IsDirty = true);
        }

        public string Replace
        {
            get => _replace;
            set => SetProperty(ref _replace, value, () => IsDirty = true);
        }

        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }

        public string Word
        {
            get => _word;
            set => SetProperty(ref _word, value, () => OnWordChanged(value));
        }

        public string WordErrorMessage
        {
            get => _wordErrorMessage;
            set => SetProperty(ref _wordErrorMessage, value);
        }

        public AddPatternDialogViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService) : base(logProvider, navigationService)
        {
            Title = "Add Pattern";

            OkCommand = new MvxCommand(Ok, CanExecuteOk);

            CancelCommand = new MvxCommand(Cancel);
        }

        private void Cancel()
        {
            throw new NotImplementedException();
        }

        private bool CanExecuteOk() => !string.IsNullOrEmpty(Word) && !string.IsNullOrEmpty(Regex);

        private void Ok()
        {
            throw new NotImplementedException();
        }

        private void OnWordChanged(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                WordErrorMessage = "Word cannot be empty.";
            }
            else
            {
                WordErrorMessage = string.Empty;
            }
        }
    }
}
