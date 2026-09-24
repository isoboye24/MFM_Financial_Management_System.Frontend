using MFMFMSF.Core.Interfaces;
using MFMFMSF.Core.Models.Expenditures;
using MFMFMSF.UI.Commands;
using MFMFMSF.UI.Features.Expenditures.Views;
using MFMFMSF.UI.Navigation;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using static SkiaSharp.HarfBuzz.SKShaper;

namespace MFMFMSF.UI.Features.Expenditures.ViewModels
{
    public class ExpendituresViewModel
    {
        private readonly INavigationService _navigationService;
        private readonly IExpenditureService _expenditureService;

        public ICommand AddExpenditureCommand { get; }


        public INavigationService NavigationService => _navigationService;
        public IExpenditureService ExpenditureService => _expenditureService;


        public ExpendituresViewModel(INavigationService navigationService, IExpenditureService expenditureService)
        {
            _navigationService = navigationService;
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
            var result = await _expenditureService
        .GetByMonthAndYearAsync(
            SelectedMonth,
            SelectedYear,
            1,
            10);

    //MessageBox.Show(
    //    $"Records received by WPF: {result.Count}");

    Expenditures = result;
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
        //private MonthlyExpendituresSatistics _monthlyExpendituresSatistics = new();

        //public MonthlyExpendituresSatistics MonthlyExpendituresSatistics
        //{
        //    get => _monthlyExpendituresSatistics;
        //    private set
        //    {
        //        _monthlyExpendituresSatistics = value;
        //        OnPropertyChanged();
        //    }
        //}


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
