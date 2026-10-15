using MFMFMSF.Core.Interfaces;
using MFMFMSF.UI.Controls;
using MFMFMSF.UI.Features.Meetings.ViewModels;
using MFMFMSF.UI.Navigation;
using System.Windows;
using System.Windows.Controls;

namespace MFMFMSF.UI.Features.Meetings.Views
{
    public partial class MeetingsView : UserControl
    {
        private readonly MeetingsViewModel _viewModel;
        public event EventHandler<PeriodChangedEventArgs>? PeriodChanged;
        private readonly IMeetingService _meetingService;

        public MeetingsView(INavigationService navigationService, IMeetingCategoryService meetingCategoryService, IMeetingService meetingService, 
            IGivingCategoryService givingCategoryService, IGivingService givingService, IExpenditureService expenditureService)
        {
            InitializeComponent();
            _meetingService = meetingService;

            _viewModel = new MeetingsViewModel(navigationService, meetingCategoryService, _meetingService, givingCategoryService, givingService, expenditureService);

            DataContext = _viewModel;

            // The chart uses the same registered meeting service.
            MaleAttendanceTrendControl.SetMeetingService(_meetingService);

            MeetingPageTopBar.PeriodChanged += MonthYearPicker_PeriodChanged;

            Loaded += MeetingsView_Loaded;
        }

        private async void MonthYearPicker_PeriodChanged(object? sender, PeriodChangedEventArgs e)
        {
            await _viewModel.SetSelectedPeriodAsync(e.Month, e.Year);
            await MaleAttendanceTrendControl.SetSelectedPeriodAsync(e.Month, e.Year);

            PeriodChanged?.Invoke(this, e);
        }

        private async void MeetingsView_Loaded(object sender, RoutedEventArgs e)
        {
            await _viewModel.LoadMeetingsAsync();
        }
    }
}