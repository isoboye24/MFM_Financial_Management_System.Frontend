using MFMFMSF.Core.Interfaces;
using MFMFMSF.Core.Models.Expenditures;
using System.Net.Http.Json;

namespace MFMFMSF.Infrastructure.Service
{
    public class ExpenditureService : IExpenditureService
    {
        private readonly HttpClient _httpClient;

        private const string Endpoint = "api/expenditures";

        public ExpenditureService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task CreateAsync(CreateExpenditureRequest request)
        {
            var response = await _httpClient.PostAsJsonAsync(Endpoint, request);

            response.EnsureSuccessStatusCode();
        }
    }
}
