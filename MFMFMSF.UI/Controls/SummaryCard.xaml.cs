using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using MaterialDesignThemes.Wpf;

namespace MFMFMSF.UI.Controls
{
    public partial class SummaryCard : UserControl
    {
        public SummaryCard()
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
                typeof(SummaryCard),
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
                typeof(SummaryCard),
                new PropertyMetadata(string.Empty));


        // =====================================================
        // PERCENTAGE
        // =====================================================

        public string Percentage
        {
            get => (string)GetValue(PercentageProperty);
            set => SetValue(PercentageProperty, value);
        }

        public static readonly DependencyProperty PercentageProperty =
            DependencyProperty.Register(
                nameof(Percentage),
                typeof(string),
                typeof(SummaryCard),
                new PropertyMetadata(string.Empty));


        // =====================================================
        // COMPARISON
        // =====================================================

        public string Comparison
        {
            get => (string)GetValue(ComparisonProperty);
            set => SetValue(ComparisonProperty, value);
        }

        public static readonly DependencyProperty ComparisonProperty =
            DependencyProperty.Register(
                nameof(Comparison),
                typeof(string),
                typeof(SummaryCard),
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
                typeof(SummaryCard),
                new PropertyMetadata(PackIconKind.None));


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
                typeof(SummaryCard),
                new PropertyMetadata(null));


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
                typeof(SummaryCard),
                new PropertyMetadata(null));


        // =====================================================
        // TREND ARROW
        // =====================================================

        public string TrendArrow
        {
            get => (string)GetValue(TrendArrowProperty);
            set => SetValue(TrendArrowProperty, value);
        }

        public static readonly DependencyProperty TrendArrowProperty =
            DependencyProperty.Register(
                nameof(TrendArrow),
                typeof(string),
                typeof(SummaryCard),
                new PropertyMetadata("↑"));


        // =====================================================
        // TREND FOREGROUND
        // =====================================================

        public Brush TrendForeground
        {
            get => (Brush)GetValue(TrendForegroundProperty);
            set => SetValue(TrendForegroundProperty, value);
        }

        public static readonly DependencyProperty TrendForegroundProperty =
            DependencyProperty.Register(
                nameof(TrendForeground),
                typeof(Brush),
                typeof(SummaryCard),
                new PropertyMetadata(
                    new SolidColorBrush(Color.FromRgb(13, 170, 104))));
    }
}