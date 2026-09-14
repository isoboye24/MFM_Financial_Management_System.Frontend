using MFMFMSF.Core.Interfaces;
using MFMFMSF.Core.Models.Givings;
using MFMFMSF.UI.Navigation;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MFMFMSF.UI.Features.Givings.ViewModels
{
    public class ViewGivingViewModel : INotifyPropertyChanged
    {
        private readonly INavigationService _navigationService;
        private readonly IGivingCategoryService _givingCategoryService;
        private readonly IGivingService _givingService;

        public Guid GivingId { get; }

        private GivingDetail? _giving;

        public GivingDetail? Giving
        {
            get => _giving;
            private set
            {
                _giving = value;
                OnPropertyChanged();
            }
        }


        public ViewGivingViewModel(Guid givingId, INavigationService navigationService, IGivingCategoryService givingCategoryService, IGivingService givingService)
        {
            GivingId = givingId;

            _navigationService = navigationService;
            _givingCategoryService = givingCategoryService;
            _givingService = givingService;

        }

        public async Task LoadAsync()
        {
            Giving = await _givingService.GetByIdAsync(GivingId);            
        }


        private void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler? PropertyChanged;
    }
}

