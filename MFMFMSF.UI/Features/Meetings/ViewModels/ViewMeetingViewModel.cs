using MFMFMSF.Core.Interfaces;
using MFMFMSF.Core.Models.Givings;
using MFMFMSF.Core.Models.Meetings;
using MFMFMSF.UI.Commands;
using MFMFMSF.UI.Features.Givings.Views;
using MFMFMSF.UI.Navigation;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace MFMFMSF.UI.Features.Meetings.ViewModels
{
    public class ViewMeetingViewModel : INotifyPropertyChanged
    {
        private readonly IMeetingService _meetingService;
        private readonly INavigationService _navigationService;
        private readonly IGivingCategoryService _givingCategoryService;
        private readonly IGivingService _givingService;

        public ICommand AddGivingCommand { get; }

        public ObservableCollection<GivingListItem> Givings { get; } = new();

        public Guid MeetingId { get; }

        private MeetingDetail? _meeting;

        public MeetingDetail? Meeting
        {
            get => _meeting;
            private set
            {
                _meeting = value;
                OnPropertyChanged();
            }
        }

        public ViewMeetingViewModel(
            Guid meetingId,
            INavigationService navigationService,
            IMeetingService meetingService,
            IGivingCategoryService givingCategoryService,
            IGivingService givingService)
        {
            MeetingId = meetingId;

            _navigationService = navigationService;
            _meetingService = meetingService;
            _givingCategoryService = givingCategoryService;
            _givingService = givingService;

            AddGivingCommand = new RelayCommand(_ => AddGiving());

            Givings.CollectionChanged += Givings_CollectionChanged;
        }



        // =========================================================
        // GIVING TOTALS
        // =========================================================

        public decimal TotalTithes => Givings.Where(x => string.Equals(x.CategoryName,"Tithe", StringComparison.OrdinalIgnoreCase)).Sum(x => x.Amount);
        public decimal TotalOfferings => Givings.Where(x => string.Equals(x.CategoryName, "Offering", StringComparison.OrdinalIgnoreCase)).Sum(x => x.Amount);
        public decimal TotalSeeds => Givings.Where(x => string.Equals(x.CategoryName, "Seed", StringComparison.OrdinalIgnoreCase)).Sum(x => x.Amount);
        public decimal TotalOtherIncome => Givings.Where(x => string.Equals(x.CategoryName, "Other Income", StringComparison.OrdinalIgnoreCase)).Sum(x => x.Amount);



        // =========================================================
        // LOAD MEETING DATA
        // =========================================================
        public async Task LoadAsync()
        {
            Meeting = await _meetingService.GetByIdAsync(MeetingId);

            var givings = await _givingService.GetByMeetingIdAsync(MeetingId);

            Givings.Clear();

            foreach (var giving in givings)
            {
                Givings.Add(giving);
            }
        }



        // =========================================================
        // GIVINGS COLLECTION CHANGED
        // =========================================================

        private void Givings_CollectionChanged(
            object? sender,
            NotifyCollectionChangedEventArgs e)
        {
            OnPropertyChanged(nameof(TotalTithes));
            OnPropertyChanged(nameof(TotalOfferings));
            OnPropertyChanged(nameof(TotalSeeds));
            OnPropertyChanged(nameof(TotalOtherIncome));
        }

        // =========================================================
        // Refresh Meeting Data
        // =========================================================
        public async Task RefreshAsync()
        {
            await LoadAsync();
        }




        // =====================================================
        // ADD GIVING
        // =====================================================

        private void AddGiving()
        {
            _navigationService.Navigate(
                new CreateGiving(
                    MeetingId,
                    _navigationService,
                    _givingCategoryService,
                    _givingService));
        }

        private void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler? PropertyChanged;
    }
}