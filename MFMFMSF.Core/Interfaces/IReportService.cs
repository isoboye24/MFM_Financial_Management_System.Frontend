using MFMFMSF.Core.Models.Reports.Create;

namespace MFMFMSF.Core.Interfaces
{
    public interface IReportService
    {
        Task CreateAsync(CreateMonthlyReportRequest request);
    }
}
