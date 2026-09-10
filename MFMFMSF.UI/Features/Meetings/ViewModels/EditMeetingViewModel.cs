using MFMFMSF.Core.Interfaces;
using MFMFMSF.Core.Models.MeetingCategories;
using MFMFMSF.Core.Models.Meetings;
using MFMFMSF.UI.Commands;
using MFMFMSF.UI.Navigation;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;

namespace MFMFMSF.UI.Features.Meetings.ViewModels
{
    public class EditMeetingViewModel : INotifyPropertyChanged
    {
        private readonly IMeetingService _meetingService;
        private readonly IMeetingCategoryService _meetingCategoryService;
        private readonly INavigationService _navigationService;

        public Guid MeetingId { get; }

        public ICommand UpdateMeetingCommand { get; }


        public EditMeetingViewModel(Guid meetingId, IMeetingService meetingService, IMeetingCategoryService meetingCategoryService, INavigationService navigationService)
        {
            MeetingId = meetingId;

            _meetingService = meetingService;
            _meetingCategoryService = meetingCategoryService;
            _navigationService = navigationService;
            UpdateMeetingCommand = new RelayCommand(async _ => await UpdateMeetingAsync());
        }


        // =====================================================
        // FORM PROPERTIES
        // =====================================================

        private DateTime? _date;

        public DateTime? Date
        {
            get => _date;
            set => SetProperty(ref _date, value);
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


        private string _summary = string.Empty;

        public string Summary
        {
            get => _summary;
            set => SetProperty(ref _summary, value);
        }


        // =====================================================
        // CATEGORIES
        // =====================================================

        public ObservableCollection<MeetingCategoryListItem> MeetingCategories { get; } = new();


        private MeetingCategoryListItem? _selectedMeetingCategory;

        public MeetingCategoryListItem? SelectedMeetingCategory
        {
            get => _selectedMeetingCategory;
            set => SetProperty(ref _selectedMeetingCategory, value);
        }


        // =====================================================
        // LOAD
        // =====================================================

        public async Task LoadAsync()
        {
            // First load categories
            var categories = await _meetingCategoryService.GetAllAsync();

            MeetingCategories.Clear();

            foreach (var category in categories)
            {
                MeetingCategories.Add(category);
            }


            // Then load the meeting
            var meeting = await _meetingService.GetByIdAsync(MeetingId);


            // Populate the form
            Date = meeting.Date;

            MessageTitle = meeting.MessageTitle;

            MinisterName = meeting.Minister;

            MaleAttendance = meeting.NoOfMaleAttendance;

            FemaleAttendance = meeting.NoOfFemaleAttendance;

            ChildrenAttendance = meeting.NoOfChildrenAttendance;

            Summary = meeting.Summary ?? string.Empty;


            // Select the existing category
            SelectedMeetingCategory = MeetingCategories.FirstOrDefault(x => x.Id == meeting.MeetingCategoryId);
        }


        // =====================================================
        // UPDATE MEETING
        // =====================================================
        private async Task UpdateMeetingAsync()
        {
            if (Date == null)
            {
                MessageBox.Show(
                    "Please select a date.",
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


            var request = new UpdateMeetingRequest
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


            await _meetingService.UpdateAsync(
                MeetingId,
                request);


            MessageBox.Show(
                "Church service updated successfully.",
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