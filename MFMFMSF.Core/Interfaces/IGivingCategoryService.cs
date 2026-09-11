using MFMFMSF.Core.Models.GivingCategories;

namespace MFMFMSF.Core.Interfaces
{
    public interface IGivingCategoryService
    {
        Task CreateAsync(string name);
        Task<IReadOnlyList<GivingCategoryListItem>> GetAllAsync();
    }
}
