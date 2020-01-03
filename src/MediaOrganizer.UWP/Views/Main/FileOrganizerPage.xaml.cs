using MediaOrganizer.Core.ViewModels.Main;
using MvvmCross.Platforms.Uap.Presenters.Attributes;
using MvvmCross.Platforms.Uap.Views;
using MvvmCross.ViewModels;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace MediaOrganizer.UWP.Views.Main
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    [MvxRegionPresentation("ContentFrame")]
    [MvxViewFor(typeof(FileOrganizerViewModel))]
    public sealed partial class FileOrganizerPage : MvxWindowsPage
    {
        public FileOrganizerViewModel Vm => DataContext as FileOrganizerViewModel;

        public FileOrganizerPage()
        {
            this.InitializeComponent();
        }
    }
}
