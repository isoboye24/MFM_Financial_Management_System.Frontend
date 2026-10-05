using MFMFMSF.Core.Interfaces;
using MFMFMSF.Core.Models.Reports.Create;
using System.Net.Http.Json;

namespace MFMFMSF.Infrastructure.Service
{
    public class ReportService : IReportService
    {
        private readonly HttpClient _httpClient;

        private const string Endpoint = "api/financial-summary-monthly-reports";

        public ReportService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task CreateAsync(CreateMonthlyReportRequest request)
        {
            var response = await _httpClient.PostAsJsonAsync(
                Endpoint,
                request);

            response.EnsureSuccessStatusCode();
        }
    }
}
