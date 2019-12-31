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
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Content Dialog item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace MediaOrganizer.UWP.Views.Dialogs
{
    [MvxViewFor(typeof(AddPatternDialogViewModel))]
    [MvxDialogViewPresentation]
    public sealed partial class AddPatternDialog : MvxWindowsContentDialog
    {
        public AddPatternDialogViewModel Vm => DataContext as AddPatternDialogViewModel;

        public AddPatternDialog()
        {
            this.InitializeComponent();
        }
    }
}
