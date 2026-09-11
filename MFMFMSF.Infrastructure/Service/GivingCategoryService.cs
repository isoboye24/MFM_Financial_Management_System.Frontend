using MFMFMSF.Core.Interfaces;
using System.Net.Http.Json;

namespace MFMFMSF.Infrastructure.Service
{
    public class GivingCategoryService : IGivingCategoryService
    {
        private readonly HttpClient _httpClient;

        private const string Endpoint = "api/givings";

        public GivingCategoryService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // ==========================================
        // CREATE
        // ==========================================
        public async Task CreateAsync(string name)
        {
            var request = new CreateGivingCategoryRequest
            {
                Name = name
            };

            var response = await _httpClient.PostAsJsonAsync(Endpoint, request);

            response.EnsureSuccessStatusCode();
        }
    }

    internal class CreateGivingCategoryRequest
    {
        public string Name { get; set; } = string.Empty;
    }
}
