using MFMFMSF.Core.Interfaces;
using MFMFMSF.Infrastructure.Service;
using MFMFMSF.UI.Controls;
using MFMFMSF.UI.Features.Dashboard.Controls;
using MFMFMSF.UI.Features.Dashboard.ViewModels;
using MFMFMSF.UI.Features.Expenditures.Views;
using MFMFMSF.UI.Features.Meetings.Views;
using MFMFMSF.UI.Features.Reports.Views;
using MFMFMSF.UI.Navigation;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;

namespace MFMFMSF.UI.Features.Dashboard.Views
{
    public partial class DashboardView : UserControl
    {
        private readonly DashboardViewModel _viewModel;
        private readonly IncomeChart _incomeChart;
        private readonly IncomeVsExpenditure _incomeVsExpenditure;
        private readonly RecentRecords _recentRecords;

        private readonly INavigationService _navigationService;
        private readonly IMeetingCategoryService _meetingCategoryService;
        private readonly IMeetingService _meetingService;
        private readonly IGivingCategoryService _givingCategoryService;
        private readonly IGivingService _givingService;
        private readonly IExpenditureService _expenditureService;
        private readonly IReportService _reportService;

        public ICommand AddMeetingCommand { get; }


        // =====================================================
        // SERVICES
        // =====================================================
        public INavigationService NavigationService => _navigationService;
        public IMeetingCategoryService MeetingCategoryService => _meetingCategoryService;
        public IMeetingService MeetingService => _meetingService;
        public IGivingCategoryService GivingCategoryService => _givingCategoryService;
        public IGivingService GivingService => _givingService;
        public IExpenditureService ExpenditureService => _expenditureService;
        public IReportService ReportService => _reportService;

        public DashboardView(INavigationService navigationService, IMeetingCategoryService meetingCategoryService, IMeetingService meetingService, 
            IGivingCategoryService givingCategoryService, IGivingService givingService, IExpenditureService expenditureService, IncomeChart incomeChart, 
            IncomeVsExpenditure incomeVsExpenditure, IReportService reportService)
        {
            InitializeComponent();

            _navigationService = navigationService;
            _meetingCategoryService = meetingCategoryService;
            _meetingService = meetingService;
            _givingCategoryService = givingCategoryService;
            _givingService = givingService;
            _expenditureService = expenditureService;
            _reportService = reportService;

            DashboardQuickActions.AddMeetingClicked += DashboardQuickActions_AddMeetingClicked;

            DashboardQuickActions.AddExpenditureClicked += DashboardQuickActions_AddExpenditureClicked;

            DashboardQuickActions.GenerateReportClicked += DashboardQuickActions_GenerateReportClicked;

            _incomeChart = incomeChart;
            IncomeChartHost.Content = _incomeChart;

            _incomeVsExpenditure = incomeVsExpenditure;
            IncomeVsExpenditureHost.Content = _incomeVsExpenditure;

            _recentRecords = new RecentRecords(_givingService);
            RecentRecordsHost.Content = _recentRecords;

            _viewModel = new DashboardViewModel(_navigationService, _givingService, _expenditureService);

            DataContext = _viewModel;

            DashboardTopWelcomeBar.PeriodChanged +=
                DashboardTopWelcomeBar_PeriodChanged;

            Loaded += DashboardView_Loaded;
        }

        private async void DashboardView_Loaded(object sender, RoutedEventArgs e)
        {
            await _viewModel.LoadDashboardDataAsync();
        }

        private void DashboardQuickActions_AddMeetingClicked(object? sender, RoutedEventArgs e)
        {
            _navigationService.Navigate(new CreateMeeting(_navigationService, _meetingCategoryService, _meetingService, _givingCategoryService, _givingService));
        }

        private void DashboardQuickActions_AddExpenditureClicked(object? sender, RoutedEventArgs e)
        {
            _navigationService.Navigate(new CreateExpenditure(_navigationService, _expenditureService));
        }
        private void DashboardQuickActions_GenerateReportClicked(object? sender, RoutedEventArgs e)
        {
            _navigationService.Navigate(new CreateFinancialSummaryReport(_reportService, _navigationService));
        }

        private async void DashboardTopWelcomeBar_PeriodChanged(object? sender, PeriodChangedEventArgs e)
        {
            await _viewModel.SetSelectedPeriodAsync(e.Month, e.Year);
            await _incomeChart.SetSelectedPeriodAsync(e.Month, e.Year);
            await _incomeVsExpenditure.SetSelectedPeriodAsync(e.Month, e.Year);
            await _recentRecords.SetSelectedPeriodAsync(e.Month, e.Year);
        }
    }
}