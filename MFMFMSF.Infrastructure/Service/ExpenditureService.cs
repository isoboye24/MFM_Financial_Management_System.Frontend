using MFMFMSF.Core.Interfaces;
using MFMFMSF.Core.Models.Expenditures;
using MFMFMSF.Core.Models.Givings;
using MFMFMSF.Core.Models.Meetings;
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
            var statistics = await _httpClient.GetFromJsonAsync<List<ExpendituresByMonthAndYear>>($"{Endpoint}/monthly/statistics?month={month}&year={year}");

            return statistics ?? new List<ExpendituresByMonthAndYear>();
        }
    }
}
