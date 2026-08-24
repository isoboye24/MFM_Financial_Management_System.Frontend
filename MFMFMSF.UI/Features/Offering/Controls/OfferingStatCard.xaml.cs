using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using MaterialDesignThemes.Wpf;

namespace MFMFMSF.UI.Features.Offering.Controls
{
    public partial class OfferingStatCard : UserControl
    {
        public OfferingStatCard()
        {
            InitializeComponent();
            DataContext = this;
        }


        public string Title
        {
            get => (string)GetValue(TitleProperty);
            set => SetValue(TitleProperty, value);
        }

        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register(
                nameof(Title),
                typeof(string),
                typeof(OfferingStatCard));


        public string Amount
        {
            get => (string)GetValue(AmountProperty);
            set => SetValue(AmountProperty, value);
        }

        public static readonly DependencyProperty AmountProperty =
            DependencyProperty.Register(
                nameof(Amount),
                typeof(string),
                typeof(OfferingStatCard));


        public string Percentage
        {
            get => (string)GetValue(PercentageProperty);
            set => SetValue(PercentageProperty, value);
        }

        public static readonly DependencyProperty PercentageProperty =
            DependencyProperty.Register(
                nameof(Percentage),
                typeof(string),
                typeof(OfferingStatCard));


        public string Comparison
        {
            get => (string)GetValue(ComparisonProperty);
            set => SetValue(ComparisonProperty, value);
        }

        public static readonly DependencyProperty ComparisonProperty =
            DependencyProperty.Register(
                nameof(Comparison),
                typeof(string),
                typeof(OfferingStatCard));


        public PackIconKind Icon
        {
            get => (PackIconKind)GetValue(IconProperty);
            set => SetValue(IconProperty, value);
        }

        public static readonly DependencyProperty IconProperty =
            DependencyProperty.Register(
                nameof(Icon),
                typeof(PackIconKind),
                typeof(OfferingStatCard));


        public Brush IconBackground
        {
            get => (Brush)GetValue(IconBackgroundProperty);
            set => SetValue(IconBackgroundProperty, value);
        }

        public static readonly DependencyProperty IconBackgroundProperty =
            DependencyProperty.Register(
                nameof(IconBackground),
                typeof(Brush),
                typeof(OfferingStatCard));


        public Brush IconForeground
        {
            get => (Brush)GetValue(IconForegroundProperty);
            set => SetValue(IconForegroundProperty, value);
        }

        public static readonly DependencyProperty IconForegroundProperty =
            DependencyProperty.Register(
                nameof(IconForeground),
                typeof(Brush),
                typeof(OfferingStatCard));
    }
}