using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xaml.Interactivity;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace MediaOrganizer.UWP.Actions
{
    public class ClearTextBoxBorderBrushAction : DependencyObject, IAction
    {
        public object Execute(object sender, object parameter)
        {
            if (!(sender is TextBox tb))
                return true;

            tb.BorderBrush = (SolidColorBrush)Application.Current.Resources["TextControlBorderBrush"];

            return true;
        }
    }
}
