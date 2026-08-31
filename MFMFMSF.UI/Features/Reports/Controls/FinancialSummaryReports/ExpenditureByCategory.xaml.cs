using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using SkiaSharp;
using System.Windows.Controls;

namespace MFMFMSF.UI.Features.Reports.Controls.FinancialSummaryReports
{
    /// <summary>
    /// Interaction logic for ExpenditureByCategory.xaml
    /// </summary>
    public partial class ExpenditureByCategory : UserControl
    {
        public ISeries[] Series { get; set; }

        public ExpenditureByCategory()
        {
            InitializeComponent();

            CreateChart();

            DataContext = this;
        }

        private void CreateChart()
        {
            var offerings = new PieSeries<double>
            {
                Name = "Ophanage",
                Values = new double[] { 62450 },
                Fill = new SolidColorPaint(
                    new SKColor(31, 174, 137)),
                Stroke = null,
                InnerRadius = 32
            };

            var tithes = new PieSeries<double>
            {
                Name = "Church Rent",
                Values = new double[] { 38700 },
                Fill = new SolidColorPaint(
                    new SKColor(48, 130, 232)),
                Stroke = null,
                InnerRadius = 32
            };

            var seeds = new PieSeries<double>
            {
                Name = "Workers",
                Values = new double[] { 22650 },
                Fill = new SolidColorPaint(
                    new SKColor(255, 157, 46)),
                Stroke = null,
                InnerRadius = 32
            };

            var otherIncome = new PieSeries<double>
            {
                Name = "Refreshment",
                Values = new double[] { 17820 },
                Fill = new SolidColorPaint(
                    new SKColor(255, 174, 105)),
                Stroke = null,
                InnerRadius = 32
            };

            var projects = new PieSeries<double>
            {
                Name = "Miscelleneous",
                Values = new double[] { 6700 },
                Fill = new SolidColorPaint(
                    new SKColor(238, 91, 125)),
                Stroke = null,
                InnerRadius = 32
            };

            Series = new ISeries[]
            {
                offerings,
                tithes,
                seeds,
                otherIncome,
                projects
            };
        }
    }
}
