using MFMFMSF.Core.Models.Positions;

namespace MFMFMSF.Core.Interfaces
{
    public interface IPositionService
    {
        Task CreateAsync(CreatePositionRequest request);
        Task<IReadOnlyList<PositionListItem>> GetAllAsync();
        Task<PositionDetail> GetByIdAsync(Guid id);
        //Task UpdateAsync(Guid id, UpdatePositionRequest request);
        //Task DeleteAsync(Guid id);
    }
}
