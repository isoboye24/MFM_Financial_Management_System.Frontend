namespace MFMFMSF.Core.Models.Offerings
{
    public class OfferingListItem
    {
        public Guid Id { get; set; }
        public string MessageTitle { get; set; } = string.Empty;
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }
        public string Minister { get; set; } = string.Empty;
        public string CategoryName { get; set; } = string.Empty;
    }
}
