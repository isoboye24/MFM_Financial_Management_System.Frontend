using MFMFMSF.Core.Interfaces;
using MFMFMSF.Core.Models.Meetings;
using MFMFMSF.UI.Commands;
using MFMFMSF.UI.Features.Meetings.Views;
using MFMFMSF.UI.Navigation;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace MFMFMSF.UI.Features.Meetings.ViewModels
{
    public class MeetingsViewModel
    {
        private readonly INavigationService _navigationService;
        private readonly IMeetingCategoryService _meetingCategoryService;
        private readonly IMeetingService _meetingService;
        private readonly IGivingCategoryService _givingCategoryService;
        private readonly IGivingService _givingService;

        public ObservableCollection<MeetingListItem> Meetings { get; } = new();

        public ICommand AddMeetingCommand { get; }

        // =====================================================
        // SERVICES
        // =====================================================
        public INavigationService NavigationService => _navigationService;
        public IMeetingCategoryService MeetingCategoryService => _meetingCategoryService;
        public IMeetingService MeetingService => _meetingService;
        public IGivingCategoryService GivingCategoryService => _givingCategoryService;
        public IGivingService GivingService => _givingService;



        public MeetingsViewModel(INavigationService navigationService, IMeetingCategoryService meetingCategoryService, IMeetingService meetingService, 
            IGivingCategoryService givingCategoryService, IGivingService givingService)
        {
            _navigationService = navigationService;
            _meetingCategoryService = meetingCategoryService;
            _meetingService = meetingService;
            _givingCategoryService = givingCategoryService;
            _givingService = givingService;

            AddMeetingCommand =  new RelayCommand(_ => AddMeeting());
        }


        private void AddMeeting()
        {
            _navigationService.Navigate(new CreateMeeting(_navigationService, _meetingCategoryService, _meetingService, _givingCategoryService, _givingService));
        }

        public async Task LoadMeetingsAsync()
        {
            var meetings = await _meetingService.GetAllAsync();

            Meetings.Clear();

            foreach (var meeting in meetings)
            {
                Meetings.Add(meeting);
            }
        }
    }
}