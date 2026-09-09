using MFMFMSF.Core.Models;

namespace MFMFMSF.Core.Interfaces
{
    public interface IMeetingService
    {
        Task CreateAsync(CreateMeetingRequest request);
        Task<IReadOnlyList<MeetingListItem>> GetAllAsync();
    }
}
