using MFMFMSF.Core.Models.Givings;

namespace MFMFMSF.Core.Interfaces
{
    public interface IGivingService
    {
        Task CreateAsync(CreateGivingRequest request);
        Task<IReadOnlyList<GivingListItem>> GetByMeetingIdAsync(Guid meetingId);
        Task<GivingDetail> GetByIdAsync(Guid id);
        Task UpdateAsync(Guid id, UpdateGivingRequest request);
    }
}
