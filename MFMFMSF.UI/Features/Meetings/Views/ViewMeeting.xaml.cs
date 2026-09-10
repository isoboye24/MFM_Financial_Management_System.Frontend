using MFMFMSF.Core.Interfaces;
using MFMFMSF.UI.Features.Meetings.ViewModels;
using MFMFMSF.UI.Navigation;
using System;
using System.Windows;
using System.Windows.Controls;

namespace MFMFMSF.UI.Features.Meetings.Views
{
    public partial class ViewMeeting : UserControl
    {
        public ViewMeeting(
            Guid meetingId,
            INavigationService navigationService,
            IMeetingService meetingService)
        {
            InitializeComponent();

            // Give this ViewMeeting control the navigation service
            NavigationService = navigationService;

            // ViewModel can remain empty for now
            DataContext = new ViewMeetingViewModel();
        }


        // =========================================================
        // Navigation Service
        // =========================================================

        public INavigationService? NavigationService
        {
            get => (INavigationService?)GetValue(NavigationServiceProperty);
            set => SetValue(NavigationServiceProperty, value);
        }

        public static readonly DependencyProperty NavigationServiceProperty =
            DependencyProperty.Register(
                nameof(NavigationService),
                typeof(INavigationService),
                typeof(ViewMeeting),
                new PropertyMetadata(null));
    }
}