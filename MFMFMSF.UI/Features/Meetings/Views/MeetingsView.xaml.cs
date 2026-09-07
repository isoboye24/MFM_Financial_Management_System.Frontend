using MFMFMSF.Core.Interfaces;
using MFMFMSF.UI.Features.Meetings.ViewModels;
using MFMFMSF.UI.Navigation;
using System.Windows.Controls;

namespace MFMFMSF.UI.Features.Meetings.Views
{
    public partial class MeetingsView : UserControl
    {
        public MeetingsView(INavigationService navigationService, IMeetingCategoryService meetingCategoryService, IMeetingService meetingService)
        {
            InitializeComponent();
            DataContext = new MeetingsViewModel(navigationService, meetingCategoryService, meetingService);
        }
    }
}