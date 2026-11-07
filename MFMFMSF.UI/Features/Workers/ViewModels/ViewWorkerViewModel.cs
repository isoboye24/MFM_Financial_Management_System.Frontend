using MFMFMSF.Core.Interfaces;
using MFMFMSF.Core.Models.Workers;
using MFMFMSF.Core.Models.WorkersNotes;
using MFMFMSF.UI.Commands;
using MFMFMSF.UI.Features.WorkerNotes.Views;
using MFMFMSF.UI.Navigation;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;

namespace MFMFMSF.UI.Features.Workers.ViewModels
{
    public class ViewWorkerViewModel : INotifyPropertyChanged
    {
        private readonly IWorkerService _workerService;
        private readonly INavigationService _navigationService;
        private readonly IPositionService _positionService;
        private readonly IGenderService _genderService;
        private readonly IWorkerNoteService _workerNoteService;

        public Guid WorkerId { get; }

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

        private bool _isWorkerNoteFormVisible;

        public bool IsWorkerNoteFormVisible
        {
            get => _isWorkerNoteFormVisible;
            set
            {
                _isWorkerNoteFormVisible = value;
                OnPropertyChanged();
            }
        }

        private string _noteTitle = string.Empty;

        public string NoteTitle
        {
            get => _noteTitle;
            set
            {
                _noteTitle = value;
                OnPropertyChanged();
            }
        }

        private string _note = string.Empty;

        public string Note
        {
            get => _note;
            set
            {
                _note = value;
                OnPropertyChanged();
            }
        }

        public ICommand AddWorkerNoteCommand { get; }
        public ICommand CancelCommand { get; }
        public ICommand SaveWorkerNoteCommand { get; }

        public ICommand ViewWorkerNoteCommand { get; }
        public ICommand EditWorkerNoteCommand { get; }
        public ICommand DeleteWorkerNoteCommand { get; }


        // =====================================================
        // EDIT WORKER NOTE FORM PROPERTIES
        // =====================================================

        private bool _isEditMode;

        public bool IsEditMode
        {
            get => _isEditMode;
            set
            {
                _isEditMode = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(FormTitle));
                OnPropertyChanged(nameof(FormDescription));
                OnPropertyChanged(nameof(SaveButtonText));
            }
        }

        public string FormTitle => IsEditMode ? "Edit Worker Note" : "Create New Note";
        public string FormDescription => IsEditMode ? "Update the title and details of this worker note." : "Enter the details of the note.";
        public string SaveButtonText => IsEditMode ? "Update Note" : "Save Note";
        private Guid? _editingNoteId;


        public ObservableCollection<WorkerNoteByWorkerIdListItem> RecentNotes { get; } = new();

        public ViewWorkerViewModel(Guid workerId, IWorkerService workerService, INavigationService navigationService, IPositionService positionService, 
            IGenderService genderService, IWorkerNoteService workerNoteService)
        {
            WorkerId = workerId;

            _workerService = workerService;
            _navigationService = navigationService;
            _positionService = positionService;
            _genderService = genderService;
            _workerNoteService = workerNoteService;

            AddWorkerNoteCommand = new RelayCommand(_ => OpenWorkerNoteForm());
            CancelCommand = new RelayCommand(_ => CloseWorkerNoteForm());
            SaveWorkerNoteCommand = new RelayCommand(async _ => await SaveWorkerNoteAsync());
            ViewWorkerNoteCommand = new RelayCommandGeneric<WorkerNoteByWorkerIdListItem>(ViewWorkerNote);
            EditWorkerNoteCommand = new RelayCommandGeneric<WorkerNoteByWorkerIdListItem>(EditWorkerNote);
            DeleteWorkerNoteCommand = new RelayCommandGeneric<WorkerNoteByWorkerIdListItem>(DeleteWorkerNote);
        }


        public async Task LoadAsync()
        {
            Worker = await _workerService.GetByIdAsync(WorkerId);

            await LoadWorkerNotesAsync();
        }

