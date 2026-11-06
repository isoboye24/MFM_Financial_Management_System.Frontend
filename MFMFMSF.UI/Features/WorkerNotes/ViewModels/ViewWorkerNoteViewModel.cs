using MFMFMSF.Core.Interfaces;
using MFMFMSF.Core.Models.Workers;
using MFMFMSF.Core.Models.WorkersNotes;
using MFMFMSF.UI.Navigation;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MFMFMSF.UI.Features.WorkerNotes.ViewModels
{
    public class ViewWorkerNoteViewModel : INotifyPropertyChanged
    {
        private readonly INavigationService _navigationService;
        private readonly IWorkerNoteService _workerNoteService;
        private readonly IWorkerService _workerService;
        private readonly IPositionService _positionService;
        private readonly IGenderService _genderService;

        public Guid NoteId { get; }

        private WorkerNoteDetail? _workerNote;

        public WorkerNoteDetail? WorkerNote
        {
            get => _workerNote;
            private set
            {
                _workerNote = value;
                OnPropertyChanged();
            }
        }

        private WorkerDetail? _worker;

        public WorkerDetail? Worker
        {
            get => _worker;
            private set
            {
                _worker = value;
                OnPropertyChanged();
            }
        }

        public ViewWorkerNoteViewModel(
            Guid noteId,
            INavigationService navigationService,
            IWorkerNoteService workerNoteService,
            IWorkerService workerService,
            IPositionService positionService,
            IGenderService genderService)
        {
            NoteId = noteId;
            _navigationService = navigationService;
            _workerNoteService = workerNoteService;
            _workerService = workerService;
            _positionService = positionService;
            _genderService = genderService;
        }

        // =====================================================
        // LOAD NOTE
        // =====================================================

        public async Task LoadAsync()
        {
            WorkerNote = await _workerNoteService.GetByIdAsync(NoteId);

            if (WorkerNote == null)
                return;

            Worker = await _workerService.GetByIdAsync(WorkerNote.MemberId);
        }

        // =====================================================
        // ACTIONS
        // =====================================================

        private void Back()
        {
            _navigationService.GoBack();
        }

        // =====================================================
        // PROPERTY CHANGED
        // =====================================================

        private void OnPropertyChanged(
            [CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(
                this,
                new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler? PropertyChanged;
    }
}