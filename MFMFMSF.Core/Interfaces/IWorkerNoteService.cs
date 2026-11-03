using MFMFMSF.Core.Models.WorkersNotes;

namespace MFMFMSF.Core.Interfaces
{
    public interface IWorkerNoteService
    {
        Task CreateAsync(CreateWorkerNoteRequest request);
    }
}
