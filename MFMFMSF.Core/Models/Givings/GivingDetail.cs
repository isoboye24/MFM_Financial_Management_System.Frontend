namespace MFMFMSF.Core.Models.Givings
{
    public class GivingDetail
    {
        public required Guid Id { get; set; }
        public required decimal Amount { get; set; }
        public required DateTime Date { get; set; }
        public required string Summary { get; set; }
        public Guid CategoryId { get; set; }
        public Guid MeetingId { get; set; }
        public required string CategoryName { get; set; }
        public required string MessageTitle { get; set; }
        public required string Minister { get; set; }
        public int NoOfMaleAttendance { get; set; }
        public int NoOfFemaleAttendance { get; set; }
        public int NoOfChildrenAttendance { get; set; }
        public int TotalAttendance => NoOfMaleAttendance + NoOfFemaleAttendance + NoOfChildrenAttendance;
    }
}
