using MFMFMSF.Core.Models.Reports.Create;

namespace MFMFMSF.Core.Interfaces
{
    public interface IReportService
    {
        Task<Guid> CreateAsync(CreateMonthlyReportRequest request);
    }
}
