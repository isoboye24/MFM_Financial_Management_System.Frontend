using MFMFMSF.UI.Commands;
using MFMFMSF.UI.Features.Meetings.Views;
using MFMFMSF.UI.Navigation;
using System.Windows.Input;

namespace MFMFMSF.UI.Features.Meetings.ViewModels
{
    public class MeetingsViewModel
    {
        private readonly INavigationService _navigationService;

        public ICommand AddMeetingCommand { get; }

        public MeetingsViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;

            AddMeetingCommand =
                new RelayCommand(_ => AddMeeting());
        }

        private void AddMeeting()
        {
            _navigationService.Navigate(
                new CreateMeeting(_navigationService));
        }
    }
}
