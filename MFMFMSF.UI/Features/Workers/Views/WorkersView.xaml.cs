using MFMFMSF.Core.Interfaces;
using MFMFMSF.UI.Controls;
using MFMFMSF.UI.Features.Workers.ViewModels;
using MFMFMSF.UI.Navigation;
using System.Windows;
using System.Windows.Controls;

namespace MFMFMSF.UI.Features.Workers.Views
{
    /// <summary>
    /// Interaction logic for WorkersView.xaml
    /// </summary>
    public partial class WorkersView : UserControl
    {
        private readonly WorkersViewModel _viewModel;
        public event EventHandler<PeriodChangedEventArgs>? PeriodChanged;
        private readonly IWorkerService _workerService;
        private readonly IWorkerNoteService _workerNoteService;

        public WorkersView(INavigationService navigationService, IPositionService positionService, IWorkerService workerService, IGenderService genderService, 
            IWorkerNoteService workerNoteService)
        {
            InitializeComponent();

            _workerService = workerService;
            _workerNoteService = workerNoteService;

            _viewModel = new WorkersViewModel(navigationService, positionService, _workerService, genderService, _workerNoteService);

            DataContext = _viewModel;

            Loaded += WorkersView_Loaded;
        }

        private async void WorkersView_Loaded(object sender, RoutedEventArgs e)
        {
            await _viewModel.LoadWorkersAsync();
        }
    }
}
