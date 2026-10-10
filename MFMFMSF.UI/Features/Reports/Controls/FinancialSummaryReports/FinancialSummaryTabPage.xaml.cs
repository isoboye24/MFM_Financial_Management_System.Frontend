using MFMFMSF.Core.Interfaces;
using MFMFMSF.UI.Navigation;
using System.Windows.Controls;

namespace MFMFMSF.UI.Features.Reports.Controls.FinancialSummaryReports
{
    /// <summary>
    /// Interaction logic for FinancialSummaryTabPage.xaml
    /// </summary>
    public partial class FinancialSummaryTabPage : UserControl
    {
        public FinancialSummaryTabPage(INavigationService navigationService, IReportService reportService)
        {
            InitializeComponent();

            RecentReportsControl.Initialize(navigationService, reportService);
        }
    }
}
