namespace MFMFMSF.Core.Models.Expenditures
{
    public class ExpenditureListItem
    {
        public Guid Id { get; set; }
        public required DateOnly Date { get; set; }
        public required string Summary { get; set; }
        public required decimal Amount { get; set; }
    }
}
