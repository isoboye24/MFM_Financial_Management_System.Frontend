using MFMFMSF.Core.Interfaces;
using MFMFMSF.UI.Features.Workers.ViewModels;
using MFMFMSF.UI.Navigation;
using System.Windows;
using System.Windows.Controls;

namespace MFMFMSF.UI.Features.Workers.Views
{
    /// <summary>
    /// Interaction logic for CreateWorker.xaml
    /// </summary>
    public partial class CreateWorker : UserControl
    {
        private readonly CreateWorkerViewModel _viewModel;
        public CreateWorker(INavigationService navigationService, IPositionService positionService, IWorkerService workerService, IGenderService genderService,
            IWorkerNoteService workerNoteService)
        {
            InitializeComponent();

            NavigationService = navigationService;

            _viewModel = new CreateWorkerViewModel(workerService, positionService, genderService, navigationService, workerNoteService);
            DataContext = _viewModel;

            Loaded += Load_positions_and_genders_Loaded;
        }


        // =====================================================
        // Load Data
        // =====================================================

        private async void Load_positions_and_genders_Loaded(object sender, RoutedEventArgs e)
        {
            Loaded -= Load_positions_and_genders_Loaded;

            try
            {
                await Task.WhenAll(_viewModel.LoadPositionsAsync(), _viewModel.LoadGendersAsync());
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Unable to load worker form data.\n\n{ex.Message}",
                    "Loading Error",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
        }


        // =====================================================
        // Navigation Service
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
                typeof(CreateWorker),
                new PropertyMetadata(null));
    }
}
