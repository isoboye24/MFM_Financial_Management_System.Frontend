using MFMFMSF.Core.Interfaces;
using MFMFMSF.Core.Models.Genders;
using System.Net.Http.Json;

namespace MFMFMSF.Infrastructure.Service
{
    public class GenderService : IGenderService
    {
        private readonly HttpClient _httpClient;

        private const string Endpoint = "api/genders";

        public GenderService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IReadOnlyList<GenderListItem>> GetAllAsync()
        {
            var response = await _httpClient.GetAsync($"{Endpoint}?Page=1&RecordsPerPage=100");

            response.EnsureSuccessStatusCode();

            var genders = await response.Content.ReadFromJsonAsync<List<GenderListItem>>();

            return genders ?? [];
        }
    }
}
