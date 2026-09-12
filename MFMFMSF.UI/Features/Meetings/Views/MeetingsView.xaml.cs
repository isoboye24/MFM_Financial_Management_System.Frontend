using MFMFMSF.Core.Interfaces;
using MFMFMSF.UI.Features.Meetings.ViewModels;
using MFMFMSF.UI.Navigation;
using System.Windows;
using System.Windows.Controls;

namespace MFMFMSF.UI.Features.Meetings.Views
{
    public partial class MeetingsView : UserControl
    {
        private readonly MeetingsViewModel _viewModel;

        public MeetingsView(INavigationService navigationService, IMeetingCategoryService meetingCategoryService, IMeetingService meetingService, 
            IGivingCategoryService givingCategoryService, IGivingService givingService)
        {
            InitializeComponent();

            _viewModel = new MeetingsViewModel(navigationService, meetingCategoryService, meetingService, givingCategoryService, givingService);

            DataContext = _viewModel;

            Loaded += MeetingsView_Loaded;
        }

        private async void MeetingsView_Loaded(object sender, RoutedEventArgs e)
        {
            await _viewModel.LoadMeetingsAsync();
        }
    }
}