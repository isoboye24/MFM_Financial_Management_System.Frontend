using MFMFMSF.Core.Models.MeetingCategories;

namespace MFMFMSF.Core.Interfaces
{
    public interface IMeetingCategoryService
    {
        Task CreateAsync(string name);
        Task<IReadOnlyList<MeetingCategoryListItem>> GetAllAsync();
        Task<MeetingCategoryDetail> GetByIdAsync(Guid id);
        Task UpdateAsync(Guid id, string name);
        Task DeleteAsync(Guid id);
    }
}
