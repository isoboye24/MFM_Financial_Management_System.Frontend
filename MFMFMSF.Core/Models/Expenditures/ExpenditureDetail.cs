namespace MFMFMSF.Core.Models.Expenditures
{
    public class ExpenditureDetail
    {
        public Guid Id { get; set; }
        public required DateTime Date { get; set; }
        public required decimal Amount { get; set; }
        public required string Summary { get; set; }
    }
}
