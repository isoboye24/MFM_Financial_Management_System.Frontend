using MFMFMSF.Core.Interfaces;
using MFMFMSF.Core.Models.Workers;
using System.Net.Http.Headers;
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

        public async Task<WorkerDetail> GetByIdAsync(Guid id)
        {
            var worker = await _httpClient.GetFromJsonAsync<WorkerDetail>($"{Endpoint}/{id}");

            return worker ?? throw new InvalidOperationException("Worker not found.");
        }

        public async Task UpdateAsync(Guid id, UpdateWorkerRequest request)
        {
            var response = await _httpClient.PutAsJsonAsync($"{Endpoint}/{id}", request);

            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteAsync(Guid id)
        {
            var response = await _httpClient.DeleteAsync($"{Endpoint}/{id}");

            response.EnsureSuccessStatusCode();
        }

        public async Task<string> UploadImageAsync(string imagePath)
        {
            if (!File.Exists(imagePath))
                throw new FileNotFoundException("Image file not found.", imagePath);

            using var form = new MultipartFormDataContent();

            await using var fileStream = File.OpenRead(imagePath);

            using var fileContent = new StreamContent(fileStream);

            var extension = Path.GetExtension(imagePath).ToLowerInvariant();

            var contentType = extension switch
            {
                ".jpg" or ".jpeg" => "image/jpeg",
                ".png" => "image/png",
                _ => throw new InvalidOperationException("Unsupported image format.")
            };

            fileContent.Headers.ContentType =
                new MediaTypeHeaderValue(contentType);

            form.Add(fileContent, "file", Path.GetFileName(imagePath));

            var response = await _httpClient.PostAsync(
                $"{Endpoint}/images",
                form);

            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadFromJsonAsync<UploadImageResponse>();

            return result?.ImagePath
                ?? throw new InvalidOperationException("Image upload did not return an image path.");
        }

        public async Task<TotalMemberStatistics> GetTotalStatisticsAsync()
        {
            var statistics = await _httpClient.GetFromJsonAsync<TotalMemberStatistics>($"{Endpoint}/total/statistics");

            return statistics ?? new TotalMemberStatistics();
        }
    }
}
