using System;
using MediaOrganizer.Core.Models;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace MediaOrganizer.Core.ViewModels.Main.Dialogs
{
    public class SimpleTextDialogViewModel : MvxNavigationViewModel<SimpleTextDialogParametersModel>
    {
        private string _primaryButtonText;
        private string _secondaryButtonText;
        private string _text;
        private string _title;

        public Action PrimaryAction { get; set; }

        public string PrimaryButtonText
        {
            get => _primaryButtonText;
            set => SetProperty(ref _primaryButtonText, value);
        }

        public Action SecondaryAction { get; set; }

        public string SecondaryButtonText
        {
            get => _secondaryButtonText;
            set => SetProperty(ref _secondaryButtonText, value);
        }

        public string Text
        {
            get => _text;
            set => SetProperty(ref _text, value);
        }

        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }

        public SimpleTextDialogViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService) : base(logProvider, navigationService)
        {
        }

        public void Cancel()
        {
            SecondaryAction?.Invoke();

            NavigationService.Close(this);
        }

        public void Ok()
        {
            PrimaryAction?.Invoke();

            NavigationService.Close(this);
        }

        public override void Prepare(SimpleTextDialogParametersModel parameter)
        {
            if (parameter == null)
                return;

            Title = parameter.Title;
            Text = parameter.Text;
            PrimaryButtonText = parameter.PrimaryButtonText;
            SecondaryButtonText = parameter.SecondaryButtonText;
            PrimaryAction = parameter.PrimaryButtonAction;
            SecondaryAction = parameter.SecondaryButtonAction;
        }
    }
}
