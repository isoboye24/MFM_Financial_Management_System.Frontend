namespace MFMFMSF.Core.Models.Expenditures
{
    public class CreateExpenditureRequest
    {
        public required DateTime Date { get; set; }
        public required decimal Amount { get; set; }
        public required string Summary { get; set; }
    }
}