        private async Task LoadWorkerNotesAsync()
        {
            var notes = await _workerNoteService.GetAllByWorkerIdAsync(WorkerId);

            RecentNotes.Clear();

            foreach (var note in notes.OrderByDescending(x => x.CreationTime).Take(3))
            {
                RecentNotes.Add(note);
            }
        }

        private void OpenWorkerNoteForm()
        {
            _editingNoteId = null;

            IsEditMode = false;

            NoteTitle = string.Empty;
            Note = string.Empty;

            IsWorkerNoteFormVisible = true;
        }

        private void CloseWorkerNoteForm()
        {
            IsWorkerNoteFormVisible = false;

            _editingNoteId = null;
            IsEditMode = false;

            NoteTitle = string.Empty;
            Note = string.Empty;
        }

        private async Task SaveWorkerNoteAsync()
        {
            if (string.IsNullOrWhiteSpace(NoteTitle))
            {
                MessageBox.Show(
                    "Please enter the note title.",
                    "Validation",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);

                return;
            }

            if (string.IsNullOrWhiteSpace(Note))
            {
                MessageBox.Show(
                    "Please enter the note.",
                    "Validation",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);

                return;
            }

            try
            {
                if (IsEditMode && _editingNoteId.HasValue)
                {
                    var request = new UpdateWorkerNoteRequest
                    {
                        Id = _editingNoteId.Value,
                        NoteTitle = NoteTitle.Trim(),
                        Note = Note.Trim(),
                        MemberId = WorkerId
                    };

                    await _workerNoteService.UpdateAsync(request.Id, request);

                    MessageBox.Show(
                        "Worker note updated successfully.",
                        "Success",
                        MessageBoxButton.OK,
                        MessageBoxImage.Information);

                    await LoadWorkerNotesAsync();
                    CloseWorkerNoteForm();
                }
                else
                {
                    var request = new CreateWorkerNoteRequest
                    {
                        NoteTitle = NoteTitle.Trim(),
                        Note = Note.Trim(),
                        MemberId = WorkerId
                    };

                    await _workerNoteService.CreateAsync(request);

                    MessageBox.Show(
                        "Worker note created successfully.",
                        "Success",
                        MessageBoxButton.OK,
                        MessageBoxImage.Information);

                    await LoadWorkerNotesAsync();
                    ClearForm();
                }                    
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Unable to save the worker note.\n\n{ex.Message}",
                    "Save Failed",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
        }

        // =====================================================
        // CLEAR FORM
        // =====================================================

        private void ClearForm()
        {
            NoteTitle = string.Empty;
            Note = string.Empty;
        }


        // =====================================================
        // ACTIONS
        // =====================================================

        private void ViewWorkerNote(WorkerNoteByWorkerIdListItem note)
        {
            if (note == null)
                return;

            if (_navigationService == null || _workerNoteService == null)
            {
                MessageBox.Show(
                    "Navigation services are not configured.",
                    "Error",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);

                return;
            }

            _navigationService.Navigate(new ViewWorkerNote(note.Id, _navigationService, _workerNoteService, _workerService, _positionService, _genderService, 
                () => EditWorkerNote(note), () => DeleteWorkerNote(note)));
        }

        private void EditWorkerNote(WorkerNoteByWorkerIdListItem note)
        {
            if (note == null)
                return;

            _editingNoteId = note.Id;

            NoteTitle = note.NoteTitle;
            Note = note.Note;

            IsEditMode = true;
            IsWorkerNoteFormVisible = true;
        }

        private async void DeleteWorkerNote(WorkerNoteByWorkerIdListItem note)
        {
            var result = MessageBox.Show($"Are you sure you want to delete '{note.NoteTitle} '?", "Delete Worker", MessageBoxButton.YesNo, MessageBoxImage.Warning);

            if (result != MessageBoxResult.Yes)
                return;

            try
            {
                if (_workerNoteService == null)
                {
                    MessageBox.Show("Worker note service is not configured.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                await _workerNoteService.DeleteAsync(note.Id);
                RecentNotes.Remove(note);

                MessageBox.Show("Worker note deleted successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"The worker could not be deleted.\n\n{ex.Message}", "Delete Failed", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }



        private void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
