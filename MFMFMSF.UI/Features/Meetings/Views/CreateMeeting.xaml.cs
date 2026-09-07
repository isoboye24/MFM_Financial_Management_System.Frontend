using MFMFMSF.Core.Interfaces;
using MFMFMSF.UI.Features.Meetings.ViewModels;
using MFMFMSF.UI.Navigation;
using System.Windows;
using System.Windows.Controls;

namespace MFMFMSF.UI.Features.Meetings.Views
{
    public partial class CreateMeeting : UserControl
    {
        private readonly CreateMeetingViewModel _viewModel;

        public CreateMeeting(INavigationService navigationService, IMeetingCategoryService meetingCategoryService, IMeetingService meetingService)
        {
            InitializeComponent();

            _viewModel = new CreateMeetingViewModel(meetingCategoryService, meetingService);

            DataContext = _viewModel;

            Loaded += CreateMeeting_Loaded;
        }


        private async void CreateMeeting_Loaded(object sender, RoutedEventArgs e)
        {
            Loaded -= CreateMeeting_Loaded;

            await _viewModel.LoadMeetingCategoriesAsync();
        }
    }
}