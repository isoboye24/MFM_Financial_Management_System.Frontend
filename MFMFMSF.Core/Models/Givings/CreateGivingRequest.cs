namespace MFMFMSF.Core.Models.Givings
{
    public class CreateGivingRequest
    {
        public required decimal Amount { get; set; }
        public required DateTime Date { get; set; }
        public required string Summary { get; set; }
        public required Guid MeetingId { get; set; }
        public required Guid GivingCategoryId { get; set; }
    }
}
