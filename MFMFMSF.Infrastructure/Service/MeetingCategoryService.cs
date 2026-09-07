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

        // ==========================================
        // GET BY ID
        // ==========================================

        public async Task<MeetingCategoryDetail> GetByIdAsync(Guid id)
        {
            var response =
                await _httpClient.GetAsync(
                    $"{Endpoint}/{id}");

            response.EnsureSuccessStatusCode();

            var category =
                await response.Content
                    .ReadFromJsonAsync<MeetingCategoryDetail>();

            if (category == null)
            {
                throw new InvalidOperationException(
                    "The meeting category could not be loaded.");
            }

            return category;
        }

        // ==========================================
        // UPDATE
        // ==========================================

        public async Task UpdateAsync(Guid id, string name)
        {
            var request = new UpdateMeetingCategoryRequest
            {
                Name = name
            };

            var response =
                await _httpClient.PutAsJsonAsync(
                    $"{Endpoint}/{id}",
                    request);

            response.EnsureSuccessStatusCode();
        }

        // ==========================================
        // DELETE
        // ==========================================

        public async Task DeleteAsync(Guid id)
        {
            var response =
                await _httpClient.DeleteAsync(
                    $"{Endpoint}/{id}");

            response.EnsureSuccessStatusCode();
        }
    }


    internal class CreateMeetingCategoryRequest
    {
        public string Name { get; set; } = string.Empty;
    }

    internal class UpdateMeetingCategoryRequest
    {
        public string Name { get; set; } = string.Empty;
    }
}