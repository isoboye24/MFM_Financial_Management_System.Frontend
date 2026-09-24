namespace MFMFMSF.Core.Models.Expenditures
{
    public class ExpendituresByMonthAndYear
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }
        public string Summary { get; set; } = string.Empty;
    }
}
