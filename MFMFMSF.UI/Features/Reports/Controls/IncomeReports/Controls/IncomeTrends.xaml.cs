
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using MFMFMSF.Core.Interfaces;
using SkiaSharp;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace MFMFMSF.UI.Features.Reports.Controls.IncomeReports.Controls
{
    public partial class IncomeTrends : UserControl, INotifyPropertyChanged
    {
        private readonly IGivingService? _givingService;

        private int _selectedYear = DateTime.Today.Year;

        private double[] _offerings = Array.Empty<double>();
        private double[] _tithes = Array.Empty<double>();
        private double[] _seeds = Array.Empty<double>();
        private double[] _otherIncome = Array.Empty<double>();

        public event PropertyChangedEventHandler? PropertyChanged;

        public IncomeTrends() : this(null!)
        {
        }

        public IncomeTrends(IGivingService givingService)
        {
            InitializeComponent();

            _givingService = givingService;

            InitializeAxes();

            DataContext = this;
        }

        public int SelectedYear
        {
            get => _selectedYear;
            private set => SetProperty(ref _selectedYear, value);
        }

        private decimal _offeringsTotal;
        public decimal OfferingsTotal
        {
            get => _offeringsTotal;
            private set => SetProperty(ref _offeringsTotal, value);
        }

        private decimal _tithesTotal;
        public decimal TithesTotal
        {
            get => _tithesTotal;
            private set => SetProperty(ref _tithesTotal, value);
        }

        private decimal _seedsTotal;
        public decimal SeedsTotal
        {
            get => _seedsTotal;
            private set => SetProperty(ref _seedsTotal, value);
        }

        private decimal _otherIncomeTotal;
        public decimal OtherIncomeTotal
        {
            get => _otherIncomeTotal;
            private set => SetProperty(ref _otherIncomeTotal, value);
        }

        public ObservableCollection<ISeries> IncomeSeries { get; } = new();

        public Axis[] XAxes { get; private set; } = Array.Empty<Axis>();

        public Axis[] YAxes { get; private set; } = Array.Empty<Axis>();

        // Reads a SolidColorBrush from the active WPF theme dictionary
        // and converts its color to SkiaSharp's SKColor.
        private static SKColor GetThemeColor(
            string resourceKey,
            SKColor fallback)
        {
            if (Application.Current?.TryFindResource(resourceKey)
                is SolidColorBrush brush)
            {
                var color = brush.Color;

                return new SKColor(
                    color.R,
                    color.G,
                    color.B,
                    color.A);
            }

            return fallback;
        }

        private static SolidColorPaint CreatePaint(
            string resourceKey,
            SKColor fallback)
        {
            return new SolidColorPaint(GetThemeColor(resourceKey, fallback));
        }


        private void InitializeAxes()
        {
            var labelColor = GetThemeColor(
                "ChartLabelBrush2",
                SKColor.Parse("#C5CAD5"));

            var separatorColor = GetThemeColor(
                "ChartSeparatorBrush2",
                SKColor.Parse("#3A4050"));

            var labelPaint = new SolidColorPaint(labelColor);
            var separatorPaint = new SolidColorPaint(separatorColor);

            XAxes = new[]
            {
        new Axis
        {
            Labels = new[]
            {
                "Jan", "Feb", "Mar", "Apr",
                "May", "Jun", "Jul", "Aug",
                "Sep", "Oct", "Nov", "Dec"
            },

            LabelsPaint = new SolidColorPaint(labelColor),
            TextSize = 12,

            SeparatorsPaint = new SolidColorPaint(separatorColor)
        }
    };

            YAxes = new[]
            {
        new Axis
        {
            Name = "Income (€)",
            NamePaint = new SolidColorPaint(labelColor),
            NameTextSize = 14,

            Labeler = value => $"€{value:N0}",
            LabelsPaint = new SolidColorPaint(labelColor),
            TextSize = 11,

            MinLimit = 0,
            SeparatorsPaint = new SolidColorPaint(separatorColor)
        }
    };

            OnPropertyChanged(nameof(XAxes));
            OnPropertyChanged(nameof(YAxes));
        }


        public async Task SetSelectedPeriodAsync(int month, int year)
        {
            if (_givingService is null)
            {
                throw new InvalidOperationException(
                    "IGivingService was not supplied to IncomeTrends.");
            }

            // Preserve the existing behavior: load the selected year.
            await LoadYearAsync(year);
        }

        public async Task LoadYearAsync(int year)
        {
            if (_givingService is null)
            {
                throw new InvalidOperationException(
                    "IGivingService was not supplied to IncomeTrends.");
            }

            SelectedYear = year;

            var monthlyTasks = Enumerable.Range(1, 12)
                .Select(async month => new
                {
                    Month = month,
                    Statistics = await _givingService
                        .GetMonthlyStatisticsAsync(month, year)
                });

            var monthlyResults = await Task.WhenAll(monthlyTasks);

            var orderedResults = monthlyResults
                .OrderBy(result => result.Month)
                .ToArray();

            _offerings = orderedResults
                .Select(x => (double)x.Statistics.MonthlyOfferings)
                .ToArray();

            _tithes = orderedResults
                .Select(x => (double)x.Statistics.MonthlyTithes)
                .ToArray();

            _seeds = orderedResults
                .Select(x => (double)x.Statistics.MonthlySeeds)
                .ToArray();

            _otherIncome = orderedResults
                .Select(x => (double)x.Statistics.MonthlyOtherIncome)
                .ToArray();

            OfferingsTotal = (decimal)_offerings.Sum();
            TithesTotal = (decimal)_tithes.Sum();
            SeedsTotal = (decimal)_seeds.Sum();
            OtherIncomeTotal = (decimal)_otherIncome.Sum();

            RefreshChartTheme();
        }

        /// <summary>
        /// Rebuilds chart paints using the currently active theme resources.
        /// Call this after switching the application's theme dictionaries.
        /// </summary>
        /// 



        private static LineSeries<double> CreateIncomeSeries(
            string name,
            double[] values,
            string colorResourceKey)
        {
            var textColor = GetThemeColor(
                "ChartLabelBrush",
                SKColors.LightGray);

            return new LineSeries<double>
            {
                Name = name,
                Values = values,

                Stroke = new SolidColorPaint(
                    GetThemeColor(
                        colorResourceKey,
                        GetThemeColor("PrimaryTextBrush", SKColors.White)))
                {
                    StrokeThickness = 3
                },

                Fill = null,
                GeometrySize = 7
            };
        }




        public void RefreshChartTheme()
        {
            InitializeAxes();

            IncomeSeries.Clear();

            IncomeSeries.Add(CreateIncomeSeries(
                IncomeCategoryNames.Offerings,
                _offerings,
                "PrimaryBrush"));

            IncomeSeries.Add(CreateIncomeSeries(
                IncomeCategoryNames.Tithes,
                _tithes,
                "InfoBrush"));

            IncomeSeries.Add(CreateIncomeSeries(
                IncomeCategoryNames.Seeds,
                _seeds,
                "SuccessBrush"));

            IncomeSeries.Add(CreateIncomeSeries(
                IncomeCategoryNames.OtherIncome,
                _otherIncome,
                "WarningBrush"));

            OnPropertyChanged(nameof(IncomeSeries));
        }

        private bool SetProperty<T>(
            ref T field,
            T value,
            [CallerMemberName] string? propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(field, value))
                return false;

            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }

        private void OnPropertyChanged(
            [CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(
                this,
                new PropertyChangedEventArgs(propertyName));
        }
    }
}
