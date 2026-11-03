using MFMFMSF.Core.Interfaces;
using MFMFMSF.Core.Models.Workers;
using MFMFMSF.UI.Commands;
using MFMFMSF.UI.Features.Workers.Views;
using MFMFMSF.UI.Navigation;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace MFMFMSF.UI.Features.Workers.Controls
{
    /// <summary>
    /// Interaction logic for WorkersTable.xaml
    /// </summary>
    public partial class WorkersTable : UserControl
    {
        public ICommand ViewWorkerCommand { get; }
        public ICommand EditWorkerCommand { get; }
        public ICommand DeleteWorkerCommand { get; }

        public WorkersTable()
        {
            InitializeComponent();

            ViewWorkerCommand = new RelayCommandGeneric<WorkersListItem>(ViewWorker);
            EditWorkerCommand = new RelayCommandGeneric<WorkersListItem>(EditWorker);
            DeleteWorkerCommand = new RelayCommandGeneric<WorkersListItem>(DeleteWorker);
        }

        // =====================================================
        // WORKERS
        // =====================================================

        public ObservableCollection<WorkersListItem> Workers
        {
            get => (ObservableCollection<WorkersListItem>)GetValue(WorkersProperty);
            set => SetValue(WorkersProperty, value);
        }

        public static readonly DependencyProperty WorkersProperty =
            DependencyProperty.Register(
                nameof(Workers),
                typeof(ObservableCollection<WorkersListItem>),
                typeof(WorkersTable),
                new PropertyMetadata(null));


        // =====================================================
        // NAVIGATION SERVICE
        // =====================================================

        public INavigationService? NavigationService
        {
            get => (INavigationService?)GetValue(NavigationServiceProperty);
            set => SetValue(NavigationServiceProperty, value);
        }

        public static readonly DependencyProperty NavigationServiceProperty =
            DependencyProperty.Register(
                nameof(NavigationService),
                typeof(INavigationService),
                typeof(WorkersTable),
                new PropertyMetadata(null));


        // =====================================================
        // POSITION SERVICE
        // =====================================================

        public IPositionService? PositionService
        {
            get => (IPositionService?)GetValue(PositionServiceProperty);
            set => SetValue(PositionServiceProperty, value);
        }

        public static readonly DependencyProperty PositionServiceProperty =
            DependencyProperty.Register(
                nameof(PositionService),
                typeof(IPositionService),
                typeof(WorkersTable),
                new PropertyMetadata(null));
        
        
        // =====================================================
        // WORKER SERVICE
        // =====================================================

        public IWorkerService? WorkerService
        {
            get => (IWorkerService?)GetValue(WorkerServiceProperty);
            set => SetValue(WorkerServiceProperty, value);
        }

        public static readonly DependencyProperty WorkerServiceProperty =
            DependencyProperty.Register(
                nameof(WorkerService),
                typeof(IWorkerService),
                typeof(WorkersTable),
                new PropertyMetadata(null));


        // =====================================================
        // GENDER SERVICE
        // =====================================================

        public IGenderService? GenderService
        {
            get => (IGenderService?)GetValue(GenderServiceProperty);
            set => SetValue(GenderServiceProperty, value);
        }

        public static readonly DependencyProperty GenderServiceProperty =
            DependencyProperty.Register(
                nameof(GenderService),
                typeof(IGenderService),
                typeof(WorkersTable),
                new PropertyMetadata(null));
        
        
        // =====================================================
        // WORKER NOTE SERVICE
        // =====================================================

        public IWorkerNoteService? WorkerNoteService
        {
            get => (IWorkerNoteService?)GetValue(WorkerNoteServiceProperty);
            set => SetValue(WorkerNoteServiceProperty, value);
        }

        public static readonly DependencyProperty WorkerNoteServiceProperty =
            DependencyProperty.Register(
                nameof(WorkerNoteService),
                typeof(IWorkerNoteService),
                typeof(WorkersTable),
                new PropertyMetadata(null));


        // =====================================================
        // ACTIONS
        // =====================================================

        private void ViewWorker(WorkersListItem worker)
        {
            if (NavigationService == null || WorkerService == null || PositionService == null || GenderService == null || WorkerNoteService == null)
            {
                MessageBox.Show(
                    "Navigation services are not configured.",
                    "Error",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);

                return;
            }

            NavigationService.Navigate(new ViewWorker(worker.Id, WorkerService, NavigationService, PositionService, GenderService, WorkerNoteService));
        }
        
        private void EditWorker(WorkersListItem worker)
        {
            if (NavigationService == null || WorkerService == null || PositionService == null || GenderService == null)
            {
                MessageBox.Show(
                    "Navigation services are not configured.",
                    "Error",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);

                return;
            }

            NavigationService.Navigate(new EditWorker(worker.Id, WorkerService, NavigationService, PositionService, GenderService, WorkerNoteService));
        }

        private async void DeleteWorker(WorkersListItem worker)
        {
            var result = MessageBox.Show($"Are you sure you want to delete '{worker.FirstName} {worker.LastName}'?", "Delete Worker", MessageBoxButton.YesNo, MessageBoxImage.Warning);

            if (result != MessageBoxResult.Yes)
                return;

            try
            {
                if (WorkerService == null)
                {
                    MessageBox.Show("Worker service is not configured.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                await WorkerService.DeleteAsync(worker.Id);

                Workers.Remove(worker);

                MessageBox.Show("Worker deleted successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"The worker could not be deleted.\n\n{ex.Message}", "Delete Failed", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
