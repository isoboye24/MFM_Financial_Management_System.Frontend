using MFMFMSF.Core.Interfaces;
using MFMFMSF.Core.Models.Positions;
using System.Net.Http.Json;

namespace MFMFMSF.Infrastructure.Service
{
    public class PositionService : IPositionService
    {
        private readonly HttpClient _httpClient;

        private const string Endpoint = "api/positions";

        public PositionService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task CreateAsync(CreatePositionRequest request)
        {
            var response = await _httpClient.PostAsJsonAsync(Endpoint, request);
            response.EnsureSuccessStatusCode();
        }

        public async Task<IReadOnlyList<PositionListItem>> GetAllAsync()
        {
            var response = await _httpClient.GetAsync($"{Endpoint}?Page=1&RecordsPerPage=100");

            response.EnsureSuccessStatusCode();

            var positions = await response.Content.ReadFromJsonAsync<List<PositionListItem>>();

            return positions ?? [];
        }

        public async Task<PositionDetail> GetByIdAsync(Guid id)
        {
            var position = await _httpClient.GetFromJsonAsync<PositionDetail>($"{Endpoint}/{id}");

            return position ?? throw new InvalidOperationException("Position not found.");
        }

        public async Task UpdateAsync(Guid id, UpdatePositionRequest request)
        {
            var response = await _httpClient.PutAsJsonAsync($"{Endpoint}/{id}", request);

            response.EnsureSuccessStatusCode();
        }
    }
}
