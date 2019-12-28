using System;
using System.Collections.Generic;
using System.Text;
using MvvmCross.Navigation;

namespace MediaOrganizer.Core.ViewModels.Main
{
    public class MainViewModel : BaseViewModel
    {
        private readonly IMvxNavigationService _navigationService;

        public MainViewModel(IMvxNavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        public void NavigateToFileOrgnizerPage()
        {
            _navigationService.Navigate<FileOrganizerViewModel>();
        }
    }
}
