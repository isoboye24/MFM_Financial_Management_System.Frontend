using MFMFMSF.Core.Interfaces;
using MFMFMSF.UI.Controls;
using MFMFMSF.UI.Features.Reports.Controls;
using MFMFMSF.UI.Features.Reports.Controls.ExpenditureReports;
using MFMFMSF.UI.Features.Reports.Controls.FinancialSummaryReports;
using MFMFMSF.UI.Features.Reports.Controls.IncomeReports;
using MFMFMSF.UI.Features.Reports.Controls.ProjectReports;
using MFMFMSF.UI.Features.Reports.ViewModels;
using MFMFMSF.UI.Navigation;
using System.Windows;
using System.Windows.Controls;

namespace MFMFMSF.UI.Features.Reports.Views
{
    public partial class ReportsView : UserControl
    {
        private readonly ReportsViewModel _viewModel;
        public event EventHandler<PeriodChangedEventArgs>? PeriodChanged;

        private readonly INavigationService _navigationService;
        private readonly IGivingService _givingService;
        private readonly IExpenditureService _expenditureService;
        private readonly IReportService _reportService;

        private int _selectedMonth = DateTime.Now.Month;
        private int _selectedYear = DateTime.Now.Year;

        public ReportsView(INavigationService navigationService, IGivingService givingService, IExpenditureService expenditureService, IReportService reportService)
        {
            InitializeComponent();
            _navigationService = navigationService;
            _givingService = givingService;
            _expenditureService = expenditureService;
            _reportService = reportService;

            _viewModel = new ReportsViewModel(_navigationService, _givingService, _expenditureService, _reportService);

            DataContext = _viewModel;

            ReportsPageTopBar.PeriodChanged += MonthYearPicker_PeriodChanged;

            Loaded += ReportsView_Loaded;

            // Show the first report when the page opens
            ReportContent.Content = new FinancialSummaryTabPage(_navigationService, _reportService, _givingService, _expenditureService);  
        }

        public void RefreshCurrentReportTheme()
        {
            if (ReportContent.Content is IncomeReportsTabPage incomePage)
            {
                incomePage.RefreshChartTheme();
            }
        }

        private async void MonthYearPicker_PeriodChanged(object? sender, PeriodChangedEventArgs e)
        {
            _selectedMonth = e.Month;
            _selectedYear = e.Year;

            await _viewModel.SetSelectedPeriodAsync(e.Month, e.Year);

            if (ReportContent.Content is IncomeReportsTabPage incomePage)
            {
                await incomePage.SetSelectedPeriodAsync(_selectedMonth, _selectedYear);
            }

            PeriodChanged?.Invoke(this, e);
        }

        private async void ReportsView_Loaded(object sender, RoutedEventArgs e)
        {
            await _viewModel.LoadReportsAsync();
        }


        private async void PageTabs_TabChanged(object? sender, TabChangedEventArgs e)
        {
            switch (e.SelectedIndex)
            {
                case 0:
                    ReportContent.Content = new FinancialSummaryTabPage(_navigationService, _reportService, _givingService, _expenditureService);
                    break;

                case 1:
                    var incomePage = new IncomeReportsTabPage(_givingService, _selectedYear);
                    ReportContent.Content = incomePage;

                    await incomePage.SetSelectedPeriodAsync(_selectedMonth, _selectedYear);
                    break;

                case 2:
                    ReportContent.Content = new ExpenditureReportsTabPage();
                    break;

                case 3:
                    ReportContent.Content = new ProjectReportsTabPage();
                    break;

                case 4:
                    //ReportContent.Content = new BudgetReports();
                    break;

                case 5:
                    //ReportContent.Content = new CustomReports();
                    break;
            }
        }
    }
}