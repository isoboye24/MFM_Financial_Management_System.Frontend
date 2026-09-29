using System.Windows;
using System.Windows.Controls;

namespace MFMFMSF.UI.Features.Reports.Controls.Form
{
    /// <summary>
    /// Interaction logic for FinancialSummaryReportForm.xaml
    /// </summary>
    public partial class FinancialSummaryReportForm : UserControl
    {
        public FinancialSummaryReportForm()
        {
            InitializeComponent();
        }

        // =====================================================
        // DATE
        // =====================================================

        public DateTime? ReportDate
        {
            get => (DateTime?)GetValue(ReportDateProperty);
            set => SetValue(ReportDateProperty, value);
        }

        public static readonly DependencyProperty ReportDateProperty =
            DependencyProperty.Register(
                nameof(ReportDate),
                typeof(DateTime?),
                typeof(FinancialSummaryReportForm),
                new PropertyMetadata(null));


        // =====================================================
        // OPENING BALANCE
        // =====================================================

        public string OpeningBalance
        {
            get => (string)GetValue(OpeningBalanceProperty);
            set => SetValue(OpeningBalanceProperty, value);
        }

        public static readonly DependencyProperty OpeningBalanceProperty =
            DependencyProperty.Register(
                nameof(OpeningBalance),
                typeof(string),
                typeof(FinancialSummaryReportForm),
                new PropertyMetadata(string.Empty));
    }
}
