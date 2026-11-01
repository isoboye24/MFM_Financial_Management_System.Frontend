using MFMFMSF.Core.Interfaces;
using MFMFMSF.Core.Models.Workers;
using System.Net.Http.Json;

namespace MFMFMSF.Infrastructure.Service
{
    public class WorkerService : IWorkerService
    {
        private readonly HttpClient _httpClient;

        private const string Endpoint = "api/members";

        public WorkerService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task CreateAsync(CreateWorkerRequest request)
        {
            var response = await _httpClient.PostAsJsonAsync(Endpoint, request);

            response.EnsureSuccessStatusCode();
        }

        public async Task<IReadOnlyList<WorkersListItem>> GetAllAsync()
        {
            var workers = await _httpClient.GetFromJsonAsync<List<WorkersListItem>>($"{Endpoint}?Page=1&RecordsPerPage=100");

            return workers ?? [];
        }
    }
}
