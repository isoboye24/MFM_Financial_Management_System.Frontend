using MFMFMSF.Core.Interfaces;
using MFMFMSF.Core.Models;
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
    }
}