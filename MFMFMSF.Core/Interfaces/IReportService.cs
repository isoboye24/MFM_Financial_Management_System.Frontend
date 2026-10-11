using MFMFMSF.Core.Models.Reports;
using MFMFMSF.Core.Models.Reports.Create;
using MFMFMSF.Core.Models.Reports.PDF;

namespace MFMFMSF.Core.Interfaces
{
    public interface IReportService
    {
        Task CreateAsync(CreateMonthlyReportRequest request);
        Task DeleteAsync(Guid id);
        Task<(IReadOnlyList<FinancialSummaryMonthlyReportListItem> Reports, int TotalItems)>GetAllAsync(int page, int recordsPerPage);

        Task<FinancialSummaryMonthlyReportPDF> GetPDFDataAsync(Guid id);
        Task<byte[]> GetPDFAsync(Guid id);
    }
}
