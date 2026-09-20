using MFMFMSF.Core.Models.Expenditures;

namespace MFMFMSF.Core.Interfaces
{
    public interface IExpenditureService
    {
        Task CreateAsync(CreateExpenditureRequest request);
        Task<IReadOnlyList<ExpenditureListItem>> GetAllAsync();
    }
}
