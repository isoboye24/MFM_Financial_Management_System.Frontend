using MFMFMSF.Core.Interfaces;
using MFMFMSF.UI.Navigation;
using System.Windows.Controls;

namespace MFMFMSF.UI.Features.Reports.Controls.FinancialSummaryReports
{
    public partial class FinancialSummaryTabPage : UserControl
    {
        public FinancialSummaryTabPage(INavigationService navigationService, IReportService reportService, IGivingService givingService, IExpenditureService expenditureService)
        {
            InitializeComponent();

            RecentReportsControl.Initialize(navigationService, reportService);

            Loaded += async (_, _) =>
            {
                await IncomeVsExpenditureTrendControl.InitializeAsync(givingService, expenditureService);
            };
        }
    }
}
