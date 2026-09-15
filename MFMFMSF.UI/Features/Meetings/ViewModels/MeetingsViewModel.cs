using MFMFMSF.Core.Interfaces;
using MFMFMSF.Core.Models.Givings;
using MFMFMSF.Core.Models.Meetings;
using MFMFMSF.UI.Commands;
using MFMFMSF.UI.Features.Meetings.Views;
using MFMFMSF.UI.Navigation;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace MFMFMSF.UI.Features.Meetings.ViewModels
{
    public class MeetingsViewModel : INotifyPropertyChanged
    {
        private readonly INavigationService _navigationService;
        private readonly IMeetingCategoryService _meetingCategoryService;
        private readonly IMeetingService _meetingService;
        private readonly IGivingCategoryService _givingCategoryService;
        private readonly IGivingService _givingService;

        public ObservableCollection<MeetingListItem> Meetings { get; } = new();

        public ICommand AddMeetingCommand { get; }

        // =====================================================
        // SERVICES
        // =====================================================
        public INavigationService NavigationService => _navigationService;
        public IMeetingCategoryService MeetingCategoryService => _meetingCategoryService;
        public IMeetingService MeetingService => _meetingService;
        public IGivingCategoryService GivingCategoryService => _givingCategoryService;
        public IGivingService GivingService => _givingService;



        public MeetingsViewModel(INavigationService navigationService, IMeetingCategoryService meetingCategoryService, IMeetingService meetingService, 
            IGivingCategoryService givingCategoryService, IGivingService givingService)
        {
            _navigationService = navigationService;
            _meetingCategoryService = meetingCategoryService;
            _meetingService = meetingService;
            _givingCategoryService = givingCategoryService;
            _givingService = givingService;

            AddMeetingCommand =  new RelayCommand(_ => AddMeeting());

            SelectedMonth = DateTime.Now.Month;
            SelectedYear = DateTime.Now.Year;
        }


        private void AddMeeting()
        {
            _navigationService.Navigate(new CreateMeeting(_navigationService, _meetingCategoryService, _meetingService, _givingCategoryService, _givingService));
        }

        public async Task LoadMeetingsAsync()
        {
            var meetings = await _meetingService.GetAllAsync();

            Meetings.Clear();

            foreach (var meeting in meetings)
            {
                Meetings.Add(meeting);
            }

            MonthlyGivingStatistics = await _givingService.GetMonthlyStatisticsAsync(SelectedMonth, SelectedYear);
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

            await LoadMeetingsAsync();
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