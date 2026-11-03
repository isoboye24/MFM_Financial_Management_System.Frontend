using MFMFMSF.Core.Interfaces;
using MFMFMSF.Core.Models.Genders;
using MFMFMSF.Core.Models.Positions;
using MFMFMSF.Core.Models.Workers;
using MFMFMSF.UI.Commands;
using MFMFMSF.UI.Features.Workers.Views;
using MFMFMSF.UI.Navigation;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;

namespace MFMFMSF.UI.Features.Workers.ViewModels
{
    public class CreateWorkerViewModel : INotifyPropertyChanged
    {
        private readonly IWorkerService _workerService;
        private readonly IPositionService _positionService;
        private readonly IGenderService _genderService;
        private readonly INavigationService _navigationService;
        private readonly IWorkerNoteService _workerNoteService;

        // =====================================================
        // COMMANDS
        // =====================================================

        public ICommand SaveWorkerCommand { get; }
        public ICommand CancelCommand { get; }


        // =====================================================
        // FORM DATA
        // =====================================================

        private string _firstName = string.Empty;
        public string FirstName
        {
            get => _firstName;
            set => SetProperty(ref _firstName, value);
        }
       
        private string _lastName = string.Empty;
        public string LastName
        {
            get => _lastName;
            set => SetProperty(ref _lastName, value);
        }
        
        private string _address = string.Empty;
        public string Address
        {
            get => _address;
            set => SetProperty(ref _address, value);
        }
       
        private string _phoneNumber = string.Empty;
        public string PhoneNumber
        {
            get => _phoneNumber;
            set => SetProperty(ref _phoneNumber, value);
        }


        // =====================================================
        // POSITION SELECTION
        // =====================================================

        public ObservableCollection<PositionListItem> Positions { get; }
            = new();


        private PositionListItem? _selectedPosition;
        public PositionListItem? SelectedPosition
        {
            get => _selectedPosition;
            set => SetProperty(ref _selectedPosition, value);
        }
        
        
        // =====================================================
        // GENDER SELECTION
        // =====================================================

        public ObservableCollection<GenderListItem> Genders { get; }
            = new();


        private GenderListItem? _selectedGender;
        public GenderListItem? SelectedGender
        {
            get => _selectedGender;
            set => SetProperty(ref _selectedGender, value);
        }

        private string _email = string.Empty;
        public string Email
        {
            get => _email;
            set => SetProperty(ref _email, value);
        }


        public CreateWorkerViewModel(IWorkerService workerService, IPositionService positionService, IGenderService genderService, 
            INavigationService navigationService, IWorkerNoteService workerNoteService)
        {
            _workerService = workerService;
            _positionService = positionService;
            _genderService = genderService;
            _navigationService = navigationService;
            _workerNoteService = workerNoteService;

            SaveWorkerCommand = new RelayCommand(async _ => await SaveWorkerAsync());
            CancelCommand = new RelayCommand(_ => Cancel());
        }


        // =====================================================
        // LOAD POSITIONS
        // =====================================================

        public async Task LoadPositionsAsync()
        {
            var positions = await _positionService.GetAllAsync();

            Positions.Clear();

            foreach (var position in positions)
            {
                Positions.Add(position);
            }
        }
        
        
        // =====================================================
        // LOAD GENDERS
        // =====================================================

        public async Task LoadGendersAsync()
        {
            var genders = await _genderService.GetAllAsync();

            Genders.Clear();

            foreach (var gender in genders)
            {
                Genders.Add(gender);
            }
        }


        // =====================================================
        // SAVE WORKER
        // =====================================================

        private async Task SaveWorkerAsync()
        {
            
            if (string.IsNullOrWhiteSpace(FirstName))
            {
                MessageBox.Show(
                    "Please enter the first name.",
                    "Validation",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);

                return;
            }

            if (string.IsNullOrWhiteSpace(LastName))
            {
                MessageBox.Show(
                    "Please enter the last name.",
                    "Validation",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);

                return;
            }

            if (SelectedPosition == null)
            {
                MessageBox.Show(
                    "Please select a position.",
                    "Validation",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);

                return;
            }
            
            if (SelectedGender == null)
            {
                MessageBox.Show(
                    "Please select a gender.",
                    "Validation",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);

                return;
            }

            var request = new CreateWorkerRequest
            {
                FirstName = FirstName.Trim(),
                LastName = LastName.Trim(),
                Address = string.IsNullOrWhiteSpace(Address) ? null : Address.Trim(),
                PhoneNumber = string.IsNullOrWhiteSpace(PhoneNumber) ? null : PhoneNumber.Trim(),
                PositionId = SelectedPosition.Id,
                GenderId = SelectedGender.Id,
                Email = string.IsNullOrWhiteSpace(Email) ? null : Email.Trim()
            };

            await _workerService.CreateAsync(request);

            MessageBox.Show(
                "Worker created successfully.",
                "Success",
                MessageBoxButton.OK,
                MessageBoxImage.Information);

            ClearForm();
        }


        // =====================================================
        // CANCEL AND NAVIGATE BACK
        // =====================================================

        private void Cancel()
        {
            ClearForm();
            _navigationService.Navigate(new WorkersView(_navigationService, _positionService, _workerService, _genderService, _workerNoteService));
        }

        // =====================================================
        // CLEAR FORM
        // =====================================================

        private void ClearForm()
        {
            SelectedGender = null;
            SelectedPosition = null;
            FirstName = string.Empty;
            LastName = string.Empty;
            Address = string.Empty;
            PhoneNumber = string.Empty;
            Email = string.Empty;
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
