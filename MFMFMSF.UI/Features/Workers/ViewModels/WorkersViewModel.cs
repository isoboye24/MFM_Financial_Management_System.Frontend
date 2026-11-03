using MFMFMSF.Core.Interfaces;
using MFMFMSF.Core.Models.Workers;
using MFMFMSF.UI.Commands;
using MFMFMSF.UI.Features.Workers.Views;
using MFMFMSF.UI.Navigation;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace MFMFMSF.UI.Features.Workers.ViewModels
{
    public class WorkersViewModel : INotifyPropertyChanged
    {
        private readonly INavigationService _navigationService;
        private readonly IPositionService _positionService;
        private readonly IWorkerService _workerService;
        private readonly IGenderService _genderService;
        private readonly IWorkerNoteService _workerNoteService;

        public ObservableCollection<WorkersListItem> Workers { get; } = new();

        public ICommand AddWorkerCommand { get; }

        // =====================================================
        // SERVICES
        // =====================================================
        public INavigationService NavigationService => _navigationService;
        public IPositionService PositionService => _positionService;
        public IWorkerService WorkerService => _workerService;
        public IGenderService GenderService => _genderService;
        public IWorkerNoteService WorkerNoteService => _workerNoteService;


        public WorkersViewModel(INavigationService navigationService, IPositionService positionService, IWorkerService workerService, 
            IGenderService genderService, IWorkerNoteService workerNoteService)
        {
            _navigationService = navigationService;
            _positionService = positionService;
            _workerService = workerService;
            _genderService = genderService;
            _workerNoteService = workerNoteService;

            AddWorkerCommand = new RelayCommand(_ => AddWorker());
        }

        private void AddWorker()
        {
            _navigationService.Navigate(new CreateWorker(_navigationService, _positionService, _workerService, _genderService, _workerNoteService));    
        }

        public async Task LoadWorkersAsync()
        {
            var workers = await _workerService.GetAllAsync();

            Workers.Clear();

            foreach (var worker in workers)
            {
                Workers.Add(worker);
            }
        }

        private void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(
                this,
                new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
