using MFMFMSF.Core.Models;
using MFMFMSF.UI.Commands;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace MFMFMSF.UI.Features.Meetings.Controls
{
    public partial class MeetingsTable : UserControl
    {
        public ICommand ViewMeetingCommand { get; }
        public ICommand EditMeetingCommand { get; }
        public ICommand DeleteMeetingCommand { get; }

        public MeetingsTable()
        {
            InitializeComponent();

            ViewMeetingCommand =
                new RelayCommandGeneric<MeetingListItem>(ViewMeeting);

            EditMeetingCommand =
                new RelayCommandGeneric<MeetingListItem>(EditMeeting);

            DeleteMeetingCommand =
                new RelayCommandGeneric<MeetingListItem>(DeleteMeeting);

            DataContext = this;
        }


        // =====================================================
        // MEETINGS
        // =====================================================

        public ObservableCollection<MeetingListItem> Meetings
        {
            get => (ObservableCollection<MeetingListItem>)GetValue(MeetingsProperty);
            set => SetValue(MeetingsProperty, value);
        }

        public static readonly DependencyProperty MeetingsProperty =
            DependencyProperty.Register(
                nameof(Meetings),
                typeof(ObservableCollection<MeetingListItem>),
                typeof(MeetingsTable),
                new PropertyMetadata(null));


        // =====================================================
        // ACTIONS
        // =====================================================

        private void ViewMeeting(MeetingListItem meeting)
        {
            MessageBox.Show(
                $"View: {meeting.MessageTitle}");
        }

        private void EditMeeting(MeetingListItem meeting)
        {
            MessageBox.Show(
                $"Edit: {meeting.MessageTitle}");
        }

        private void DeleteMeeting(MeetingListItem meeting)
        {
            var result = MessageBox.Show(
                $"Delete '{meeting.MessageTitle}'?",
                "Delete Meeting",
                MessageBoxButton.YesNo,
                MessageBoxImage.Warning);

            if (result == MessageBoxResult.Yes)
            {
                Meetings.Remove(meeting);
            }
        }
    }
}