namespace MFMFMSF.Core.Models
{
    public class PaginatedResponse<T>
    {
        public List<T> Items { get; set; } = [];
        public int TotalAmountOfRecords { get; set; }
    }
}
