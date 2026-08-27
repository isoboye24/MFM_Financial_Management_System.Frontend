using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace MFMFMSF.UI.Controls
{
    public partial class ProgressBarWithLabelAndAmount : UserControl
    {
        public ProgressBarWithLabelAndAmount()
        {
            InitializeComponent();
        }


        // =====================================================
        // LABEL
        // =====================================================

        public string Label
        {
            get => (string)GetValue(LabelProperty);
            set => SetValue(LabelProperty, value);
        }

        public static readonly DependencyProperty LabelProperty =
            DependencyProperty.Register(
                nameof(Label),
                typeof(string),
                typeof(ProgressBarWithLabelAndAmount),
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
                typeof(ProgressBarWithLabelAndAmount),
                new PropertyMetadata(string.Empty));


        // =====================================================
        // PERCENTAGE
        // =====================================================

        public double Percentage
        {
            get => (double)GetValue(PercentageProperty);
            set => SetValue(PercentageProperty, value);
        }

        public static readonly DependencyProperty PercentageProperty =
            DependencyProperty.Register(
                nameof(Percentage),
                typeof(double),
                typeof(ProgressBarWithLabelAndAmount),
                new PropertyMetadata(0.0));


        // =====================================================
        // BAR FOREGROUND
        // =====================================================

        public Brush BarForeground
        {
            get => (Brush)GetValue(BarForegroundProperty);
            set => SetValue(BarForegroundProperty, value);
        }

        public static readonly DependencyProperty BarForegroundProperty =
            DependencyProperty.Register(
                nameof(BarForeground),
                typeof(Brush),
                typeof(ProgressBarWithLabelAndAmount),
                new PropertyMetadata(Brushes.MediumPurple));
    }
}