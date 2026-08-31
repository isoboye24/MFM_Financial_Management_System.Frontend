using MFMFMSF.UI.Controls;
using MFMFMSF.UI.Features.Reports.Controls.FinancialSummaryReports;
using System.Windows.Controls;

namespace MFMFMSF.UI.Features.Reports.Views
{
    public partial class ReportsView : UserControl
    {
        public ReportsView()
        {
            InitializeComponent();

            // Show the first report when the page opens
            ReportContent.Content = new FinancialSummaryTabPage();
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
                    //ReportContent.Content = new IncomeReports();
                    break;

                case 2:
                    //ReportContent.Content = new ExpenditureReports();
                    break;

                case 3:
                    //ReportContent.Content = new BudgetReports();
                    break;

                case 4:
                    //ReportContent.Content = new ProjectReports();
                    break;

                case 5:
                    //ReportContent.Content = new CustomReports();
                    break;
            }
        }
    }
}