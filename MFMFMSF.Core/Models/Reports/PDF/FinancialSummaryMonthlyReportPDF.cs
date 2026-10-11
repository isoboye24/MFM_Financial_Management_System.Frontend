namespace MFMFMSF.Core.Models.Reports.PDF
{
    public class FinancialSummaryMonthlyReportPDF
    {
        public Guid Id { get; set; }

        public int Year { get; set; }

        public int Month { get; set; }

        public decimal OpeningBalance { get; set; }

        public decimal TotalIncome { get; set; }

        public decimal TotalExpenditure { get; set; }

        public decimal ClosingBalance { get; set; }

        public List<FinancialSummaryMonthlyReportServicePDF> Services {get; set; } = new();

        public List<FinancialSummaryMonthlyReportExpenditurePDF> Expenditures {get; set; } = new();

        public string MonthName => new DateTime(Year, Month, 1).ToString("MMMM yyyy");
    }
}
