using MFMFMSF.Core.Interfaces;
using MFMFMSF.Core.Models.Expenditures;
using MFMFMSF.Core.Models.Givings;
using MFMFMSF.UI.Navigation;
using MFMFMSF.UI.Utilities;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Media;

namespace MFMFMSF.UI.Features.Offering.ViewModels
{
    public class OfferingViewModel : INotifyPropertyChanged
    {
        private readonly INavigationService _navigationService;
        private readonly IGivingService _givingService;
        private readonly IExpenditureService _expenditureService;
        private readonly IGivingCategoryService _givingCategoryService;

        public OfferingViewModel(INavigationService navigationService, IGivingService givingService, IExpenditureService expenditureService, IGivingCategoryService givingCategoryService)
        {
            _navigationService = navigationService;
            _givingService = givingService;
            _expenditureService = expenditureService;

            SelectedMonth = DateTime.Now.Month;
            SelectedYear = DateTime.Now.Year;
            _givingCategoryService = givingCategoryService;
        }


        // =====================================================
        // PREVIOUS MONTH STATISTICS
        // =====================================================

        private MonthlyGivingStatistics _previousMonthGivingStatistics = new();

        private MonthlyExpendituresSatistics _previousMonthExpendituresStatistics = new();

        private string _comparisonText = string.Empty;

        public string ComparisonText
        {
            get => _comparisonText;
            private set
            {
                if (_comparisonText == value)
                    return;

                _comparisonText = value;
                OnPropertyChanged();
            }
        }

        // =====================================================
        // TREND INDICATORS
        // =====================================================

        private TrendIndicator _offeringsTrend = new();
        public TrendIndicator OfferingsTrend
        {
            get => _offeringsTrend;
            private set
            {
                _offeringsTrend = value;
                OnPropertyChanged();
            }
        }

        private TrendIndicator _tithesTrend = new();
        public TrendIndicator TithesTrend
        {
            get => _tithesTrend;
            private set
            {
                _tithesTrend = value;
                OnPropertyChanged();
            }
        }

        private TrendIndicator _seedsTrend = new();
        public TrendIndicator SeedsTrend
        {
            get => _seedsTrend;
            private set
            {
                _seedsTrend = value;
                OnPropertyChanged();
            }
        }

        private TrendIndicator _otherIncomeTrend = new();
        public TrendIndicator OtherIncomeTrend
        {
            get => _otherIncomeTrend;
            private set
            {
                _otherIncomeTrend = value;
                OnPropertyChanged();
            }
        }

        private TrendIndicator _expendituresTrend = new();
        public TrendIndicator ExpendituresTrend
        {
            get => _expendituresTrend;
            private set
            {
                _expendituresTrend = value;
                OnPropertyChanged();
            }
        }

        public async Task LoadDataAsync()
        {
            // Calculate the previous month, including January -> December.
            var selectedDate = new DateTime(SelectedYear, SelectedMonth, 1);
            var previousDate = selectedDate.AddMonths(-1);

            // Request current and previous month statistics.
            var currentGivingTask =
                _givingService.GetMonthlyStatisticsAsync(
                    SelectedMonth, SelectedYear);

            var previousGivingTask =
                _givingService.GetMonthlyStatisticsAsync(
                    previousDate.Month, previousDate.Year);

            var currentExpenditureTask =
                _expenditureService.GetMonthlyStatisticsAsync(
                    SelectedMonth, SelectedYear);

            var previousExpenditureTask =
                _expenditureService.GetMonthlyStatisticsAsync(
                    previousDate.Month, previousDate.Year);

            await Task.WhenAll(
                currentGivingTask,
                previousGivingTask,
                currentExpenditureTask,
                previousExpenditureTask);

            MonthlyGivingStatistics = await currentGivingTask;
            _previousMonthGivingStatistics = await previousGivingTask;

            MonthlyExpendituresStatistics = await currentExpenditureTask;
            _previousMonthExpendituresStatistics = await previousExpenditureTask;

            ComparisonText = $"vs {previousDate.ToString("MMM yyyy")}";

            // Income increases are positive; decreases are negative.
            OfferingsTrend = CalculateTrend(
                MonthlyGivingStatistics.MonthlyOfferings,
                _previousMonthGivingStatistics.MonthlyOfferings,
                higherIsBetter: true);

            TithesTrend = CalculateTrend(
                MonthlyGivingStatistics.MonthlyTithes,
                _previousMonthGivingStatistics.MonthlyTithes,
                higherIsBetter: true);

            SeedsTrend = CalculateTrend(
                MonthlyGivingStatistics.MonthlySeeds,
                _previousMonthGivingStatistics.MonthlySeeds,
                higherIsBetter: true);

            OtherIncomeTrend = CalculateTrend(
                MonthlyGivingStatistics.MonthlyOtherIncome,
                _previousMonthGivingStatistics.MonthlyOtherIncome,
                higherIsBetter: true);

            // Increased expenditure is shown in red.
            ExpendituresTrend = CalculateTrend(
                MonthlyExpendituresStatistics.MonthlyExpenditures,
                _previousMonthExpendituresStatistics.MonthlyExpenditures,
                higherIsBetter: false);



            Givings = await _givingService.GetByMonthAndYearAsync(SelectedMonth, SelectedYear, "Offering", 1, 10);
        }


