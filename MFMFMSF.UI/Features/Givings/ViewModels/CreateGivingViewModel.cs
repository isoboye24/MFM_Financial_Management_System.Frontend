using MFMFMSF.Core.Interfaces;
using MFMFMSF.Core.Models.GivingCategories;
using MFMFMSF.Core.Models.Givings;
using MFMFMSF.UI.Commands;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;

namespace MFMFMSF.UI.Features.Givings.ViewModels
{
    public class CreateGivingViewModel : INotifyPropertyChanged
    {
        private readonly IGivingCategoryService _givingCategoryService;
        private readonly IGivingService _givingService;

        // =====================================================
        // MEETING
        // =====================================================

        public Guid MeetingId { get; }


        // =====================================================
        // FORM DATA
        // =====================================================

        private DateTime? _date;

        public DateTime? Date
        {
            get => _date;
            set => SetProperty(ref _date, value);
        }


        private string _summary = string.Empty;

        public string Summary
        {
            get => _summary;
            set => SetProperty(ref _summary, value);
        }


        private decimal _amount;

        public decimal Amount
        {
            get => _amount;
            set => SetProperty(ref _amount, value);
        }


        // =====================================================
        // GIVING CATEGORIES
        // =====================================================

        public ObservableCollection<GivingCategoryListItem> GivingCategories { get; }
            = new();


        private GivingCategoryListItem? _selectedGivingCategory;

        public GivingCategoryListItem? SelectedGivingCategory
        {
            get => _selectedGivingCategory;
            set => SetProperty(
                ref _selectedGivingCategory,
                value);
        }


        // =====================================================
        // COMMAND
        // =====================================================

        public ICommand SaveGivingCommand { get; }


        // =====================================================
        // CONSTRUCTOR
        // =====================================================

        public CreateGivingViewModel(
            Guid meetingId,
            IGivingCategoryService givingCategoryService,
            IGivingService givingService)
        {
            MeetingId = meetingId;

            _givingCategoryService = givingCategoryService;
            _givingService = givingService;

            SaveGivingCommand =
                new RelayCommand(
                    async _ => await SaveGivingAsync());
        }


        // =====================================================
        // LOAD GIVING CATEGORIES
        // =====================================================

        public async Task LoadGivingCategoriesAsync()
        {
            var categories =
                await _givingCategoryService.GetAllAsync();

            GivingCategories.Clear();

            foreach (var category in categories)
            {
                GivingCategories.Add(category);
            }
        }


        // =====================================================
        // SAVE GIVING
        // =====================================================

        private async Task SaveGivingAsync()
        {
            // DATE
            if (Date == null)
            {
                MessageBox.Show(
                    "Please select the date of the giving.",
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
                    "Please enter a summary for the giving.",
                    "Validation",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);

                return;
            }


            // CATEGORY
            if (SelectedGivingCategory == null)
            {
                MessageBox.Show(
                    "Please select a giving category.",
                    "Validation",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);

                return;
            }


            try
            {
                var request = new CreateGivingRequest
                {
                    Amount = Amount,
                    Date = Date.Value,
                    Summary = Summary.Trim(),
                    MeetingId = MeetingId,
                    CategoryId = SelectedGivingCategory.Id
                };


                await _givingService.CreateAsync(request);


                MessageBox.Show(
                    "Giving created successfully.",
                    "Success",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information);


                ClearForm();
            }
            catch (HttpRequestException ex)
            {
                MessageBox.Show(
        $"Giving could not be created.\n\n" +
        $"Message: {ex.Message}\n" +
        $"Status Code: {ex.StatusCode}",
        "Create Giving Failed",
        MessageBoxButton.OK,
        MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.Message,
                    "Unable to Create Giving",
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
            SelectedGivingCategory = null;
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