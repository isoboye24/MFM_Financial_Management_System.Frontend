using MFMFMSF.Core.Interfaces;
using MFMFMSF.Core.Models.Reports;
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

        public async Task<(IReadOnlyList<FinancialSummaryMonthlyReportListItem> Reports, int TotalItems)> GetAllAsync(int page, int recordsPerPage)
        {
            var response = await _httpClient.GetAsync($"{Endpoint}?page={page}&recordsPerPage={recordsPerPage}");

            response.EnsureSuccessStatusCode();

            var reports = await response.Content.ReadFromJsonAsync<List<FinancialSummaryMonthlyReportListItem>>() ?? new List<FinancialSummaryMonthlyReportListItem>();

            const string headerName = "Total-amount-of-records";

            if (!response.Headers.TryGetValues(headerName, out var headerValues) || !int.TryParse(headerValues.FirstOrDefault(), out var totalItems))
            {
                throw new InvalidOperationException($"The API response does not contain a valid '{headerName}' header.");
            }

            return (reports, totalItems);
        }
    }
}
