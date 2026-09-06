using System.Net.Http;
using System.Net.Http.Json;
using MFMFMSF.Core.Interfaces;

namespace MFMFMSF.Infrastructure.Service
{
    public class MeetingCategoryService : IMeetingCategoryService
    {
        private readonly HttpClient _httpClient;

        private const string Endpoint = "api/meeting-categories";

        public MeetingCategoryService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task CreateAsync(string name)
        {
            var request = new CreateMeetingCategoryRequest
            {
                Name = name
            };

            var response = await _httpClient.PostAsJsonAsync(Endpoint, request);

            response.EnsureSuccessStatusCode();
        }
    }


    internal class CreateMeetingCategoryRequest
    {
        public string Name { get; set; } = string.Empty;
    }
}