using MFMFMSF.UI.Commands;
using MFMFMSF.UI.Features.Reports.Views;
using MFMFMSF.UI.Navigation;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace MFMFMSF.UI.Features.Reports.ViewModels
{
    public class RecentReportsViewModel : INotifyPropertyChanged
    {
        private readonly INavigationService _navigationService;

        public ICommand ViewAllReportsCommand { get; }

        public INavigationService NavigationService => _navigationService;

        public RecentReportsViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;

            ViewAllReportsCommand = new RelayCommand(_ => ViewAllReports());
        }


        private void ViewAllReports()
        {
            _navigationService.Navigate(new ViewAllFSReports());
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


