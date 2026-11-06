using MFMFMSF.Core.Models.WorkersNotes;

namespace MFMFMSF.Core.Interfaces
{
    public interface IWorkerNoteService
    {
        Task CreateAsync(CreateWorkerNoteRequest request);
        Task<IReadOnlyList<WorkerNoteByWorkerIdListItem>> GetAllByWorkerIdAsync(Guid memberId);
        Task<WorkerNoteDetail> GetByIdAsync(Guid id);
    }
}
