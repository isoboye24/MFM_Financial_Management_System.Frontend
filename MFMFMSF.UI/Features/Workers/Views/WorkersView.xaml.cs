using MFMFMSF.Core.Interfaces;
using MFMFMSF.UI.Controls;
using MFMFMSF.UI.Features.Workers.ViewModels;
using MFMFMSF.UI.Navigation;
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

        public WorkersView(INavigationService navigationService, IPositionService positionService, IWorkerService workerService, IGenderService genderService)
        {
            InitializeComponent();

            _workerService = workerService;

            _viewModel = new WorkersViewModel(navigationService, positionService, _workerService, genderService);

            DataContext = _viewModel;
        }
    }
}
