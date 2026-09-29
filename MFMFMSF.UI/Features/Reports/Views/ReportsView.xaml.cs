using MFMFMSF.Core.Interfaces;
using MFMFMSF.UI.Controls;
using MFMFMSF.UI.Features.Meetings.ViewModels;
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

        public ReportsView(INavigationService navigationService, IGivingService givingService, IExpenditureService expenditureService)
        {
            InitializeComponent();

            _viewModel = new ReportsViewModel(navigationService, givingService, expenditureService);

            DataContext = _viewModel;

            ReportsPageTopBar.PeriodChanged += MonthYearPicker_PeriodChanged;

            Loaded += ReportsView_Loaded;

            // Show the first report when the page opens
            ReportContent.Content = new FinancialSummaryTabPage(); 
        }

        private async void MonthYearPicker_PeriodChanged(object? sender, PeriodChangedEventArgs e)
        {
            await _viewModel.SetSelectedPeriodAsync(e.Month, e.Year);
            PeriodChanged?.Invoke(this, e);
        }

        private async void ReportsView_Loaded(object sender, RoutedEventArgs e)
        {
            await _viewModel.LoadReportsAsync();
        }


        private void PageTabs_TabChanged(
            object? sender,
            TabChangedEventArgs e)
        {
            switch (e.SelectedIndex)
            {
                case 0:
                    ReportContent.Content = new FinancialSummaryTabPage();
                    break;

                case 1:
                    ReportContent.Content = new IncomeReportsTabPage();
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