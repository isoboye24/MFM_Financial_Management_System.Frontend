using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using MFMFMSF.UI.Features.Meetings.ViewModels;
using MFMFMSF.UI.Commands;

namespace MFMFMSF.UI.Features.Meetings.Controls
{
    public partial class MeetingsTable : UserControl
    {
        public ObservableCollection<MeetingItem> Meetings { get; set; }

        public ICommand ViewMeetingCommand { get; }
        public ICommand EditMeetingCommand { get; }
        public ICommand DeleteMeetingCommand { get; }


        public MeetingsTable()
        {
            InitializeComponent();

            Meetings = new ObservableCollection<MeetingItem>
            {
                new MeetingItem
                {
                    Title = "Sunday Service",
                    MaleCount = 45,
                    FemaleCount = 68,
                    ChildrenCount = 32,
                    Date = "May 4, 2025"
                },

                new MeetingItem
                {
                    Title = "Workers Meeting",
                    MaleCount = 28,
                    FemaleCount = 35,
                    ChildrenCount = 0,
                    Date = "May 10, 2025"
                },

                new MeetingItem
                {
                    Title = "Youth Fellowship",
                    MaleCount = 32,
                    FemaleCount = 41,
                    ChildrenCount = 15,
                    Date = "May 17, 2025"
                },

                new MeetingItem
                {
                    Title = "Sunday Service",
                    MaleCount = 52,
                    FemaleCount = 73,
                    ChildrenCount = 38,
                    Date = "May 18, 2025"
                },

                new MeetingItem
                {
                    Title = "Bible Study",
                    MaleCount = 21,
                    FemaleCount = 29,
                    ChildrenCount = 12,
                    Date = "May 21, 2025"
                }
            };


            ViewMeetingCommand =
                new RelayCommandGeneric<MeetingItem>(ViewMeeting);

            EditMeetingCommand =
                new RelayCommandGeneric<MeetingItem>(EditMeeting);

            DeleteMeetingCommand =
                new RelayCommandGeneric<MeetingItem>(DeleteMeeting);


            DataContext = this;
        }


        private void ViewMeeting(MeetingItem meeting)
        {
            MessageBox.Show($"View: {meeting.Title}");
        }


        private void EditMeeting(MeetingItem meeting)
        {
            MessageBox.Show($"Edit: {meeting.Title}");
        }


        private void DeleteMeeting(MeetingItem meeting)
        {
            var result = MessageBox.Show(
                $"Delete '{meeting.Title}'?",
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