using MFMFMSF.Core.Interfaces;
using MFMFMSF.UI.Navigation;
using MFMFMSF.UI.Features.Reports.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace MFMFMSF.UI.Features.Reports.Controls.FinancialSummaryReports
{
    public partial class RecentReports : UserControl
    {
        private RecentReportsViewModel? _viewModel;

        public RecentReports()
        {
            InitializeComponent();
        }

        public void Initialize(
            INavigationService navigationService,
            IReportService reportService)
        {
            _viewModel = new RecentReportsViewModel(
                navigationService,
                reportService);

            DataContext = _viewModel;

            Loaded -= RecentReports_Loaded;
            Loaded += RecentReports_Loaded;
        }

        private async void RecentReports_Loaded(
            object sender,
            RoutedEventArgs e)
        {
            if (_viewModel != null)
            {
                Loaded -= RecentReports_Loaded;
                await _viewModel.LoadRecentReportsAsync();
            }
        }
    }
}