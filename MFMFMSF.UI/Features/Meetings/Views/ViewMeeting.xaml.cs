using MFMFMSF.Core.Interfaces;
using MFMFMSF.UI.Features.Meetings.ViewModels;
using MFMFMSF.UI.Navigation;
using System.Windows;
using System.Windows.Controls;

namespace MFMFMSF.UI.Features.Meetings.Views
{
    public partial class ViewMeeting : UserControl
    {
        private readonly ViewMeetingViewModel _viewModel;

        public ViewMeeting(Guid meetingId, 
            INavigationService navigationService, 
            IMeetingService meetingService, 
            IGivingCategoryService givingCategoryService, 
            IGivingService givingService)
        {
            InitializeComponent();

            NavigationService = navigationService;

            // Create the ViewModel
            _viewModel = new ViewMeetingViewModel(meetingId, navigationService, meetingService, givingCategoryService, givingService);

            // Set the ViewModel as DataContext
            DataContext = _viewModel;

            Loaded += ViewMeeting_Loaded;
        }


        // =========================================================
        // Navigation Service
        // =========================================================

        public INavigationService? NavigationService
        {
            get => (INavigationService?)GetValue(
                NavigationServiceProperty);

            set => SetValue(
                NavigationServiceProperty,
                value);
        }

        public static readonly DependencyProperty NavigationServiceProperty =
            DependencyProperty.Register(
                nameof(NavigationService),
                typeof(INavigationService),
                typeof(ViewMeeting),
                new PropertyMetadata(null));


        // =========================================================
        // Load Meeting Data
        // =========================================================

        private async void ViewMeeting_Loaded(object sender, RoutedEventArgs e)
        {
            Loaded -= ViewMeeting_Loaded;

            try
            {
                await _viewModel.LoadAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.Message,
                    "Unable to Load Church Service",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
        }
    }
}