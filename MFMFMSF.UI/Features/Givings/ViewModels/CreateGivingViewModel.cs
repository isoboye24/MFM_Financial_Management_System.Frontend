using MFMFMSF.Core.Interfaces;
using MFMFMSF.Core.Models.GivingCategories;
using MFMFMSF.Core.Models.Givings;
using MFMFMSF.UI.Commands;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;

namespace MFMFMSF.UI.Features.Givings.ViewModels
{
    class CreateGivingViewModel : INotifyPropertyChanged
    {
        private readonly IGivingCategoryService _givingCategoryService;
        private readonly IGivingService _givingService;

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


        private int _amount;
        public int Amount
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
            set => SetProperty(ref _selectedGivingCategory, value);
        }


        // =====================================================
        // COMMANDS
        // =====================================================

        public ICommand SaveGivingingCommand { get; }


        // =====================================================
        // CONSTRUCTOR
        // =====================================================

        public CreateGivingViewModel(Guid meetingId, IGivingCategoryService givingCategoryService, IGivingService givingService)
        {
            MeetingId = meetingId;
            _givingCategoryService = givingCategoryService;
            _givingService = givingService;

            SaveGivingingCommand = new RelayCommand(async _ => await SaveGivingAsync());
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
            if (Date == null)
            {
                MessageBox.Show(
                    "Please select the date of the giving.",
                    "Validation",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);

                return;
            }

            if (string.IsNullOrWhiteSpace(Amount.ToString()))
            {
                MessageBox.Show(
                    "Please enter the amount.",
                    "Validation",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);

                return;
            }

            if (SelectedGivingCategory == null)
            {
                MessageBox.Show(
                    "Please select a giving category.",
                    "Validation",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);

                return;
            }

            var request = new CreateGivingRequest
            {
                Date = Date.Value,
                Summary = string.IsNullOrWhiteSpace(Summary) ? null : Summary.Trim(),
                Amount = Amount,
                GivingCategoryId = SelectedGivingCategory.Id,
                MeetingId = Guid.NewGuid() // Replace with actual meeting ID if applicable
            };

            await _givingService.CreateAsync(request);

            MessageBox.Show(
                "Church service created successfully.",
                "Success",
                MessageBoxButton.OK,
                MessageBoxImage.Information);

            ClearForm();
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
