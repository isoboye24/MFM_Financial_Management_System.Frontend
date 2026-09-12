using MaterialDesignThemes.Wpf;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace MFMFMSF.UI.Controls
{
    public partial class PlainSideCard : UserControl
    {
        public PlainSideCard()
        {
            InitializeComponent();
        }


        // =====================================================
        // TITLE
        // =====================================================

        public string Title
        {
            get => (string)GetValue(TitleProperty);
            set => SetValue(TitleProperty, value);
        }

        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register(
                nameof(Title),
                typeof(string),
                typeof(PlainSideCard),
                new PropertyMetadata(string.Empty));


        // =====================================================
        // AMOUNT
        // =====================================================

        public string Amount
        {
            get => (string)GetValue(AmountProperty);
            set => SetValue(AmountProperty, value);
        }

        public static readonly DependencyProperty AmountProperty =
            DependencyProperty.Register(
                nameof(Amount),
                typeof(string),
                typeof(PlainSideCard),
                new PropertyMetadata(string.Empty));


        // =====================================================
        // ICON
        // =====================================================

        public PackIconKind Icon
        {
            get => (PackIconKind)GetValue(IconProperty);
            set => SetValue(IconProperty, value);
        }

        public static readonly DependencyProperty IconProperty =
            DependencyProperty.Register(
                nameof(Icon),
                typeof(PackIconKind),
                typeof(PlainSideCard),
                new PropertyMetadata(PackIconKind.Account));


        // =====================================================
        // ICON BACKGROUND
        // =====================================================

        public Brush IconBackground
        {
            get => (Brush)GetValue(IconBackgroundProperty);
            set => SetValue(IconBackgroundProperty, value);
        }

        public static readonly DependencyProperty IconBackgroundProperty =
            DependencyProperty.Register(
                nameof(IconBackground),
                typeof(Brush),
                typeof(PlainSideCard),
                new PropertyMetadata(
                    new SolidColorBrush(Color.FromRgb(238, 231, 255))));


        // =====================================================
        // ICON FOREGROUND
        // =====================================================

        public Brush IconForeground
        {
            get => (Brush)GetValue(IconForegroundProperty);
            set => SetValue(IconForegroundProperty, value);
        }

        public static readonly DependencyProperty IconForegroundProperty =
            DependencyProperty.Register(
                nameof(IconForeground),
                typeof(Brush),
                typeof(PlainSideCard),
                new PropertyMetadata(
                    new SolidColorBrush(Color.FromRgb(105, 65, 232))));
    }
}