using MFMFMSF.Core.Interfaces;
using MFMFMSF.Core.Models.Expenditures;
using MFMFMSF.UI.Commands;
using System.ComponentModel;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;

namespace MFMFMSF.UI.Features.Expenditures.ViewModels
{
    public class CreateExpenditureViewModel
    {
        private readonly IExpenditureService _expenditureService;


        // =====================================================
        // FORM DATA
        // =====================================================

        private DateTime? _date;

        public DateTime? Date
        {
            get => _date;
            set => SetProperty(ref _date, value);
        }


        private decimal _amount;

        public decimal Amount
        {
            get => _amount;
            set => SetProperty(ref _amount, value);
        }


        private string _summary = string.Empty;

        public string Summary
        {
            get => _summary;
            set => SetProperty(ref _summary, value);
        }


        // =====================================================
        // COMMAND
        // =====================================================

        public ICommand SaveExpenditureCommand { get; }


        // =====================================================
        // CONSTRUCTOR
        // =====================================================

        public CreateExpenditureViewModel(IExpenditureService expenditureService)
        {
            _expenditureService = expenditureService;

            SaveExpenditureCommand = new RelayCommand(async _ => await SaveExpenditureAsync());
        }


        // =====================================================
        // SAVE GIVING
        // =====================================================

        private async Task SaveExpenditureAsync()
        {
            // DATE
            if (Date == null)
            {
                MessageBox.Show(
                    "Please select the date of the expenditure.",
                    "Validation",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);

                return;
            }


            // AMOUNT
            if (Amount <= 0)
            {
                MessageBox.Show(
                    "Please enter an amount greater than zero.",
                    "Validation",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);

                return;
            }


            // SUMMARY
            if (string.IsNullOrWhiteSpace(Summary))
            {
                MessageBox.Show(
                    "Please enter a summary for the expenditure.",
                    "Validation",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);

                return;
            }

            try
            {
                var request = new CreateExpenditureRequest
                {
                    Amount = Amount,
                    Date = Date.Value,
                    Summary = Summary.Trim(),
                };


                await _expenditureService.CreateAsync(request);


                MessageBox.Show(
                    "Expenditure created successfully.",
                    "Success",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information);


                ClearForm();
            }
            catch (HttpRequestException ex)
            {
                MessageBox.Show(
                    $"Expenditure could not be created.\n\n" +
                    $"Message: {ex.Message}\n" +
                    $"Status Code: {ex.StatusCode}",
                    "Create Expenditure Failed",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.Message,
                    "Unable to Create Expenditure",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
        }


        // =====================================================
        // CLEAR FORM
        // =====================================================

        private void ClearForm()
        {
            Date = null;
            Summary = string.Empty;
            Amount = 0;
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
