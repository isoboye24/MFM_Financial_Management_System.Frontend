namespace MFMFMSF.Core.Models.Reports.Generate.RequestUnits
{
    public class ServiceReportRow
    {
        public string Day { get; set; } = string.Empty;
        public DateTime Date { get; set; }

        public string MessageTitle { get; set; } = string.Empty;
        public string Minister { get; set; } = string.Empty;

        public int MaleAttendance { get; set; }
        public int FemaleAttendance { get; set; }
        public int ChildrenAttendance { get; set; }
        public int TotalAttendance { get; set; }

        public decimal Offering { get; set; }
        public decimal Tithe { get; set; }
        public decimal OtherIncome { get; set; }
        public decimal TotalIncome { get; set; }
    }
}
