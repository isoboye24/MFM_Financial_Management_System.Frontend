namespace MFMFMSF.Core.Models.Workers
{
    public class WorkerDetail
    {
        public Guid Id { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public string Address { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string PositionName { get; set; } = string.Empty;
        public string GenderName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public DateOnly CreatedAt { get; set; }
    }
}
