using MFMFMSF.Core.Interfaces;
using MFMFMSF.Core.Models.Givings;
using MFMFMSF.UI.Navigation;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MFMFMSF.UI.Features.Tithes.ViewModels
{
    public class TithesViewModel : INotifyPropertyChanged
    {
        private readonly INavigationService _navigationService;
        private readonly IGivingService _givingService;

        public TithesViewModel(INavigationService navigationService, IGivingService givingService)
        {
            _navigationService = navigationService;
            _givingService = givingService;

            SelectedMonth = DateTime.Now.Month;
            SelectedYear = DateTime.Now.Year;
        }


        public async Task LoadDataAsync()
        {
            MonthlyGivingStatistics = await _givingService.GetMonthlyStatisticsAsync(SelectedMonth, SelectedYear);

            Givings = await _givingService.GetByMonthAndYearAsync(SelectedMonth, SelectedYear, "Tithe", 1, 10);
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
        // TITHE LIST
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
