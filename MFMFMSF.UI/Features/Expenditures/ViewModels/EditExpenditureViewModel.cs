using MFMFMSF.Core.Interfaces;
using MFMFMSF.Core.Models.Expenditures;
using MFMFMSF.UI.Commands;
using MFMFMSF.UI.Navigation;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;

namespace MFMFMSF.UI.Features.Expenditures.ViewModels
{
    public class EditExpenditureViewModel : INotifyPropertyChanged
    {
        private readonly IExpenditureService _expenditureService;
        private readonly INavigationService _navigationService;


        public Guid ExpenditureId { get; }

        public ICommand UpdateExpenditureCommand { get; }


        public EditExpenditureViewModel(Guid expenditureId, IExpenditureService expenditureService, INavigationService navigationService)
        {
            _expenditureService = expenditureService;
            _navigationService = navigationService;
            ExpenditureId = expenditureId;

            UpdateExpenditureCommand = new RelayCommand(async _ => await UpdateExpenditureAsync());
        }


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
        // LOAD
        // =====================================================

        public async Task LoadAsync()
        {
            var expenditure = await _expenditureService.GetByIdAsync(ExpenditureId);

            // Populate the form
            Date = expenditure.Date;
            Amount = expenditure.Amount;
            Summary = expenditure.Summary ?? string.Empty;
        }


        // =====================================================
        // UPDATE EXPENDITURE
        // =====================================================

        private async Task UpdateExpenditureAsync()
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

            var request = new UpdateExpenditureRequest
            {
                Amount = Amount,
                Date = Date.Value,
                Summary = Summary.Trim(),
            };


            await _expenditureService.UpdateAsync(ExpenditureId, request);


            MessageBox.Show(
                "Expenditure updated successfully.",
                "Success",
                MessageBoxButton.OK,
                MessageBoxImage.Information);


            _navigationService.GoBack();
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
