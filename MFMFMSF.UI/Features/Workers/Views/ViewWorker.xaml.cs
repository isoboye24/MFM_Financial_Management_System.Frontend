using MFMFMSF.Core.Interfaces;
using MFMFMSF.UI.Features.Workers.ViewModels;
using MFMFMSF.UI.Navigation;
using System.Windows;
using System.Windows.Controls;

namespace MFMFMSF.UI.Features.Workers.Views
{
    /// <summary>
    /// Interaction logic for ViewWorker.xaml
    /// </summary>
    public partial class ViewWorker : UserControl
    {
        private readonly ViewWorkerViewModel _viewModel;

        public ViewWorker(Guid workerId, IWorkerService workerService, INavigationService navigationService, IPositionService positionService, 
            IGenderService genderService, IWorkerNoteService workerNoteService)
        {
            InitializeComponent();

            NavigationService = navigationService;

            _viewModel = new ViewWorkerViewModel(workerId, workerService, navigationService, positionService, genderService, workerNoteService);
            DataContext = _viewModel;

            Loaded += ViewWorker_Loaded;
        }

        private async void ViewWorker_Loaded(object sender, RoutedEventArgs e)
        {
            Loaded -= ViewWorker_Loaded;

            try
            {
                await _viewModel.LoadAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.Message,
                    "Unable to Load Worker details",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
        }

        public INavigationService? NavigationService
        {
            get => (INavigationService?)GetValue(NavigationServiceProperty);
            set => SetValue(NavigationServiceProperty, value);
        }

        public static readonly DependencyProperty NavigationServiceProperty =
            DependencyProperty.Register(
                nameof(NavigationService),
                typeof(INavigationService),
                typeof(ViewWorker),
                new PropertyMetadata(null));

    }
}
