using MFMFMSF.Core.Interfaces;
using MFMFMSF.UI.Features.Workers.ViewModels;
using MFMFMSF.UI.Navigation;
using System.Windows;
using System.Windows.Controls;

namespace MFMFMSF.UI.Features.Workers.Views
{
    /// <summary>
    /// Interaction logic for EditWorker.xaml
    /// </summary>
    public partial class EditWorker : UserControl
    {
        private readonly EditWorkerViewModel _viewModel;

        public EditWorker(Guid workerId, IWorkerService workerService, INavigationService navigationService, IPositionService positionService, IGenderService genderService)
        {
            InitializeComponent();

            NavigationService = navigationService;

            _viewModel = new EditWorkerViewModel(workerId, workerService, navigationService, positionService, genderService);
            DataContext = _viewModel;

            Loaded += EditWorker_Loaded;
        }

        private async void EditWorker_Loaded(object sender, RoutedEventArgs e)
        {
            Loaded -= EditWorker_Loaded;
            await _viewModel.LoadAsync();
        }

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
                typeof(EditWorker),
                new PropertyMetadata(null));
    }
}
