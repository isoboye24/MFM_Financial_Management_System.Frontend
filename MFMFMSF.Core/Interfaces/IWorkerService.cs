using MFMFMSF.Core.Models.Workers;

namespace MFMFMSF.Core.Interfaces
{
    public interface IWorkerService
    {
        Task CreateAsync(CreateWorkerRequest request);
        Task<IReadOnlyList<WorkersListItem>> GetAllAsync();
        Task<WorkerDetail> GetByIdAsync(Guid id);
    }
}
