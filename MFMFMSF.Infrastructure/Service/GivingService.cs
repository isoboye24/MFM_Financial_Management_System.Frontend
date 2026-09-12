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


        public async Task<IReadOnlyList<GivingListItem>> GetByMeetingIdAsync(
            Guid meetingId)
        {
            var givings = await _httpClient.GetFromJsonAsync<List<GivingListItem>>($"{Endpoint}?Page=1&RecordsPerPage=100&MeetingId={meetingId}");

            return givings ?? [];
        }
    }
}
