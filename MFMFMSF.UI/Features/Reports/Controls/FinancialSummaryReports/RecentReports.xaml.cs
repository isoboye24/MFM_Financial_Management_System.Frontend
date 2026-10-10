using MFMFMSF.Core.Interfaces;
using MFMFMSF.UI.Controls;
using MFMFMSF.UI.Features.Reports.ViewModels;
using MFMFMSF.UI.Navigation;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace MFMFMSF.UI.Features.Reports.Controls.FinancialSummaryReports
{
    /// <summary>
    /// Interaction logic for RecentReports.xaml
    /// </summary>
    public partial class RecentReports : UserControl
    {
        public event EventHandler<PeriodChangedEventArgs>? PeriodChanged;

        public RecentReports()
        {
            InitializeComponent();
        }

        public void Initialize(INavigationService navigationService, IReportService reportService)
        {
            DataContext = new RecentReportsViewModel(navigationService, reportService);
        }
    }
}
