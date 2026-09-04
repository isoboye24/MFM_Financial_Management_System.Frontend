using System.Windows;
using System.Windows.Controls;

namespace MFMFMSF.UI.Features.Meetings.Controls.Form
{
    public partial class MeetingForm : UserControl
    {
        public MeetingForm()
        {
            InitializeComponent();
        }


        // =====================================================
        // DATE
        // =====================================================

        public DateTime? MeetingDate
        {
            get => (DateTime?)GetValue(MeetingDateProperty);
            set => SetValue(MeetingDateProperty, value);
        }

        public static readonly DependencyProperty MeetingDateProperty =
            DependencyProperty.Register(
                nameof(MeetingDate),
                typeof(DateTime?),
                typeof(MeetingForm),
                new PropertyMetadata(null));


        // =====================================================
        // CATEGORY
        // =====================================================

        public string SelectedMeetingCategory
        {
            get => (string)GetValue(SelectedMeetingCategoryProperty);
            set => SetValue(SelectedMeetingCategoryProperty, value);
        }

        public static readonly DependencyProperty SelectedMeetingCategoryProperty =
            DependencyProperty.Register(
                nameof(SelectedMeetingCategory),
                typeof(string),
                typeof(MeetingForm),
                new PropertyMetadata(string.Empty));


        // =====================================================
        // SUMMARY
        // =====================================================

        public string Summary
        {
            get => (string)GetValue(SummaryProperty);
            set => SetValue(SummaryProperty, value);
        }

        public static readonly DependencyProperty SummaryProperty =
            DependencyProperty.Register(
                nameof(Summary),
                typeof(string),
                typeof(MeetingForm),
                new PropertyMetadata(string.Empty));


        // =====================================================
        // MESSAGE TITLE
        // =====================================================

        public string MessageTitle
        {
            get => (string)GetValue(MessageTitleProperty);
            set => SetValue(MessageTitleProperty, value);
        }

        public static readonly DependencyProperty MessageTitleProperty =
            DependencyProperty.Register(
                nameof(MessageTitle),
                typeof(string),
                typeof(MeetingForm),
                new PropertyMetadata(string.Empty));


        // =====================================================
        // MINISTER NAME
        // =====================================================

        public string MinisterName
        {
            get => (string)GetValue(MinisterNameProperty);
            set => SetValue(MinisterNameProperty, value);
        }

        public static readonly DependencyProperty MinisterNameProperty =
            DependencyProperty.Register(
                nameof(MinisterName),
                typeof(string),
                typeof(MeetingForm),
                new PropertyMetadata(string.Empty));


        // =====================================================
        // MALE ATTENDANCE
        // =====================================================

        public int MaleAttendance
        {
            get => (int)GetValue(MaleAttendanceProperty);
            set => SetValue(MaleAttendanceProperty, value);
        }

        public static readonly DependencyProperty MaleAttendanceProperty =
            DependencyProperty.Register(
                nameof(MaleAttendance),
                typeof(int),
                typeof(MeetingForm),
                new PropertyMetadata(0));


        // =====================================================
        // FEMALE ATTENDANCE
        // =====================================================

        public int FemaleAttendance
        {
            get => (int)GetValue(FemaleAttendanceProperty);
            set => SetValue(FemaleAttendanceProperty, value);
        }

        public static readonly DependencyProperty FemaleAttendanceProperty =
            DependencyProperty.Register(
                nameof(FemaleAttendance),
                typeof(int),
                typeof(MeetingForm),
                new PropertyMetadata(0));


        // =====================================================
        // CHILDREN ATTENDANCE
        // =====================================================

        public int ChildrenAttendance
        {
            get => (int)GetValue(ChildrenAttendanceProperty);
            set => SetValue(ChildrenAttendanceProperty, value);
        }

        public static readonly DependencyProperty ChildrenAttendanceProperty =
            DependencyProperty.Register(
                nameof(ChildrenAttendance),
                typeof(int),
                typeof(MeetingForm),
                new PropertyMetadata(0));


        // =====================================================
        // MESSAGE
        // =====================================================

        public string Message
        {
            get => (string)GetValue(MessageProperty);
            set => SetValue(MessageProperty, value);
        }

        public static readonly DependencyProperty MessageProperty =
            DependencyProperty.Register(
                nameof(Message),
                typeof(string),
                typeof(MeetingForm),
                new PropertyMetadata(string.Empty));
    }
}