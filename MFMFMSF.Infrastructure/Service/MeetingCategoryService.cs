using MFMFMSF.Core.Interfaces;
using MFMFMSF.Core.Models;
using System.Net.Http;
using System.Net.Http.Json;

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

        // ==========================================
        // CREATE
        // ==========================================

        public async Task CreateAsync(string name)
        {
            var request = new CreateMeetingCategoryRequest
            {
                Name = name
            };

            var response = await _httpClient.PostAsJsonAsync(Endpoint, request);

            response.EnsureSuccessStatusCode();
        }

        // ==========================================
        // GET ALL
        // ==========================================

        public async Task<IReadOnlyList<MeetingCategoryListItem>> GetAllAsync()
        {
            var response = await _httpClient.GetAsync(
                $"{Endpoint}?Page=1&RecordsPerPage=100");

            response.EnsureSuccessStatusCode();

            var categories =
                await response.Content.ReadFromJsonAsync<
                    List<MeetingCategoryListItem>>();

            return categories ?? [];
        }
    }


    internal class CreateMeetingCategoryRequest
    {
        public string Name { get; set; } = string.Empty;
    }
}