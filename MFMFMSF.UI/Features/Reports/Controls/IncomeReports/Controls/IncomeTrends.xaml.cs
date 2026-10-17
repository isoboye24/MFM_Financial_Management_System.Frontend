using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using MFMFMSF.Core.Interfaces;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Controls;
using SkiaSharp;

namespace MFMFMSF.UI.Features.Reports.Controls.IncomeReports.Controls
{
    public partial class IncomeTrends : UserControl, INotifyPropertyChanged
    {
        private readonly IGivingService? _givingService;

        private int _selectedYear = DateTime.Today.Year;
        private int _selectedMonth = DateTime.Today.Month;

        private decimal _offeringsTotal;
        private decimal _tithesTotal;
        private decimal _seedsTotal;
        private decimal _otherIncomeTotal;

        public event PropertyChangedEventHandler? PropertyChanged;

        public IncomeTrends() : this(null!)
        {
        }

        public IncomeTrends(IGivingService givingService)
        {
            InitializeComponent();

            _givingService = givingService;
            DataContext = this;
        }

        public int SelectedYear
        {
            get => _selectedYear;
            private set => SetProperty(ref _selectedYear, value);
        }

        public decimal OfferingsTotal
        {
            get => _offeringsTotal;
            private set => SetProperty(ref _offeringsTotal, value);
        }

        public decimal TithesTotal
        {
            get => _tithesTotal;
            private set => SetProperty(ref _tithesTotal, value);
        }

        public decimal SeedsTotal
        {
            get => _seedsTotal;
            private set => SetProperty(ref _seedsTotal, value);
        }

        public decimal OtherIncomeTotal
        {
            get => _otherIncomeTotal;
            private set => SetProperty(ref _otherIncomeTotal, value);
        }

        public ObservableCollection<ISeries> IncomeSeries { get; } = new();

        public async Task SetSelectedPeriodAsync(int month, int year)
        {
            if (_givingService is null)
            {
                throw new InvalidOperationException(
             "IGivingService was not supplied to IncomeTrends.");
            }
            else
            {
                await LoadYearAsync(year);                
            }
        }

        public Axis[] XAxes { get; } =
        {
            new Axis
            {
                Labels = new[]
                {
                    "Jan", "Feb", "Mar", "Apr",
                    "May", "Jun", "Jul", "Aug",
                    "Sep", "Oct", "Nov", "Dec"
                },
                LabelsPaint = new SolidColorPaint(SKColor.Parse("#68708A")),
                TextSize = 12,
                SeparatorsPaint = new SolidColorPaint(
                    SKColor.Parse("#EDF0F6"))
            }
        };

        public Axis[] YAxes { get; } =
        {
            new Axis
            {
                Name = "Income (€)",
                Labeler = value => $"€{value:N0}",
                LabelsPaint = new SolidColorPaint(SKColor.Parse("#68708A")),
                TextSize = 11,
                MinLimit = 0,
                SeparatorsPaint = new SolidColorPaint(
                    SKColor.Parse("#EDF0F6"))
            }
        };
        

        public async Task LoadYearAsync(int year)
        {
            SelectedYear = year;

            var monthlyTasks = Enumerable.Range(1, 12)
                .Select(async month => new
                {
                    Month = month,
                    Statistics = await _givingService.GetMonthlyStatisticsAsync(month, year)
                });

            var monthlyResults = await Task.WhenAll(monthlyTasks);

            var orderedResults = monthlyResults
                .OrderBy(result => result.Month)
                .ToArray();

            
            var offerings = orderedResults
                .Select(x => (double)x.Statistics.MonthlyOfferings)
                .ToArray();

            var tithes = orderedResults
                .Select(x => (double)x.Statistics.MonthlyTithes)
                .ToArray();

            var seeds = orderedResults
                .Select(x => (double)x.Statistics.MonthlySeeds)
                .ToArray();

            var otherIncome = orderedResults
                .Select(x => (double)x.Statistics.MonthlyOtherIncome)
                .ToArray();

            OfferingsTotal = (decimal)offerings.Sum();
            TithesTotal = (decimal)tithes.Sum();
            SeedsTotal = (decimal)seeds.Sum();
            OtherIncomeTotal = (decimal)otherIncome.Sum();

            IncomeSeries.Clear();

            IncomeSeries.Add(new LineSeries<double>
            {
                Name = "Offerings",
                Values = offerings,
                Stroke = new SolidColorPaint(SKColor.Parse("#6048EE"))
                {
                    StrokeThickness = 3
                },
                Fill = null,
                GeometrySize = 7
            });

            IncomeSeries.Add(new LineSeries<double>
            {
                Name = "Tithes",
                Values = tithes,
                Stroke = new SolidColorPaint(SKColor.Parse("#3784ED"))
                {
                    StrokeThickness = 3
                },
                Fill = null,
                GeometrySize = 7
            });

            IncomeSeries.Add(new LineSeries<double>
            {
                Name = "Seeds (Faith Promise)",
                Values = seeds,
                Stroke = new SolidColorPaint(SKColor.Parse("#28A879"))
                {
                    StrokeThickness = 3
                },
                Fill = null,
                GeometrySize = 7
            });

            IncomeSeries.Add(new LineSeries<double>
            {
                Name = "Other Income",
                Values = otherIncome,
                Stroke = new SolidColorPaint(SKColor.Parse("#E99A2E"))
                {
                    StrokeThickness = 3
                },
                Fill = null,
                GeometrySize = 7
            });

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