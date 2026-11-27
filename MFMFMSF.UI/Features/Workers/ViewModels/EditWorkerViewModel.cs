using MFMFMSF.Core.Interfaces;
using MFMFMSF.Core.Models.Genders;
using MFMFMSF.Core.Models.Positions;
using MFMFMSF.Core.Models.Workers;
using MFMFMSF.UI.Commands;
using MFMFMSF.UI.Features.Workers.Views;
using MFMFMSF.UI.Navigation;
using Microsoft.Win32;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace MFMFMSF.UI.Features.Workers.ViewModels
{
    public class EditWorkerViewModel : INotifyPropertyChanged
    {
        private readonly IWorkerService _workerService;
        private readonly INavigationService _navigationService;
        private readonly IPositionService _positionService;
        private readonly IGenderService _genderService;
        private readonly IWorkerNoteService _workerNoteService;

        public Guid WorkerId { get; }

        public ICommand UpdateWorkerCommand { get; }
        public ICommand CancelCommand { get; }
        public ICommand SelectImageCommand { get; }
        public ICommand RemoveImageCommand { get; }


        public EditWorkerViewModel(Guid workerId, IWorkerService workerService, INavigationService navigationService, IPositionService positionService, 
            IGenderService genderService, IWorkerNoteService workerNoteService)
        {
            WorkerId = workerId;

            _workerService = workerService;
            _navigationService = navigationService;
            _positionService = positionService;
            _genderService = genderService;
            _workerNoteService = workerNoteService;

            UpdateWorkerCommand = new RelayCommand(async _ => await UpdateWorkerAsync());
            CancelCommand = new RelayCommand(_ => Cancel());
            SelectImageCommand = new RelayCommand(_ => SelectImage());
            RemoveImageCommand = new RelayCommand(_ => RemoveImage());
        }

        // =====================================================
        // FORM DATA PROPERTIES
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

        public ObservableCollection<PositionListItem> Positions { get; } = new();


        private PositionListItem? _selectedPosition;
        public PositionListItem? SelectedPosition
        {
            get => _selectedPosition;
            set => SetProperty(ref _selectedPosition, value);
        }


        // =====================================================
        // GENDER SELECTION
        // =====================================================

        public ObservableCollection<GenderListItem> Genders { get; } = new();


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


        // =====================================================
        // WORKER IMAGE
        // =====================================================

        private string? _currentImagePath;
        private bool _isImageRemoved;

        private string? _selectedImagePath;
        public string? SelectedImagePath
        {
            get => _selectedImagePath;
            set => SetProperty(ref _selectedImagePath, value);
        }

        private ImageSource? _selectedImagePreview;
        public ImageSource? SelectedImagePreview
        {
            get => _selectedImagePreview;
            set => SetProperty(ref _selectedImagePreview, value);
        }

        // =====================================================
        // LOAD DATA
        // =====================================================

        public async Task LoadAsync()
        {
            // Load positions
            var positions = await _positionService.GetAllAsync();

            Positions.Clear();

            foreach (var position in positions)
            {
                Positions.Add(position);
            }
            
            // Load genders
            var genders = await _genderService.GetAllAsync();

            Genders.Clear();

            foreach (var gender in genders)
            {
                Genders.Add(gender);
            }


            // Then load the worker
            var worker = await _workerService.GetByIdAsync(WorkerId);

            // Load image if selected
            _currentImagePath = worker.ImagePath;
            _isImageRemoved = false;
            SelectedImagePath = null;

            if (!string.IsNullOrWhiteSpace(_currentImagePath))
            {
                var imageUrl = new Uri(
                    App.HttpClient.BaseAddress!,
                    $"uploads/Members/{Uri.EscapeDataString(_currentImagePath)}");

                var bitmap = new BitmapImage();

                bitmap.BeginInit();
                bitmap.CacheOption = BitmapCacheOption.OnLoad;
                bitmap.UriSource = imageUrl;
                bitmap.EndInit();
                bitmap.Freeze();

                SelectedImagePreview = bitmap;
            }
            else
            {
                SelectedImagePreview = null;
            }


            // Populate the form
            FirstName = worker.FirstName;
            LastName = worker.LastName;
            Address = worker.Address;
            PhoneNumber = worker.PhoneNumber;
            Email = worker.Email;

            // Select the existing position and gender
            SelectedPosition = Positions.FirstOrDefault(x => x.Id == worker.PositionId);
            SelectedGender = Genders.FirstOrDefault(x => x.Id == worker.GenderId);
        }

        // =====================================================
        // UPDATE WORKER
        // =====================================================
        private async Task UpdateWorkerAsync()
        {
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


            string? imagePath;

            if (_isImageRemoved)
            {
                imagePath = null;
            }
            else if (!string.IsNullOrWhiteSpace(SelectedImagePath))
            {
                imagePath = await _workerService.UploadImageAsync(SelectedImagePath);
            }
            else
            {
                imagePath = _currentImagePath;
            }


            var request = new UpdateWorkerRequest
            {
                FirstName = FirstName.Trim(),
                LastName = LastName.Trim(),
                Address = string.IsNullOrWhiteSpace(Address) ? null : Address.Trim(),
                PhoneNumber = string.IsNullOrWhiteSpace(PhoneNumber) ? null : PhoneNumber.Trim(),
                Email = string.IsNullOrWhiteSpace(Email) ? null : Email.Trim(),
                PositionId = SelectedPosition.Id,
                GenderId = SelectedGender.Id,
                ImagePath = imagePath
            };


            await _workerService.UpdateAsync(WorkerId, request);

            MessageBox.Show(
                "Church worker updated successfully.",
                "Success",
                MessageBoxButton.OK,
                MessageBoxImage.Information);

            _navigationService.GoBack();
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
        // SELECT IMAGE
        // =====================================================
        private void SelectImage()
        {
            var dialog = new OpenFileDialog
            {
                Title = "Select Worker Photograph",
                Filter = "Image Files|*.jpg;*.jpeg;*.png",
                Multiselect = false
            };

            if (dialog.ShowDialog() != true)
                return;

            SelectedImagePath = dialog.FileName;
            _isImageRemoved = false;

            var bitmap = new BitmapImage();

            bitmap.BeginInit();
            bitmap.CacheOption = BitmapCacheOption.OnLoad;
            bitmap.UriSource = new Uri(dialog.FileName);
            bitmap.EndInit();
            bitmap.Freeze();

            SelectedImagePreview = bitmap;
        }


        // =====================================================
        // REMOVE IMAGE
        // =====================================================
        private void RemoveImage()
        {
            SelectedImagePath = null;
            SelectedImagePreview = null;
            _isImageRemoved = true;
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
