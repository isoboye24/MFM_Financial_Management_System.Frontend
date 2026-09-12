using MFMFMSF.Core.Interfaces;
using MFMFMSF.UI.Features.Givings.ViewModels;
using MFMFMSF.UI.Features.Meetings.Views;
using MFMFMSF.UI.Navigation;
using System.Windows;
using System.Windows.Controls;

namespace MFMFMSF.UI.Features.Givings.Views
{
    /// <summary>
    /// Interaction logic for CreateGiving.xaml
    /// </summary>
    public partial class CreateGiving : UserControl
    {
        private readonly CreateGivingViewModel _viewModel;

        public CreateGiving(
            Guid meetingId,
            INavigationService navigationService,
            IGivingCategoryService givingCategoryService,
            IGivingService givingService)
        {
            InitializeComponent();

            NavigationService = navigationService;

            _viewModel = new CreateGivingViewModel(
                meetingId,
                givingCategoryService,
                givingService);

            DataContext = _viewModel;

            Loaded += CreateGiving_Loaded;
        }

        private async void CreateGiving_Loaded(
            object sender,
            RoutedEventArgs e)
        {
            Loaded -= CreateGiving_Loaded;
            await _viewModel.LoadGivingCategoriesAsync();
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
                typeof(CreateMeeting),
                new PropertyMetadata(null));
    }
}