        // =====================================================
        // SELECTED PERIOD
        // =====================================================

        private int _selectedMonth;
        public int SelectedMonth
        {
            get => _selectedMonth;
            private set
            {
                if (_selectedMonth == value)
                    return;

                _selectedMonth = value;
                OnPropertyChanged();
            }
        }

        private int _selectedYear;
        public int SelectedYear
        {
            get => _selectedYear;
            private set
            {
                if (_selectedYear == value)
                    return;

                _selectedYear = value;
                OnPropertyChanged();
            }
        }



        // =====================================================
        // MONTHLY STATISTICS
        // =====================================================
        private MonthlyGivingStatistics _monthlyGivingStatistics = new();

        public MonthlyGivingStatistics MonthlyGivingStatistics
        {
            get => _monthlyGivingStatistics;
            private set
            {
                _monthlyGivingStatistics = value;
                OnPropertyChanged();
            }
        }

        private MonthlyExpendituresSatistics _monthlyExpendituresStatistics = new();

        public MonthlyExpendituresSatistics MonthlyExpendituresStatistics
        {
            get => _monthlyExpendituresStatistics;
            private set
            {
                _monthlyExpendituresStatistics = value;
                OnPropertyChanged();
            }
        }

        // =====================================================
        // CALCULATE MONTH-ON-MONTH CHANGE
        // =====================================================

        private static TrendIndicator CalculateTrend(
            decimal current,
            decimal previous,
            bool higherIsBetter)
        {
            // Avoid division by zero.
            if (previous == 0)
            {
                if (current == 0)
                {
                    return new TrendIndicator
                    {
                        Percentage = "0.0%",
                        TrendArrow = "→",
                        TrendForeground = Brushes.Gray
                    };
                }

                return new TrendIndicator
                {
                    Percentage = "New",
                    TrendArrow = "↑",
                    TrendForeground = higherIsBetter
                        ? Brushes.SeaGreen
                        : Brushes.IndianRed
                };
            }

            var change = ((current - previous) / previous) * 100m;

            if (change == 0)
            {
                return new TrendIndicator
                {
                    Percentage = "0.0%",
                    TrendArrow = "→",
                    TrendForeground = Brushes.Gray
                };
            }

            bool increased = change > 0;
            bool positiveTrend = increased == higherIsBetter;

            return new TrendIndicator
            {
                Percentage = $"{Math.Abs(change):0.0}%",
                TrendArrow = increased ? "↑" : "↓",
                TrendForeground = positiveTrend
                    ? Brushes.SeaGreen
                    : Brushes.IndianRed
            };
        }


        // =====================================================
        // CHANGE PERIOD
        // =====================================================

        public async Task SetSelectedPeriodAsync(int month, int year)
        {
            if (month < 1 || month > 12)
                throw new ArgumentOutOfRangeException(nameof(month));

            if (year < 1)
                throw new ArgumentOutOfRangeException(nameof(year));

            SelectedMonth = month;
            SelectedYear = year;

            await LoadDataAsync();
        }


        // =====================================================
        // OFFERING LIST
        // =====================================================
        private IReadOnlyList<GivingByMonthAndYear> _givings = [];

        public IReadOnlyList<GivingByMonthAndYear> Givings
        {
            get => _givings;
            private set
            {
                _givings = value;
                OnPropertyChanged();
            }
        }


        private void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(
                this,
                new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
