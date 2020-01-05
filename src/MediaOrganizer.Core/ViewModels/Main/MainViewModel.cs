using System;
using System.Collections.Generic;
using System.Text;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace MediaOrganizer.Core.ViewModels.Main
{
    public class MainViewModel : MvxNavigationViewModel
    {
        public MainViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService) : base(logProvider, navigationService)
        {
        }

        public void NavigateToFileOrgnizerPage()
        {
            NavigationService.Navigate<FileOrganizerViewModel>();
        }

        public void NavigateToSettingsPage()
        {
            NavigationService.Navigate<SettingsViewModel>();
        }
    }
}
