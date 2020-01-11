using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Documents;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;

// The Templated Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234235

namespace MediaOrganizer.UWP.CustomControls.ScanMediaControl
{
    public sealed class ScanMedia : Control
    {
        public static readonly DependencyProperty ImageSourceUriProperty =
            DependencyProperty.Register("ImageSourceUri", typeof(string), typeof(ScanMedia), new PropertyMetadata(default));

        public static readonly DependencyProperty TextProperty =
                    DependencyProperty.Register("Text", typeof(string), typeof(ScanMedia), new PropertyMetadata(default));

        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register("Title", typeof(string), typeof(ScanMedia), new PropertyMetadata(default));

        public static readonly DependencyProperty UpToDateImageProperty =
            DependencyProperty.Register("UpToDateImage", typeof(string), typeof(ScanMedia), new PropertyMetadata(default));

        public string ImageSourceUri
        {
            get => (string)GetValue(ImageSourceUriProperty);
            set => SetValue(ImageSourceUriProperty, value);
        }

        public string Text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }

        public string Title
        {
            get => (string)GetValue(TitleProperty);
            set => SetValue(TitleProperty, value);
        }

        public string UpToDateImage
        {
            get => (string)GetValue(UpToDateImageProperty);
            set => SetValue(UpToDateImageProperty, value);
        }

        public ScanMedia()
        {
            DefaultStyleKey = typeof(ScanMedia);
        }

        protected override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            ImageSourceUri = "ms-appx:///Assets/Images/up_to_date.png";
        }
    }
}
