
using MFMFMSF.Core.Interfaces;
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using LiveChartsCore.Measure;
using SkiaSharp;
using System;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace MFMFMSF.UI.Features.Reports.Controls.FinancialSummaryReports
{
    public partial class IncomeVsExpenditureTrend : UserControl
    {
        private IGivingService? _givingService;
        private IExpenditureService? _expenditureService;

        private readonly int _currentYear = DateTime.Now.Year;

        public ISeries[] Series { get; private set; } = Array.Empty<ISeries>();
        public Axis[] XAxes { get; private set; } = Array.Empty<Axis>();
        public Axis[] YAxes { get; private set; } = Array.Empty<Axis>();

        private bool _isInitialized;

        public IncomeVsExpenditureTrend()
        {
            InitializeComponent();
            DataContext = this;
            UpdateButtonStyles(true);
        }

        public async Task InitializeAsync( IGivingService givingService, IExpenditureService expenditureService)
        {
            _givingService = givingService ?? throw new ArgumentNullException(nameof(givingService));

            _expenditureService = expenditureService ?? throw new ArgumentNullException(nameof(expenditureService));

            _isInitialized = true;

            await LoadMonthlyDataAsync();
        }

        private async void MonthlyButton_Click(
            object sender,
            RoutedEventArgs e)
        {
            if (!_isInitialized)
                return;

            UpdateButtonStyles(true);
            await LoadMonthlyDataAsync();
        }

        private async void YearlyButton_Click(
            object sender,
            RoutedEventArgs e)
        {
            if (!_isInitialized)
                return;

            UpdateButtonStyles(false);
            await LoadYearlyDataAsync();
        }

        private void UpdateButtonStyles(bool isMonthly)
        {
            if (MonthlyButton == null || YearlyButton == null)
                return;

            var selectedBackground =
                new System.Windows.Media.SolidColorBrush(
                    System.Windows.Media.Color.FromRgb(91, 76, 243));

            var normalBackground =
                System.Windows.Media.Brushes.White;

            var normalForeground =
                new System.Windows.Media.SolidColorBrush(
                    System.Windows.Media.Color.FromRgb(48, 54, 83));

            var normalBorder =
                new System.Windows.Media.SolidColorBrush(
                    System.Windows.Media.Color.FromRgb(232, 234, 242));

            MonthlyButton.Background = isMonthly
                ? selectedBackground : normalBackground;
            MonthlyButton.Foreground = isMonthly
                ? System.Windows.Media.Brushes.White : normalForeground;
            MonthlyButton.BorderBrush = isMonthly
                ? selectedBackground : normalBorder;

            YearlyButton.Background = isMonthly
                ? normalBackground : selectedBackground;
            YearlyButton.Foreground = isMonthly
                ? normalForeground : System.Windows.Media.Brushes.White;
            YearlyButton.BorderBrush = isMonthly
                ? normalBorder : selectedBackground;
        }

        private async Task LoadMonthlyDataAsync()
        {
            if (_givingService == null || _expenditureService == null)
                return;

            try
            {
                MonthlyButton.IsEnabled = false;
                YearlyButton.IsEnabled = false;

                var monthLabels = Enumerable.Range(1, 12)
                    .Select(month => new DateTime(_currentYear, month, 1)
                        .ToString("MMM", CultureInfo.InvariantCulture))
                    .ToArray();

                var incomeValues = new double[12];
                var expenditureValues = new double[12];
                var netIncomeValues = new double[12];

                for (int month = 1; month <= 12; month++)
                {
                    var givingTask = _givingService
                        .GetMonthlyStatisticsAsync(month, _currentYear);

                    var expenditureTask = _expenditureService
                        .GetMonthlyStatisticsAsync(month, _currentYear);

                    await Task.WhenAll(givingTask, expenditureTask);

                    var giving = await givingTask;
                    var expenditure = await expenditureTask;

                    decimal totalIncome =
                        giving.MonthlyOfferings +
                        giving.MonthlyTithes +
                        giving.MonthlySeeds +
                        giving.MonthlyOtherIncome;

                    decimal totalExpenditure =
                        expenditure.MonthlyExpenditures;

                    incomeValues[month - 1] = (double)totalIncome;
                    expenditureValues[month - 1] = (double)totalExpenditure;
                    netIncomeValues[month - 1] =
                        (double)(totalIncome - totalExpenditure);
                }

                UpdateChart(
                    monthLabels,
                    incomeValues,
                    expenditureValues,
                    netIncomeValues);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Unable to load monthly financial data.\n\n{ex.Message}",
                    "Financial Trend",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
            finally
            {
                MonthlyButton.IsEnabled = true;
                YearlyButton.IsEnabled = true;
            }
        }

        private async Task LoadYearlyDataAsync()
        {
            if (_givingService == null || _expenditureService == null)
                return;

            try
            {
                MonthlyButton.IsEnabled = false;
                YearlyButton.IsEnabled = false;

                var givingTask =
                    _givingService.GetAnnualStatisticsAsync(_currentYear);

                var expenditureTask =
                    _expenditureService.GetAnnualStatisticsAsync(_currentYear);

                await Task.WhenAll(givingTask, expenditureTask);

                var giving = await givingTask;
                var expenditure = await expenditureTask;

                decimal totalIncome =
                    giving.AnnualOfferings +
                    giving.AnnualTithes +
                    giving.AnnualSeeds +
                    giving.AnnualOtherIncome;

                decimal totalExpenditure = expenditure.AnnualExpenditures;
                decimal netIncome = totalIncome - totalExpenditure;

                UpdateChart(
                    new[] { _currentYear.ToString() },
                    new[] { (double)totalIncome },
                    new[] { (double)totalExpenditure },
                    new[] { (double)netIncome });
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Unable to load annual financial data.\n\n{ex.Message}",
                    "Financial Trend",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
            finally
            {
                MonthlyButton.IsEnabled = true;
                YearlyButton.IsEnabled = true;
            }
        }

        private void UpdateChart(
            string[] labels,
            double[] incomeValues,
            double[] expenditureValues,
            double[] netIncomeValues)
        {
            var incomeColor = new SKColor(53, 181, 140);
            var expenditureColor = new SKColor(240, 68, 95);
            var netIncomeColor = new SKColor(96, 70, 216);

            var income = new LineSeries<double>
            {
                Name = "Income",
                Values = incomeValues,
                Stroke = new SolidColorPaint(incomeColor)
                {
                    StrokeThickness = 2
                },
                Fill = null,
                GeometrySize = 6,
                GeometryFill = new SolidColorPaint(incomeColor),
                GeometryStroke = null,
                DataLabelsSize = 7,
                DataLabelsPaint = new SolidColorPaint(incomeColor),
                DataLabelsPosition = DataLabelsPosition.Top,
                DataLabelsFormatter =
                    point => $"{point.Coordinate.PrimaryValue:N0} €"
            };

            var expenditure = new LineSeries<double>
            {
                Name = "Expenditure",
                Values = expenditureValues,
                Stroke = new SolidColorPaint(expenditureColor)
                {
                    StrokeThickness = 2
                },
                Fill = null,
                GeometrySize = 6,
                GeometryFill = new SolidColorPaint(expenditureColor),
                GeometryStroke = null,
                DataLabelsSize = 7,
                DataLabelsPaint = new SolidColorPaint(expenditureColor),
                DataLabelsPosition = DataLabelsPosition.Top,
                DataLabelsFormatter =
                    point => $"{point.Coordinate.PrimaryValue:N0} €"
            };

            var netIncome = new ColumnSeries<double>
            {
                Name = "Net Income",
                Values = netIncomeValues,
                Fill = new SolidColorPaint(netIncomeColor),
                Stroke = null,
                MaxBarWidth = 30,
                DataLabelsSize = 7,
                DataLabelsPaint = new SolidColorPaint(netIncomeColor),
                DataLabelsPosition = DataLabelsPosition.Top,
                DataLabelsFormatter =
                    point => $"{point.Coordinate.PrimaryValue:N0} €"
            };

            Series = new ISeries[]
            {
                netIncome,
                income,
                expenditure
            };

            double maximumValue = new[]
                {
                    incomeValues.DefaultIfEmpty(0).Max(),
                    expenditureValues.DefaultIfEmpty(0).Max(),
                    netIncomeValues.DefaultIfEmpty(0).Max()
                }
                .Max();

            XAxes = new[]
            {
                new Axis
                {
                    Labels = labels,
                    TextSize = 8,
                    LabelsPaint = new SolidColorPaint(
                        new SKColor(48, 54, 83)),
                    SeparatorsPaint = null
                }
            };

            YAxes = new[]
            {
                new Axis
                {
                    MinLimit = Math.Min(0, netIncomeValues.Min()),
                    MaxLimit = maximumValue <= 0
                        ? 100
                        : maximumValue * 1.15,
                    TextSize = 8,
                    LabelsPaint = new SolidColorPaint(
                        new SKColor(48, 54, 83)),
                    Labeler = value => value.ToString("N0") + " €",
                    SeparatorsPaint = new SolidColorPaint(
                        new SKColor(235, 237, 243))
                    {
                        StrokeThickness = 1
                    }
                }
            };

            OnChartChanged();
        }

        private void OnChartChanged()
        {
            // Notify LiveCharts that the bound chart properties changed.
            // Reassigning DataContext is unnecessary; refresh the chart bindings.
            var currentSeries = Series;
            var currentXAxes = XAxes;
            var currentYAxes = YAxes;

            DataContext = null;
            DataContext = this;
        }
    }
}
