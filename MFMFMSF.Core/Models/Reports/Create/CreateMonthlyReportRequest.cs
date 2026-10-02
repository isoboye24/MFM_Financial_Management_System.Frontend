namespace MFMFMSF.Core.Models.Reports.Create
{
    public class CreateMonthlyReportRequest
    {
        public int Year { get; set; }
        public int Month { get; set; }

        public decimal OpeningBalance { get; set; }
    }
}
