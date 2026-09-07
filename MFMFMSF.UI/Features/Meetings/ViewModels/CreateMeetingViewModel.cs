using MFMFMSF.Core.Interfaces;
using MFMFMSF.Core.Models;
using MFMFMSF.UI.Commands;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;

namespace MFMFMSF.UI.Features.Meetings.ViewModels
{
    public class CreateMeetingViewModel : INotifyPropertyChanged
    {
        private readonly IMeetingCategoryService _meetingCategoryService;
        private readonly IMeetingService _meetingService;

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


        private string _messageTitle = string.Empty;
        public string MessageTitle
        {
            get => _messageTitle;
            set => SetProperty(ref _messageTitle, value);
        }


        private string _ministerName = string.Empty;
        public string MinisterName
        {
            get => _ministerName;
            set => SetProperty(ref _ministerName, value);
        }


        private int _maleAttendance;
        public int MaleAttendance
        {
            get => _maleAttendance;
            set => SetProperty(ref _maleAttendance, value);
        }


        private int _femaleAttendance;
        public int FemaleAttendance
        {
            get => _femaleAttendance;
            set => SetProperty(ref _femaleAttendance, value);
        }


        private int _childrenAttendance;
        public int ChildrenAttendance
        {
            get => _childrenAttendance;
            set => SetProperty(ref _childrenAttendance, value);
        }


        // =====================================================
        // MEETING CATEGORIES
        // =====================================================

        public ObservableCollection<MeetingCategoryListItem> MeetingCategories { get; }
            = new();


        private MeetingCategoryListItem? _selectedMeetingCategory;
        public MeetingCategoryListItem? SelectedMeetingCategory
        {
            get => _selectedMeetingCategory;
            set => SetProperty(ref _selectedMeetingCategory, value);
        }


        // =====================================================
        // COMMANDS
        // =====================================================

        public ICommand SaveMeetingCommand { get; }


        // =====================================================
        // CONSTRUCTOR
        // =====================================================

        public CreateMeetingViewModel(
            IMeetingCategoryService meetingCategoryService,
            IMeetingService meetingService)
        {
            _meetingCategoryService = meetingCategoryService;
            _meetingService = meetingService;

            SaveMeetingCommand =
                new RelayCommand(async _ => await SaveMeetingAsync());
        }


        // =====================================================
        // LOAD MEETING CATEGORIES
        // =====================================================

        public async Task LoadMeetingCategoriesAsync()
        {
            var categories =
                await _meetingCategoryService.GetAllAsync();

            MeetingCategories.Clear();

            foreach (var category in categories)
            {
                MeetingCategories.Add(category);
            }
        }


        // =====================================================
        // SAVE MEETING
        // =====================================================

        private async Task SaveMeetingAsync()
        {
            if (Date == null)
            {
                MessageBox.Show(
                    "Please select the date of the meeting.",
                    "Validation",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);

                return;
            }

            if (string.IsNullOrWhiteSpace(MessageTitle))
            {
                MessageBox.Show(
                    "Please enter the message title.",
                    "Validation",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);

                return;
            }

            if (string.IsNullOrWhiteSpace(MinisterName))
            {
                MessageBox.Show(
                    "Please enter the minister name.",
                    "Validation",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);

                return;
            }

            if (SelectedMeetingCategory == null)
            {
                MessageBox.Show(
                    "Please select a meeting category.",
                    "Validation",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);

                return;
            }

            var request = new CreateMeetingRequest
            {
                MessageTitle = MessageTitle.Trim(),
                Date = Date.Value,
                Summary = string.IsNullOrWhiteSpace(Summary)
                    ? null
                    : Summary.Trim(),
                Minister = MinisterName.Trim(),
                NoOfMaleAttendance = MaleAttendance,
                NoOfFemaleAttendance = FemaleAttendance,
                NoOfChildrenAttendance = ChildrenAttendance,
                MeetingCategoryId = SelectedMeetingCategory.Id
            };

            await _meetingService.CreateAsync(request);

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
            SelectedMeetingCategory = null;
            Summary = string.Empty;
            MessageTitle = string.Empty;
            MinisterName = string.Empty;
            MaleAttendance = 0;
            FemaleAttendance = 0;
            ChildrenAttendance = 0;
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