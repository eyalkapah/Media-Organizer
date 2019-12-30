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

namespace MediaOrganizer.UWP.CustomControls.ListViewButton
{
    [TemplatePart(Name = StackPanelPart, Type = typeof(StackPanel))]
    public sealed class ListViewButton : Control
    {
        public static readonly DependencyProperty HoverBrushProperty =
            DependencyProperty.Register("HoverBrush", typeof(Brush), typeof(ListViewItem), new PropertyMetadata(default));

        public static readonly DependencyProperty IconHeightProperty =
            DependencyProperty.Register("IconHeight", typeof(double), typeof(ListViewItem), new PropertyMetadata(default));

        public static readonly DependencyProperty IconWidthProperty =
            DependencyProperty.Register("IconWidth", typeof(double), typeof(ListViewItem), new PropertyMetadata(default));

        public static readonly DependencyProperty SymbolProperty =
            DependencyProperty.Register("Symbol", typeof(Symbol), typeof(ListViewItem), new PropertyMetadata(default));

        public static readonly DependencyProperty TextProperty =
                                            DependencyProperty.Register("Text", typeof(string), typeof(ListViewButton), new PropertyMetadata(default));

        private const string StackPanelPart = "PART_StackPanel";

        private StackPanel _stackPanel;

        public Brush HoverBrush
        {
            get => (Brush)GetValue(HoverBrushProperty);
            set => SetValue(HoverBrushProperty, value);
        }

        public double IconHeight
        {
            get => (double)GetValue(IconHeightProperty);
            set => SetValue(IconHeightProperty, value);
        }

        public double IconWidth
        {
            get => (double)GetValue(IconWidthProperty);
            set => SetValue(IconWidthProperty, value);
        }

        public Symbol Symbol
        {
            get => (Symbol)GetValue(SymbolProperty);
            set => SetValue(SymbolProperty, value);
        }

        public string Text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }

        // C'tor
        //
        public ListViewButton()
        {
            DefaultStyleKey = typeof(ListViewButton);
        }

        protected override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            _stackPanel = GetTemplateChild(StackPanelPart) as StackPanel;
        }

        protected override void OnPointerEntered(PointerRoutedEventArgs e)
        {
            base.OnPointerEntered(e);

            _stackPanel.Background = HoverBrush;
        }

        protected override void OnPointerExited(PointerRoutedEventArgs e)
        {
            base.OnPointerExited(e);

            if (_stackPanel.Background == Background)
            {
                return;
            }
            _stackPanel.Background = Background;
        }
    }
}
