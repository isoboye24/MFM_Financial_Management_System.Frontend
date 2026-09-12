using MFMFMSF.Core.Models;
using MFMFMSF.Core.Models.Meetings;
using System.Windows;
using System.Windows.Controls;

namespace MFMFMSF.UI.Features.Meetings.Controls.ViewMeeting
{
    public partial class ViewMeetingSideCards : UserControl
    {
        public ViewMeetingSideCards()
        {
            InitializeComponent();
        }


        // =====================================================
        // MEETING
        // =====================================================

        public MeetingDetail? Meeting
        {
            get => (MeetingDetail?)GetValue(MeetingProperty);
            set => SetValue(MeetingProperty, value);
        }

        public static readonly DependencyProperty MeetingProperty =
            DependencyProperty.Register(
                nameof(Meeting),
                typeof(MeetingDetail),
                typeof(ViewMeetingSideCards),
                new PropertyMetadata(null));
    }
}