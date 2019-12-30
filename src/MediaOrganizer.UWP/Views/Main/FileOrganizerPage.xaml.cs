using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using MediaOrganizer.Core.ViewModels.Main;
using MvvmCross.Platforms.Uap.Presenters.Attributes;
using MvvmCross.Platforms.Uap.Views;
using MvvmCross.ViewModels;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

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

        private void StackPanel_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            var stackPanel = sender as StackPanel;

            stackPanel.Background = new SolidColorBrush(Colors.Bisque);
        }

        private void StackPanel_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            var stackPanel = sender as StackPanel;

            stackPanel.Background = new SolidColorBrush(Colors.Transparent);
        }
    }
}
