using MFMFMSF.Core.Models.Reports;
using MFMFMSF.Core.Models.Reports.Create;

namespace MFMFMSF.Core.Interfaces
{
    public interface IReportService
    {
        Task CreateAsync(CreateMonthlyReportRequest request);
        Task<(IReadOnlyList<FinancialSummaryMonthlyReportListItem> Reports, int TotalItems)>GetAllAsync(int page, int recordsPerPage);
    }
}
