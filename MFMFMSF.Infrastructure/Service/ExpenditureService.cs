using MFMFMSF.Core.Interfaces;
using MFMFMSF.Core.Models.Expenditures;
using System.Net.Http.Json;

namespace MFMFMSF.Infrastructure.Service
{
    public class ExpenditureService : IExpenditureService
    {
        private readonly HttpClient _httpClient;

        private const string Endpoint = "api/expenditures";

        public ExpenditureService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task CreateAsync(CreateExpenditureRequest request)
        {
            var response = await _httpClient.PostAsJsonAsync(Endpoint, request);

            response.EnsureSuccessStatusCode();
        }

        public async Task<IReadOnlyList<ExpenditureListItem>> GetAllAsync()
        {
            var expenditures = await _httpClient.GetFromJsonAsync<List<ExpenditureListItem>>($"{Endpoint}?Page=1&RecordsPerPage=100");

            return expenditures ?? [];
        }

        public async Task<IReadOnlyList<ExpendituresByMonthAndYear>> GetByMonthAndYearAsync(int month, int year, int page, int recordsPerPage)
        {
            var url = $"{Endpoint}/by-month-year" + $"?month={month}" + $"&year={year}" + $"&page={page}" + $"&recordsPerPage={recordsPerPage}";

            var statistics = await _httpClient.GetFromJsonAsync<List<ExpendituresByMonthAndYear>>(url);

            return statistics ?? new List<ExpendituresByMonthAndYear>();
        }

        public async Task<MonthlyExpendituresSatistics> GetMonthlyStatisticsAsync(int month, int year)
        {
            var statistics = await _httpClient.GetFromJsonAsync<MonthlyExpendituresSatistics>($"{Endpoint}/monthly/statistics?month={month}&year={year}");

            return statistics ?? new MonthlyExpendituresSatistics();
        }

        public async Task UpdateAsync(Guid id, UpdateExpenditureRequest request)
        {
            var response = await _httpClient.PutAsJsonAsync($"{Endpoint}/{id}", request);

            response.EnsureSuccessStatusCode();
        }

        public async Task<ExpenditureDetail> GetByIdAsync(Guid id)
        {
            var giving = await _httpClient.GetFromJsonAsync<ExpenditureDetail>($"{Endpoint}/{id}");

            return giving ?? throw new InvalidOperationException("Giving not found.");
        }

        public async Task DeleteAsync(Guid id)
        {
            var response = await _httpClient.DeleteAsync($"{Endpoint}/{id}");

            response.EnsureSuccessStatusCode();
        }
    }
}
