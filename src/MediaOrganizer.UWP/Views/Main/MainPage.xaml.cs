using MvvmCross.Platforms.Uap.Presenters.Attributes;
using MvvmCross.Platforms.Uap.Views;
using MvvmCross.ViewModels;
using MediaOrganizer.Core.ViewModels.Main;

namespace MediaOrganizer.UWP
{
    [MvxPagePresentation]
    [MvxViewFor(typeof(MainViewModel))]
    public sealed partial class MainPage : MvxWindowsPage
    {
        public MainViewModel Vm => DataContext as MainViewModel;

        public MainPage()
        {
            InitializeComponent();
        }
    }
}
