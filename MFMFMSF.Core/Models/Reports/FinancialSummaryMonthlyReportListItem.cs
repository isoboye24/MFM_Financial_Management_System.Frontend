namespace MFMFMSF.Core.Models.Reports
{
    public class FinancialSummaryMonthlyReportListItem
    {
        public Guid Id { get; set; }
        public int Month { get; set; }
        public string MonthName => new DateTime(Year, Month, 1).ToString("MMMM");
        public int Year { get; set; }
        public decimal OpeningBalance { get; set; }
        public decimal TotalIncome { get; set; }
        public decimal TotalExpenditure { get; set; }
        public decimal ClosingBalance { get; set; }
    }
}
