using MFMFMSF.Core.Models.Meetings;

namespace MFMFMSF.Core.Interfaces
{
    public interface IMeetingService
    {
        Task CreateAsync(CreateMeetingRequest request);
        Task<IReadOnlyList<MeetingListItem>> GetAllAsync();
        Task<MeetingDetail> GetByIdAsync(Guid id);
        Task UpdateAsync(Guid id, UpdateMeetingRequest request);
        Task DeleteAsync(Guid id);

        Task<IReadOnlyList<MeetingsByMonthAndYear>> GetByMonthAndYearAsync(int month, int year, int page, int recordsPerPage);
    }
}
