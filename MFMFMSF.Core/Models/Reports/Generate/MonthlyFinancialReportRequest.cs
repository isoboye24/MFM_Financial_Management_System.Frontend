using MFMFMSF.Core.Models.Reports.Generate.RequestUnits;

namespace MFMFMSF.Core.Models.Reports.Generate
{
    public class MonthlyFinancialReportRequest
    {
        public int Year { get; set; }
        public int Month { get; set; }

        public decimal OpeningBalance { get; set; }

        public decimal TotalIncome { get; set; }
        public decimal TotalExpenditure { get; set; }

        public decimal ClosingBalance { get; set; }

        public List<ServiceReportRow> Services { get; set; } = new();

        public List<ExpenditureReportRow> Expenditures { get; set; } = new();
    }
}
