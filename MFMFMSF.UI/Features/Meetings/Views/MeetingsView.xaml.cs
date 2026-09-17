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

        public MeetingsView(INavigationService navigationService, IMeetingCategoryService meetingCategoryService, IMeetingService meetingService, 
            IGivingCategoryService givingCategoryService, IGivingService givingService)
        {
            InitializeComponent();

            _viewModel = new MeetingsViewModel(navigationService, meetingCategoryService, meetingService, givingCategoryService, givingService);

            DataContext = _viewModel;

            MeetingPageTopBar.PeriodChanged += MonthYearPicker_PeriodChanged;

            Loaded += MeetingsView_Loaded;
        }

        private async void MonthYearPicker_PeriodChanged(object? sender, PeriodChangedEventArgs e)
        {
            await _viewModel.SetSelectedPeriodAsync(e.Month, e.Year);
            PeriodChanged?.Invoke(this, e);
        }

        private async void MeetingsView_Loaded(object sender, RoutedEventArgs e)
        {
            await _viewModel.LoadMeetingsAsync();
        }
    }
}