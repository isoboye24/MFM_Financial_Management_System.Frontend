using System.Windows;
using System.Windows.Controls;

namespace MFMFMSF.UI.Features.Expenditures.Controls.Form
{
    /// <summary>
    /// Interaction logic for ExpenditureForm.xaml
    /// </summary>
    public partial class ExpenditureForm : UserControl
    {
        public ExpenditureForm()
        {
            InitializeComponent();
        }



        // =====================================================
        // DATE
        // =====================================================

        public DateTime? ExpenditureDate
        {
            get => (DateTime?)GetValue(ExpenditureDateProperty);
            set => SetValue(ExpenditureDateProperty, value);
        }

        public static readonly DependencyProperty ExpenditureDateProperty =
            DependencyProperty.Register(
                nameof(ExpenditureDate),
                typeof(DateTime?),
                typeof(ExpenditureForm),
                new PropertyMetadata(null));



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
                typeof(ExpenditureForm),
                new PropertyMetadata(string.Empty));



        // =====================================================
        // SUMMARY
        // =====================================================

        public string Summary
        {
            get => (string)GetValue(SummaryProperty);
            set => SetValue(SummaryProperty, value);
        }

        public static readonly DependencyProperty SummaryProperty =
            DependencyProperty.Register(
                nameof(Summary),
                typeof(string),
                typeof(ExpenditureForm),
                new PropertyMetadata(string.Empty));
    }
}
