using MFMFMSF.Core.Interfaces;
using MFMFMSF.Core.Models.GivingCategories;
using MFMFMSF.Core.Models.Givings;
using MFMFMSF.UI.Commands;
using MFMFMSF.UI.Features.Meetings.Views;
using MFMFMSF.UI.Navigation;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;

namespace MFMFMSF.UI.Features.Givings.ViewModels
{
    public class EditGivingViewModel : INotifyPropertyChanged
    {
        private readonly IGivingCategoryService _givingCategoryService;
        private readonly IGivingService _givingService;
        private readonly INavigationService _navigationService;

        // =====================================================
        // MEETING
        // =====================================================

        public Guid GivingId { get; }

        public ICommand UpdateGivingCommand { get; }


        public EditGivingViewModel(Guid givingId, IGivingCategoryService givingCategoryService, IGivingService givingService, INavigationService navigationService)
        {
            GivingId = givingId;

            _givingCategoryService = givingCategoryService;
            _givingService = givingService;
            _navigationService = navigationService;

            UpdateGivingCommand = new RelayCommand(async _ => await UpdateGivingAsync());
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
        
        
        private Guid _meetingId;

        public Guid MeetingId
        {
            get => _meetingId;
            set => SetProperty(ref _meetingId, value);
        }


        // =====================================================
        // GIVING CATEGORIES
        // =====================================================

        public ObservableCollection<GivingCategoryListItem> GivingCategories { get; } = new();


        private GivingCategoryListItem? _selectedGivingCategory;

        public GivingCategoryListItem? SelectedGivingCategory
        {
            get => _selectedGivingCategory;
            set => SetProperty(ref _selectedGivingCategory, value);
        }


        // =====================================================
        // LOAD
        // =====================================================

        public async Task LoadAsync()
        {
            // First load categories
            var categories = await _givingCategoryService.GetAllAsync();

            GivingCategories.Clear();

            foreach (var category in categories)
            {
                GivingCategories.Add(category);
            }


            // Then load the giving
            var giving = await _givingService.GetByIdAsync(GivingId);


            // Populate the form
            Date = giving.Date;

            Amount = giving.Amount;

            Summary = giving.Summary ?? string.Empty;

            MeetingId = giving.MeetingId;

            
            // Select the existing category
            SelectedGivingCategory = categories.FirstOrDefault(x => x.Id == giving.CategoryId);
        }


        // =====================================================
        // UPDATE GIVING
        // =====================================================

        private async Task UpdateGivingAsync()
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

            var request = new UpdateGivingRequest
            {
                Amount = Amount,
                Date = Date.Value,
                Summary = Summary.Trim(),
                MeetingId = MeetingId,
                CategoryId = SelectedGivingCategory.Id
            };


            await _givingService.UpdateAsync(GivingId, request);


            MessageBox.Show(
                "Giving updated successfully.",
                "Success",
                MessageBoxButton.OK,
                MessageBoxImage.Information);

            var previousPage = _navigationService.PreviousPage;

            _navigationService.GoBack();

            if (previousPage is ViewMeeting viewMeeting)
            {
                await viewMeeting.RefreshAsync();
            }
        }


        // =====================================================
        // PROPERTY CHANGED
        // =====================================================

        private void SetProperty<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(field, value))
                return;

            field = value;

            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
