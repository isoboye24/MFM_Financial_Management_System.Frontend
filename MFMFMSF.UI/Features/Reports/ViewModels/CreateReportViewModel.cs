using MFMFMSF.Core.Interfaces;
using MFMFMSF.Core.Models.Reports.Create;
using MFMFMSF.UI.Commands;
using MFMFMSF.UI.Navigation;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;

namespace MFMFMSF.UI.Features.Reports.ViewModels
{
    public class CreateReportViewModel : INotifyPropertyChanged
    {
        private readonly IReportService _reportService;
        private readonly INavigationService _navigationService;

        // =====================================================
        // FORM DATA
        // =====================================================

        private decimal? _openingBalance;

        public decimal? OpeningBalance
        {
            get => _openingBalance;
            set => SetProperty(ref _openingBalance, value);
        }


        private int _month;

        public int Month
        {
            get => _month;
            set => SetProperty(ref _month, value);
        }


        private int _year;

        public int Year
        {
            get => _year;
            set => SetProperty(ref _year, value);
        }

        private bool _isGenerating;

        public bool IsGenerating
        {
            get => _isGenerating;
            private set => SetProperty(ref _isGenerating, value);
        }


        public ICommand GenerateReportCommand { get; }

        public CreateReportViewModel(
            IReportService reportService,
            INavigationService navigationService)
        {
            _reportService = reportService;
            _navigationService = navigationService;

            GenerateReportCommand =
                new RelayCommand(async _ => await GenerateReportAsync());
        }


        // =====================================================
        // GENERATE REPORT
        // =====================================================

        private async Task GenerateReportAsync()
        {
            if (IsGenerating)
                return;

            if (Month == 0)
            {
                MessageBox.Show(
                    "Please select the month.",
                    "Validation",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);

                return;
            }

            if (Year == 0)
            {
                MessageBox.Show(
                    "Please select the year.",
                    "Validation",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);

                return;
            }

            if (OpeningBalance == null)
            {
                MessageBox.Show(
                    "Please enter the opening balance.",
                    "Validation",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);

                return;
            }


            try
            {
                IsGenerating = true;

                var request = new CreateMonthlyReportRequest
                {
                    Month = Month,
                    Year = Year,
                    OpeningBalance = OpeningBalance.Value
                };

                await _reportService.CreateAsync(request);


                // -------------------------------------------------
                // SHOW GENERATING ANIMATION
                // -------------------------------------------------

                await Task.Delay(2000);

                _navigationService.GoBack();
            }
            catch (Exception ex)
            {
                IsGenerating = false;

                MessageBox.Show(
                    ex.Message,
                    "Error",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
        }


        // =====================================================
        // PROPERTY CHANGED
        // =====================================================

        private void SetProperty<T>(
            ref T field,
            T value,
            [CallerMemberName] string? propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(field, value))
                return;

            field = value;

            PropertyChanged?.Invoke(
                this,
                new PropertyChangedEventArgs(propertyName));
        }


        public event PropertyChangedEventHandler? PropertyChanged;
    }
}