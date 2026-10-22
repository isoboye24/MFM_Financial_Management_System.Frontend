
using MFMFMSF.Core.Interfaces;
using MFMFMSF.UI.Features.Reports.Controls.IncomeReports.Controls;
using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace MFMFMSF.UI.Features.Reports.Controls.IncomeReports
{
    public partial class IncomeReportsTabPage : UserControl
    {
        public IGivingService GivingService { get; }

        private readonly IncomeTrends _incomeTrends;

        public IncomeReportsTabPage(
            IGivingService givingService,
            int selectedYear)
        {
            InitializeComponent();

            GivingService = givingService;

            _incomeTrends = new IncomeTrends(givingService);

            // Display the chart in the named content grid.
            ContentGrid.Children.Add(_incomeTrends);

            Loaded += async (_, _) =>
            {
                try
                {
                    await _incomeTrends.LoadYearAsync(selectedYear);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(
                        $"Unable to load income trends.\n\n{ex.Message}",
                        "Income Trends",
                        MessageBoxButton.OK,
                        MessageBoxImage.Error);
                }
            };
        }

        public Task SetSelectedPeriodAsync(int month, int year)
        {
            return _incomeTrends.SetSelectedPeriodAsync(month, year);
        }

        public Task LoadYearAsync(int year)
        {
            return _incomeTrends.LoadYearAsync(year);
        }

        public void RefreshChartTheme()
        {
            _incomeTrends.RefreshChartTheme();
        }
    }
}
