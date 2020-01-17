using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Windows.Input;
using MediaOrganizer.Core.Enums;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Documents;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;

// The Templated Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234235

namespace MediaOrganizer.UWP.CustomControls.ScanMediaControl
{
    [TemplatePart(Name = ScanMediaButtonPart, Type = typeof(Button))]
    public sealed class ScanMedia : Control
    {
        public static readonly DependencyProperty ImageSourceUriProperty =
                    DependencyProperty.Register("ImageSourceUri", typeof(Uri), typeof(ScanMedia), new PropertyMetadata(default));

        public static readonly DependencyProperty ScanMediaCommandProperty =
            DependencyProperty.Register("ScanMediaCommand", typeof(ICommand), typeof(ScanMedia), new PropertyMetadata(default));

        public static readonly DependencyProperty ScanStatusProperty =
            DependencyProperty.Register("ScanStatus", typeof(ScanStatus), typeof(ScanMedia), new PropertyMetadata(ScanStatus.Idle, ScanStatusChanged));

        public static readonly DependencyProperty TextProperty =
                                    DependencyProperty.Register("Text", typeof(string), typeof(ScanMedia), new PropertyMetadata(default));

        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register("Title", typeof(string), typeof(ScanMedia), new PropertyMetadata(default));

        public static readonly DependencyProperty UpToDateImageProperty =
            DependencyProperty.Register("UpToDateImage", typeof(string), typeof(ScanMedia), new PropertyMetadata(default));

        private const string ScanMediaButtonPart = "PART_ScanMediaButton";

        private Dictionary<ScanStatus, Uri> _scanStatusMap = new Dictionary<ScanStatus, Uri>
        {
            {  ScanStatus.Idle,  new Uri("ms-appx:///Assets/Images/up_to_date.png") },
            {  ScanStatus.Scanning,  new Uri("ms-appx:///Assets/Images/checking_media.png") },
        };

        public Uri ImageSourceUri
        {
            get => (Uri)GetValue(ImageSourceUriProperty);
            set => SetValue(ImageSourceUriProperty, value);
        }

        public ICommand ScanMediaCommand
        {
            get => (ICommand)GetValue(ScanMediaCommandProperty);
            set => SetValue(ScanMediaCommandProperty, value);
        }

        public ScanStatus ScanStatus
        {
            get => (ScanStatus)GetValue(ScanStatusProperty);
            set => SetValue(ScanStatusProperty, value);
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

            ImageSourceUri = _scanStatusMap[ScanStatus.Idle];

            var button = GetTemplateChild(ScanMediaButtonPart) as Button;

            button.Tapped += ScanMediaButton_Tapped;
        }

        private static void ScanStatusChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var status = (ScanStatus)Enum.Parse(typeof(ScanStatus), e.NewValue.ToString());

            var sender = d as ScanMedia;

            sender.SetImage(status);
        }

        private void ScanMediaButton_Tapped(object sender, TappedRoutedEventArgs e)
        {
        }

        private void SetImage(ScanStatus status)
        {
            ImageSourceUri = _scanStatusMap[status];
        }
    }
}
