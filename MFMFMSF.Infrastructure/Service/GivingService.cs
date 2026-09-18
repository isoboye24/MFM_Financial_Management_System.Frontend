using MFMFMSF.Core.Interfaces;
using MFMFMSF.Core.Models.Givings;
using System.Net.Http.Json;

namespace MFMFMSF.Infrastructure.Service
{
    public class GivingService : IGivingService
    {
        private readonly HttpClient _httpClient;

        private const string Endpoint = "api/givings";

        public GivingService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task CreateAsync(CreateGivingRequest request)
        {
            var response = await _httpClient.PostAsJsonAsync(Endpoint, request);
            
            response.EnsureSuccessStatusCode();
        }


        public async Task<IReadOnlyList<GivingListItem>> GetByMeetingIdAsync(Guid meetingId)
        {
            var givings = await _httpClient.GetFromJsonAsync<List<GivingListItem>>($"{Endpoint}?Page=1&RecordsPerPage=100&MeetingId={meetingId}");

            return givings ?? [];
        }

        public async Task<GivingDetail> GetByIdAsync(Guid id)
        {
            var giving = await _httpClient.GetFromJsonAsync<GivingDetail>($"{Endpoint}/{id}");

            return giving ?? throw new InvalidOperationException("Giving not found.");
        }

        public async Task UpdateAsync(Guid id, UpdateGivingRequest request)
        {
            var response = await _httpClient.PutAsJsonAsync($"{Endpoint}/{id}", request);

            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteAsync(Guid id)
        {
            var response = await _httpClient.DeleteAsync($"{Endpoint}/{id}");

            response.EnsureSuccessStatusCode();
        }

        public async Task<MonthlyGivingStatistics> GetMonthlyStatisticsAsync(int month, int year)
        {
            var statistics = await _httpClient.GetFromJsonAsync<MonthlyGivingStatistics>($"{Endpoint}/monthly/statistics?month={month}&year={year}");

            return statistics ?? new MonthlyGivingStatistics();
        }

        public async Task<AnnualGivingStatistics> GetAnnualStatisticsAsync(int year)
        {
            var statistics = await _httpClient.GetFromJsonAsync<AnnualGivingStatistics>($"{Endpoint}/annual/statistics?year={year}");

            return statistics ?? new AnnualGivingStatistics();
        }

        public async Task<TotalGivingStatistics> GetTotalStatisticsAsync()
        {
            var statistics = await _httpClient.GetFromJsonAsync<TotalGivingStatistics>($"{Endpoint}/total/statistics");

            return statistics ?? new TotalGivingStatistics();
        }

        public async Task<IReadOnlyList<GivingByMonthAndYear>> GetByMonthAndYearAsync(int month, int year, string? categoryName, int page, int recordsPerPage)
        {
            var url = $"{Endpoint}/by-month-year" + $"?month={month}" + $"&year={year}" + $"&page={page}" + $"&recordsPerPage={recordsPerPage}";

            if (!string.IsNullOrWhiteSpace(categoryName))
            {
                url += $"&categoryName={Uri.EscapeDataString(categoryName)}";
            }

            var givings = await _httpClient.GetFromJsonAsync<List<GivingByMonthAndYear>>(url);

            return givings ?? [];
        }
    }
}
