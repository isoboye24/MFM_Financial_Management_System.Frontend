using MFMFMSF.Core.Interfaces;
using MFMFMSF.Core.Models.Workers;
using MFMFMSF.Core.Models.WorkersNotes;
using System.Net.Http.Json;

namespace MFMFMSF.Infrastructure.Service
{
    public class WorkerNoteService : IWorkerNoteService
    {
        private readonly HttpClient _httpClient;

        private const string Endpoint = "api/member-notes";

        public WorkerNoteService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task CreateAsync(CreateWorkerNoteRequest request)
        {
            var response = await _httpClient.PostAsJsonAsync(Endpoint, request);

            var responseContent = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException(
                    $"Failed to create worker note.\n" +
                    $"Status: {(int)response.StatusCode} ({response.StatusCode})\n" +
                    $"Response: {responseContent}");
            }
        }

        public async Task<IReadOnlyList<WorkerNoteByWorkerIdListItem>> GetAllByWorkerIdAsync(Guid memberId)
        {
            var notes = await _httpClient.GetFromJsonAsync<List<WorkerNoteByWorkerIdListItem>>($"{Endpoint}/{memberId}/by-member-id");

            return notes ?? [];
        }
    }
}
