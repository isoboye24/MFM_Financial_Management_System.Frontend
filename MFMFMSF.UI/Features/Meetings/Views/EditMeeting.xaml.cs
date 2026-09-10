using MFMFMSF.Core.Interfaces;
using MFMFMSF.UI.Features.Meetings.ViewModels;
using MFMFMSF.UI.Navigation;
using System.Windows;
using System.Windows.Controls;

namespace MFMFMSF.UI.Features.Meetings.Views
{
    public partial class EditMeeting : UserControl
    {
        private readonly EditMeetingViewModel _viewModel;

        public EditMeeting(
            Guid meetingId,
            INavigationService navigationService,
            IMeetingCategoryService meetingCategoryService,
            IMeetingService meetingService)
        {
            InitializeComponent();

            NavigationService = navigationService;

            _viewModel = new EditMeetingViewModel(meetingId, meetingService, meetingCategoryService, navigationService);

            DataContext = _viewModel;

            Loaded += EditMeeting_Loaded;
        }


        private async void EditMeeting_Loaded(
            object sender,
            RoutedEventArgs e)
        {
            Loaded -= EditMeeting_Loaded;

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
                typeof(EditMeeting),
                new PropertyMetadata(null));
    }
}