using MFMFMSF.Core.Interfaces;
using MFMFMSF.Core.Models.Meetings;
using MFMFMSF.UI.Commands;
using MFMFMSF.UI.Features.Meetings.Views;
using MFMFMSF.UI.Navigation;
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
        // NAVIGATION SERVICE
        // =====================================================

        public INavigationService? NavigationService
        {
            get => (INavigationService?)GetValue(NavigationServiceProperty);
            set => SetValue(NavigationServiceProperty, value);
        }

        public static readonly DependencyProperty NavigationServiceProperty =
            DependencyProperty.Register(
                nameof(NavigationService),
                typeof(INavigationService),
                typeof(MeetingsTable),
                new PropertyMetadata(null));


        // =====================================================
        // MEETING CATEGORY SERVICE
        // =====================================================

        public IMeetingCategoryService? MeetingCategoryService
        {
            get => (IMeetingCategoryService?)GetValue(MeetingCategoryServiceProperty);
            set => SetValue(MeetingCategoryServiceProperty, value);
        }

        public static readonly DependencyProperty MeetingCategoryServiceProperty =
            DependencyProperty.Register(
                nameof(MeetingCategoryService),
                typeof(IMeetingCategoryService),
                typeof(MeetingsTable),
                new PropertyMetadata(null));


        // =====================================================
        // MEETING SERVICE
        // =====================================================

        public IMeetingService? MeetingService
        {
            get => (IMeetingService?)GetValue(MeetingServiceProperty);
            set => SetValue(MeetingServiceProperty, value);
        }

        public static readonly DependencyProperty MeetingServiceProperty =
            DependencyProperty.Register(
                nameof(MeetingService),
                typeof(IMeetingService),
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
            if (NavigationService == null ||
                MeetingCategoryService == null ||
                MeetingService == null)
            {
                MessageBox.Show(
                    "Navigation services are not configured.",
                    "Error",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);

                return;
            }

            NavigationService.Navigate(
                new EditMeeting(
                    meeting.Id,
                    NavigationService,
                    MeetingCategoryService,
                    MeetingService));
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