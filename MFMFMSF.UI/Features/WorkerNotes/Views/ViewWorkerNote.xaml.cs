using MFMFMSF.Core.Interfaces;
using MFMFMSF.UI.Features.Givings.Views;
using MFMFMSF.UI.Features.WorkerNotes.ViewModels;
using MFMFMSF.UI.Navigation;
using System.Windows;
using System.Windows.Controls;

namespace MFMFMSF.UI.Features.WorkerNotes.Views
{
    public partial class ViewWorkerNote : UserControl
    {
        private readonly ViewWorkerNoteViewModel _viewModel;

        public ViewWorkerNote(Guid noteId, INavigationService navigationService, IWorkerNoteService workerNoteService, IWorkerService workerService, 
            IPositionService positionService, IGenderService genderService)
        {
            InitializeComponent();

            NavigationService = navigationService;

            _viewModel = new ViewWorkerNoteViewModel(noteId, navigationService, workerNoteService, workerService, positionService, genderService);

            DataContext = _viewModel;

            Loaded += ViewWorkerNote_Loaded;
        }

        private async void ViewWorkerNote_Loaded(object sender, RoutedEventArgs e)
        {
            Loaded -= ViewWorkerNote_Loaded;

            try
            {
                await _viewModel.LoadAsync();
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(
                    $"Unable to load worker note.\n\n{ex.Message}",
                    "Loading Failed",
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
                typeof(ViewWorkerNote),
                new PropertyMetadata(null));
    }
}