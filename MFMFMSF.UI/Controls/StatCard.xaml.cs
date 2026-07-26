using MaterialDesignThemes.Wpf;
using System.Windows;
using System.Windows.Controls;

namespace MFMFMSF.UI.Controls
{
    /// <summary>
    /// Interaction logic for StatCard.xaml
    /// </summary>
    public partial class StatCard : UserControl
    {
        public StatCard()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty TitleProperty =
        DependencyProperty.Register(nameof(Title), typeof(string),
            typeof(StatCard));

        public string Title
        {
            get => (string)GetValue(TitleProperty);
            set => SetValue(TitleProperty, value);
        }

        public static readonly DependencyProperty AmountProperty =
            DependencyProperty.Register(nameof(Amount), typeof(string),
                typeof(StatCard));

        public string Amount
        {
            get => (string)GetValue(AmountProperty);
            set => SetValue(AmountProperty, value);
        }

        public static readonly DependencyProperty PercentageProperty =
            DependencyProperty.Register(nameof(Percentage), typeof(string),
                typeof(StatCard));

        public string Percentage
        {
            get => (string)GetValue(PercentageProperty);
            set => SetValue(PercentageProperty, value);
        }

        public static readonly DependencyProperty ComparisonTextProperty =
            DependencyProperty.Register(nameof(ComparisonText), typeof(string),
                typeof(StatCard));

        public string ComparisonText
        {
            get => (string)GetValue(ComparisonTextProperty);
            set => SetValue(ComparisonTextProperty, value);
        }

        public static readonly DependencyProperty IconKindProperty =
            DependencyProperty.Register(nameof(IconKind),
                typeof(PackIconKind),
                typeof(StatCard),
                new PropertyMetadata(PackIconKind.Bank));

        public PackIconKind IconKind
        {
            get => (PackIconKind)GetValue(IconKindProperty);
            set => SetValue(IconKindProperty, value);
        }
    }
}
