using MFMFMSF.Core.Interfaces;
using MFMFMSF.Core.Models.Expenditures;
using MFMFMSF.Core.Models.Givings;
using MFMFMSF.UI.Commands;
using MFMFMSF.UI.Features.Expenditures.Views;
using MFMFMSF.UI.Navigation;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace MFMFMSF.UI.Features.Expenditures.ViewModels
{
    public class ExpendituresViewModel : INotifyPropertyChanged
    {
        private readonly INavigationService _navigationService;
        private readonly IGivingService _givingService;
        private readonly IExpenditureService _expenditureService;

        public ICommand AddExpenditureCommand { get; }


        public INavigationService NavigationService => _navigationService;
        public IGivingService GivingService => _givingService;
        public IExpenditureService ExpenditureService => _expenditureService;


        public ExpendituresViewModel(INavigationService navigationService, IGivingService givingService, IExpenditureService expenditureService)
        {
            _navigationService = navigationService;
            _givingService = givingService;
            _expenditureService = expenditureService;

            AddExpenditureCommand = new RelayCommand(_ => AddExpenditure());

            SelectedMonth = DateTime.Now.Month;
            SelectedYear = DateTime.Now.Year;
        }

        private void AddExpenditure()
        {
            _navigationService.Navigate(new CreateExpenditure(_navigationService, _expenditureService));
        }

        public async Task LoadExpendituresAsync()
        {
            MonthlyGivingStatistics = await _givingService.GetMonthlyStatisticsAsync(SelectedMonth, SelectedYear);
            MonthlyExpendituresStatistics = await _expenditureService.GetMonthlyStatisticsAsync(SelectedMonth, SelectedYear);

            Expenditures = await _expenditureService.GetByMonthAndYearAsync(SelectedMonth, SelectedYear, 1, 10);
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

            await LoadExpendituresAsync();
        }


        // =====================================================
        // EXPENDITURE LIST
        // =====================================================
        private IReadOnlyList<ExpendituresByMonthAndYear> _expenditures = [];

        public IReadOnlyList<ExpendituresByMonthAndYear> Expenditures
        {
            get => _expenditures;
            private set
            {
                _expenditures = value;
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
