
using MFMFMSF.Core.Interfaces;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace MFMFMSF.UI.Features.Dashboard.Controls
{
    public partial class IncomeVsExpenditure : UserControl,
        INotifyPropertyChanged
    {
        private readonly IGivingService _givingService;
        private readonly IExpenditureService _expenditureService;

        private bool _isUpdatingPeriod;
        private bool _isLoading;

        private decimal _totalIncome;
        private decimal _totalExpenditure;
        private decimal _netBalance;

        private double _incomeBarHeight;
        private double _expenditureBarHeight;
        private double _netBalanceBarHeight;

        private Thickness _incomeLabelMargin;
        private Thickness _expenditureLabelMargin;
        private Thickness _netBalanceLabelMargin;

        private string _axisLabel1 = "0";
        private string _axisLabel2 = "0";
        private string _axisLabel3 = "0";
        private string _axisLabel4 = "0";

        private string _balanceStatus = "Loading...";
        private Brush _balanceStatusBrush = Brushes.Gray;

        public event PropertyChangedEventHandler? PropertyChanged;

        public decimal TotalIncome
        {
            get => _totalIncome;
            private set
            {
                if (_totalIncome == value) return;
                _totalIncome = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(TotalIncomeLabel));
            }
        }

        public decimal TotalExpenditure
        {
            get => _totalExpenditure;
            private set
            {
                if (_totalExpenditure == value) return;
                _totalExpenditure = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(TotalExpenditureLabel));
            }
        }

        public decimal NetBalance
        {
            get => _netBalance;
            private set
            {
                if (_netBalance == value) return;
                _netBalance = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(NetBalanceLabel));
                OnPropertyChanged(nameof(BalanceStatus));
                OnPropertyChanged(nameof(BalanceStatusBrush));
            }
        }

        public string TotalIncomeLabel => FormatMoney(TotalIncome);

        public string TotalExpenditureLabel => FormatMoney(TotalExpenditure);

        public string NetBalanceLabel => FormatMoney(NetBalance);

        public double IncomeBarHeight
        {
            get => _incomeBarHeight;
            private set
            {
                _incomeBarHeight = value;
                OnPropertyChanged();
            }
        }

        public double ExpenditureBarHeight
        {
            get => _expenditureBarHeight;
            private set
            {
                _expenditureBarHeight = value;
                OnPropertyChanged();
            }
        }

        public double NetBalanceBarHeight
        {
            get => _netBalanceBarHeight;
            private set
            {
                _netBalanceBarHeight = value;
                OnPropertyChanged();
            }
        }

        public Thickness IncomeLabelMargin
        {
            get => _incomeLabelMargin;
            private set
            {
                _incomeLabelMargin = value;
                OnPropertyChanged();
            }
        }

        public Thickness ExpenditureLabelMargin
        {
            get => _expenditureLabelMargin;
            private set
            {
                _expenditureLabelMargin = value;
                OnPropertyChanged();
            }
        }

        public Thickness NetBalanceLabelMargin
        {
            get => _netBalanceLabelMargin;
            private set
            {
                _netBalanceLabelMargin = value;
                OnPropertyChanged();
            }
        }

        public string AxisLabel1
        {
            get => _axisLabel1;
            private set { _axisLabel1 = value; OnPropertyChanged(); }
        }

        public string AxisLabel2
        {
            get => _axisLabel2;
            private set { _axisLabel2 = value; OnPropertyChanged(); }
        }

        public string AxisLabel3
        {
            get => _axisLabel3;
            private set { _axisLabel3 = value; OnPropertyChanged(); }
        }

        public string AxisLabel4
        {
            get => _axisLabel4;
            private set { _axisLabel4 = value; OnPropertyChanged(); }
        }

        public string BalanceStatus
        {
            get => _balanceStatus;
            private set { _balanceStatus = value; OnPropertyChanged(); }
        }

        public Brush BalanceStatusBrush
        {
            get => _balanceStatusBrush;
            private set { _balanceStatusBrush = value; OnPropertyChanged(); }
        }

        public decimal ExpenditureRatio =>
            TotalIncome > 0
                ? TotalExpenditure / TotalIncome * 100
                : 0;

        public string ExpenditureRatioLabel =>
            $"{ExpenditureRatio:N1}%";

        public string ExpenditureRatioDescription =>
            TotalIncome <= 0
                ? "No income recorded"
                : TotalExpenditure > TotalIncome
                    ? "Expenditure exceeds income"
                    : $"{FormatMoney(TotalIncome - TotalExpenditure)} remaining";

        public double ExpenditureRatioProgress =>
            TotalIncome <= 0
                ? 0
                : (double)Math.Clamp(ExpenditureRatio, 0, 100);

        public IncomeVsExpenditure(IGivingService givingService, IExpenditureService expenditureService)
        {
            InitializeComponent();

            _givingService = givingService
                ?? throw new ArgumentNullException(nameof(givingService));

            _expenditureService = expenditureService
                ?? throw new ArgumentNullException(nameof(expenditureService));

            DataContext = this;

            Loaded += IncomeVsExpenditure_Loaded;
        }

        private async void IncomeVsExpenditure_Loaded(
            object sender,
            RoutedEventArgs e)
        {
            Loaded -= IncomeVsExpenditure_Loaded;

            await LoadMonthlyAsync(DateTime.Now.Month, DateTime.Now.Year);
        }

        private async void PeriodSelector_SelectionChanged(
            object sender,
            SelectionChangedEventArgs e)
        {
            if (!IsLoaded || _isUpdatingPeriod || _isLoading)
                return;

            var selectedPeriod =
                (PeriodSelector.SelectedItem as ComboBoxItem)?
                .Tag?.ToString();

            var now = DateTime.Now;

            switch (selectedPeriod)
            {
                case "LastMonth":
                    var previousMonth = now.AddMonths(-1);
                    await LoadMonthlyAsync(
                        previousMonth.Month,
                        previousMonth.Year);
                    break;

                case "ThisYear":
                    await LoadAnnualAsync(now.Year);
                    break;

                case "ThisMonth":
                    await LoadMonthlyAsync(now.Month, now.Year);
                    break;
            }
        }

        /// <summary>
        /// Called by DashboardView when its month/year selector changes.
        /// </summary>
        public async Task SetSelectedPeriodAsync(int month, int year)
        {
            if (month < 1 || month > 12)
                throw new ArgumentOutOfRangeException(nameof(month));

            _isUpdatingPeriod = true;

            try
            {
                // The header can select any month, not just the
                // three relative periods available in this dropdown.
                PeriodSelector.SelectedIndex = -1;
            }
            finally
            {
                _isUpdatingPeriod = false;
            }

            await LoadMonthlyAsync(month, year);
        }

        private async Task LoadMonthlyAsync(int month, int year)
        {
            if (_isLoading) return;

            _isLoading = true;

            try
            {
                var incomeTask =
                    _givingService.GetMonthlyStatisticsAsync(month, year);

                var expenditureTask =
                    _expenditureService.GetMonthlyStatisticsAsync(month, year);

                await Task.WhenAll(incomeTask, expenditureTask);

                var income = await incomeTask;
                var expenditure = await expenditureTask;

                var totalIncome =
                    income.MonthlyOfferings +
                    income.MonthlyTithes +
                    income.MonthlySeeds +
                    income.MonthlyOtherIncome;

                UpdateValues(totalIncome, expenditure.MonthlyExpenditures);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Unable to load financial statistics for {month:D2}/{year}.\n\n{ex.Message}",
                    "Income vs Expenditure",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
            finally
            {
                _isLoading = false;
            }
        }

        private async Task LoadAnnualAsync(int year)
        {
            if (_isLoading) return;

            _isLoading = true;

            try
            {
                var incomeTask =
                    _givingService.GetAnnualStatisticsAsync(year);

                var expenditureTask =
                    _expenditureService.GetAnnualStatisticsAsync(year);

                await Task.WhenAll(incomeTask, expenditureTask);

                var income = await incomeTask;
                var expenditure = await expenditureTask;

                var totalIncome =
                    income.AnnualOfferings +
                    income.AnnualTithes +
                    income.AnnualSeeds +
                    income.AnnualOtherIncome;

                UpdateValues(totalIncome, expenditure.AnnualExpenditures);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Unable to load financial statistics for {year}.\n\n{ex.Message}",
                    "Income vs Expenditure",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
            finally
            {
                _isLoading = false;
            }
        }

        private void UpdateValues(decimal income, decimal expenditure)
        {
            TotalIncome = income;
            TotalExpenditure = expenditure;
            NetBalance = income - expenditure;

            OnPropertyChanged(nameof(ExpenditureRatio));
            OnPropertyChanged(nameof(ExpenditureRatioLabel));
            OnPropertyChanged(nameof(ExpenditureRatioDescription));
            OnPropertyChanged(nameof(ExpenditureRatioProgress));

            BalanceStatus = NetBalance >= 0
                ? "Income covers expenditure"
                : "Expenditure exceeds income";

            BalanceStatusBrush = NetBalance >= 0
                ? new SolidColorBrush(Color.FromRgb(32, 165, 107))
                : new SolidColorBrush(Color.FromRgb(239, 91, 131));

            // Use the same scale for all bars. The net balance is
            // displayed by magnitude, while its sign remains in the label.
            var maximum = Math.Max(
                Math.Max(income, expenditure),
                Math.Abs(NetBalance));

            var axisMaximum = maximum <= 0
                ? 4m
                : Math.Ceiling(maximum / 4m);

            AxisLabel1 = FormatAxis(axisMaximum);
            AxisLabel2 = FormatAxis(axisMaximum * 2);
            AxisLabel3 = FormatAxis(axisMaximum * 3);
            AxisLabel4 = FormatAxis(axisMaximum * 4);

            const double maxBarHeight = 120;

            IncomeBarHeight = ScaleBar(income, maximum, maxBarHeight);
            ExpenditureBarHeight = ScaleBar(expenditure, maximum, maxBarHeight);
            NetBalanceBarHeight = ScaleBar(
                Math.Abs(NetBalance), maximum, maxBarHeight);

            // Position each value just above its corresponding bar.
            IncomeLabelMargin = LabelMargin(IncomeBarHeight);
            ExpenditureLabelMargin = LabelMargin(ExpenditureBarHeight);
            NetBalanceLabelMargin = LabelMargin(NetBalanceBarHeight);
        }

        private static Thickness LabelMargin(double barHeight) =>
            new(0, 0, 0, barHeight + 4);

        private static double ScaleBar(
            decimal value,
            decimal maximum,
            double maxHeight)
        {
            if (maximum <= 0 || value <= 0)
                return 0;

            return (double)(value / maximum) * maxHeight;
        }

        private static string FormatAxis(decimal value)
        {
            if (value >= 1_000_000m)
                return $"{value / 1_000_000m:0.#}M";

            if (value >= 1_000m)
                return $"{value / 1_000m:0.#}K";

            return value.ToString("0");
        }

        private static string FormatMoney(decimal amount) =>
            amount.ToString("#,##0.00") + " €";

        private void OnPropertyChanged(
            [CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(
                this,
                new PropertyChangedEventArgs(propertyName));
        }
    }
}
