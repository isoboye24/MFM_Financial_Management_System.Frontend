using MFMFMSF.UI.Controls;
using System.Windows;
using System.Windows.Controls;

namespace MFMFMSF.UI.Features.Reports.Controls.Form
{
    public partial class FinancialSummaryReportForm : UserControl
    {
        public FinancialSummaryReportForm()
        {
            InitializeComponent();

            MonthYearPicker.PeriodChanged += MonthYearPicker_PeriodChanged;
        }


        // =====================================================
        // SELECTED MONTH
        // =====================================================

        public int SelectedMonth
        {
            get => (int)GetValue(SelectedMonthProperty);
            set => SetValue(SelectedMonthProperty, value);
        }

        public static readonly DependencyProperty SelectedMonthProperty =
            DependencyProperty.Register(
                nameof(SelectedMonth),
                typeof(int),
                typeof(FinancialSummaryReportForm),
                new PropertyMetadata(0));


        // =====================================================
        // SELECTED YEAR
        // =====================================================

        public int SelectedYear
        {
            get => (int)GetValue(SelectedYearProperty);
            set => SetValue(SelectedYearProperty, value);
        }

        public static readonly DependencyProperty SelectedYearProperty =
            DependencyProperty.Register(
                nameof(SelectedYear),
                typeof(int),
                typeof(FinancialSummaryReportForm),
                new PropertyMetadata(0));


        // =====================================================
        // OPENING BALANCE
        // =====================================================

        public decimal? OpeningBalance
        {
            get => (decimal?)GetValue(OpeningBalanceProperty);
            set => SetValue(OpeningBalanceProperty, value);
        }

        public static readonly DependencyProperty OpeningBalanceProperty =
            DependencyProperty.Register(
                nameof(OpeningBalance),
                typeof(decimal?),
                typeof(FinancialSummaryReportForm),
                new PropertyMetadata(null));


        // =====================================================
        // PERIOD CHANGED
        // =====================================================

        private void MonthYearPicker_PeriodChanged(
            object? sender,
            PeriodChangedEventArgs e)
        {
            SelectedMonth = e.Month;
            SelectedYear = e.Year;

            PeriodChanged?.Invoke(this, e);
        }


        public event EventHandler<PeriodChangedEventArgs>? PeriodChanged;
    }
}