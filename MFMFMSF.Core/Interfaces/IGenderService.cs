using MFMFMSF.Core.Models.Genders;
using MFMFMSF.Core.Models.Positions;

namespace MFMFMSF.Core.Interfaces
{
    public interface IGenderService
    {
        Task<IReadOnlyList<GenderListItem>> GetAllAsync();
    }
}
