using MFMFMSF.Core.Interfaces;
using MFMFMSF.Core.Models.Meetings;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MFMFMSF.UI.Features.Meetings.ViewModels
{
    public class ViewMeetingViewModel : INotifyPropertyChanged
    {
        private readonly IMeetingService _meetingService;

        public Guid MeetingId { get; }

        private MeetingDetail? _meeting;

        public MeetingDetail? Meeting
        {
            get => _meeting;
            private set
            {
                _meeting = value;
                OnPropertyChanged();
            }
        }

        public ViewMeetingViewModel(Guid meetingId, IMeetingService meetingService)
        {
            MeetingId = meetingId;
            _meetingService = meetingService;
        }

        public async Task LoadAsync()
        {
            Meeting = await _meetingService.GetByIdAsync(MeetingId);
        }

        private void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler? PropertyChanged;
    }
}