using MFMFMSF.Core.Interfaces;
using MFMFMSF.Infrastructure.Service;
using MFMFMSF.UI.Commands;
using MFMFMSF.UI.Features.Meetings.Views;
using MFMFMSF.UI.Navigation;
using System.Windows.Input;

namespace MFMFMSF.UI.Features.Meetings.ViewModels
{
    public class MeetingsViewModel
    {
        private readonly INavigationService _navigationService;
        private readonly IMeetingCategoryService _meetingCategoryService;
        private readonly IMeetingService _meetingService;

        public ICommand AddMeetingCommand { get; }


        public MeetingsViewModel(
            INavigationService navigationService,
            IMeetingCategoryService meetingCategoryService,
            IMeetingService meetingService)
        {
            _navigationService = navigationService;
            _meetingCategoryService = meetingCategoryService;
            _meetingService = meetingService;

            AddMeetingCommand =  new RelayCommand(_ => AddMeeting());
        }


        private void AddMeeting()
        {
            _navigationService.Navigate(new CreateMeeting(_navigationService, _meetingCategoryService, _meetingService));
        }
    }
}