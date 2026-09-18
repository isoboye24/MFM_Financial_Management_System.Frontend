using MFMFMSF.Core.Models.Givings;

namespace MFMFMSF.Core.Interfaces
{
    public interface IGivingService
    {
        Task CreateAsync(CreateGivingRequest request);
        Task<IReadOnlyList<GivingListItem>> GetByMeetingIdAsync(Guid meetingId);
        Task<GivingDetail> GetByIdAsync(Guid id);
        Task UpdateAsync(Guid id, UpdateGivingRequest request);
        Task DeleteAsync(Guid id);
        Task<MonthlyGivingStatistics> GetMonthlyStatisticsAsync(int month, int year);
        Task<AnnualGivingStatistics> GetAnnualStatisticsAsync(int year);
        Task<TotalGivingStatistics> GetTotalStatisticsAsync();
        Task<IReadOnlyList<GivingByMonthAndYear>> GetByMonthAndYearAsync(int month, int year, string? categoryName, int page, int recordsPerPage);
    }
}
