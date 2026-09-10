using MFMFMSF.Core.Interfaces;
using MFMFMSF.Core.Models.Meetings;
using System.Net.Http;
using System.Net.Http.Json;

namespace MFMFMSF.Infrastructure.Service
{
    public class MeetingService : IMeetingService
    {
        private readonly HttpClient _httpClient;

        private const string Endpoint = "api/meetings";

        public MeetingService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task CreateAsync(CreateMeetingRequest request)
        {
            var response = await _httpClient.PostAsJsonAsync(
                Endpoint,
                request);

            response.EnsureSuccessStatusCode();
        }

        public async Task<IReadOnlyList<MeetingListItem>> GetAllAsync()
        {
            var meetings = await _httpClient.GetFromJsonAsync<
                List<MeetingListItem>>(
                    $"{Endpoint}?Page=1&RecordsPerPage=100");

            return meetings ?? [];
        }

        public async Task<MeetingDetail> GetByIdAsync(Guid id)
        {
            var meeting = await _httpClient.GetFromJsonAsync<MeetingDetail>(
                $"{Endpoint}/{id}");

            return meeting
                ?? throw new InvalidOperationException("Meeting not found.");
        }

        public async Task UpdateAsync(Guid id, UpdateMeetingRequest request)
        {
            var response = await _httpClient.PutAsJsonAsync(
                $"{Endpoint}/{id}",
                request);

            response.EnsureSuccessStatusCode();
        }
    }
}