using MFMFMSF.Core.Models;

namespace MFMFMSF.Core.Interfaces
{
    public interface IMeetingCategoryService
    {
        Task CreateAsync(string name);
        Task<IReadOnlyList<MeetingCategoryListItem>> GetAllAsync();
        Task<MeetingCategoryDetail> GetByIdAsync(Guid id);
    }
}
