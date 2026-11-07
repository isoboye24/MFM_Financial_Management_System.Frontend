namespace MFMFMSF.Core.Models.WorkersNotes
{
    public class UpdateWorkerNoteRequest
    {
        public required Guid Id { get; set; }
        public required string NoteTitle { get; set; }
        public required string Note { get; set; }
        public required Guid MemberId { get; set; }
    }
}
