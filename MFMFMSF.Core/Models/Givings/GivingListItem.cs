namespace MFMFMSF.Core.Models.Givings
{
    public class GivingListItem
    {
        public Guid Id { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public string Summary { get; set; } = string.Empty;
        public string CategoryName { get; set; } = string.Empty;
        public string MessageTitle { get; set; } = string.Empty;
    }
}
