using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Templated Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234235

namespace MediaOrganizer.UWP.CustomControls.NumericTextBoxControl
{
    public sealed class NumericTextBox : TextBox
    {
        protected override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            BeforeTextChanging += NumericTextBox_BeforeTextChanging;
        }

        private void NumericTextBox_BeforeTextChanging(TextBox sender, TextBoxBeforeTextChangingEventArgs args)
        {
            args.Cancel = args.NewText.Any(c => !char.IsDigit(c));

            if (args.Cancel)
                return;
        }
    }
}
