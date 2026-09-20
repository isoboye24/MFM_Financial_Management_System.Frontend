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
    }
}
