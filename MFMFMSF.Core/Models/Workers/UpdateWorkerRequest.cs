namespace MFMFMSF.Core.Models.Workers
{
    public class UpdateWorkerRequest
    {
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public string? Address { get; set; }
        public string? PhoneNumber { get; set; }
        public Guid PositionId { get; set; }
        public Guid GenderId { get; set; }
        public string? Email { get; set; }
    }
}
