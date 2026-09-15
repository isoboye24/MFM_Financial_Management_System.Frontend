using MFMFMSF.Core.Interfaces;
using MFMFMSF.Core.Models.Givings;
using MFMFMSF.UI.Navigation;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MFMFMSF.UI.Features.Dashboard.ViewModels
{
    public class DashboardViewModel : INotifyPropertyChanged
    {
        private readonly INavigationService _navigationService;
        private readonly IGivingService _givingService;


        // =====================================================
        // SERVICES
        // =====================================================
        public INavigationService NavigationService => _navigationService;
        public IGivingService GivingService => _givingService;


        public DashboardViewModel(INavigationService navigationService, IGivingService givingService)
        {
            _navigationService = navigationService;
            _givingService = givingService;
        }

        public async Task LoadDashboardDataAsync()
        {
            GivingStatistics = await _givingService.GetStatisticsAsync();
        }


        // =====================================================
        // STATISTICS
        // =====================================================
        private GivingStatistics _givingStatistics = new();

        public GivingStatistics GivingStatistics
        {
            get => _givingStatistics;
            private set
            {
                _givingStatistics = value;
                OnPropertyChanged();
            }
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

