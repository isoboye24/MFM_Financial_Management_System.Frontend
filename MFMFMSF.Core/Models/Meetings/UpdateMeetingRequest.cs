namespace MFMFMSF.Core.Models.Meetings
{
    public class UpdateMeetingRequest
    {
        public string MessageTitle { get; set; } = string.Empty;
        public DateTime Date { get; set; }
        public string? Summary { get; set; }
        public string Minister { get; set; } = string.Empty;
        public int NoOfMaleAttendance { get; set; }
        public int NoOfFemaleAttendance { get; set; }
        public int NoOfChildrenAttendance { get; set; }
        public Guid MeetingCategoryId { get; set; }
    }
}
