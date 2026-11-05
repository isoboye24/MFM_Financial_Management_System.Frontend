using MFMFMSF.Core.Interfaces;
using MFMFMSF.Core.Models.Workers;
using MFMFMSF.Core.Models.WorkersNotes;
using MFMFMSF.UI.Commands;
using MFMFMSF.UI.Features.Workers.Views;
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
            //ViewWorkerNoteCommand = new RelayCommand(async _ => await ViewWorkerNoteAsync());
            //EditWorkerNoteCommand = new RelayCommand(async _ => await EditWorkerNoteAsync());
            //DeleteWorkerNoteCommand = new RelayCommand(async _ => await DeleteWorkerNoteAsync());
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

            foreach (var note in notes)
            {
                RecentNotes.Add(note);
            }
        }

        private void OpenWorkerNoteForm()
        {
            NoteTitle = string.Empty;
            Note = string.Empty;

            IsWorkerNoteFormVisible = true;
        }

        private void CloseWorkerNoteForm()
        {
            IsWorkerNoteFormVisible = false;

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

            
            var request = new CreateWorkerNoteRequest
            {
                NoteTitle = NoteTitle.Trim(),
                Note = Note.Trim(),
                MemberId = WorkerId
            };
                

            await _workerNoteService.CreateAsync(request);

            await LoadWorkerNotesAsync();

            MessageBox.Show(
                "Worker note created successfully.",
                "Success",
                MessageBoxButton.OK,
                MessageBoxImage.Information);

            ClearForm();
        }

        // =====================================================
        // CLEAR FORM
        // =====================================================

        private void ClearForm()
        {
            NoteTitle = string.Empty;
            Note = string.Empty;
        }


        private void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
