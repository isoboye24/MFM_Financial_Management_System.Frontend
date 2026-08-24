using MFMFMSF.UI.Features.Dashboard.Views;
using MFMFMSF.UI.Navigation;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Controls;

namespace MFMFMSF.UI.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private readonly INavigationService _navigation;

        private UserControl _currentView;

        public UserControl CurrentView
        {
            get => _currentView;
            private set
            {
                _currentView = value;
                OnPropertyChanged();
            }
        }


        public MainViewModel(INavigationService navigation)
        {
            _navigation = navigation;

            _navigation.PageChanged += OnPageChanged;

            // Initial page
            CurrentView = new DashboardView();
        }


        private void OnPageChanged(UserControl page)
        {
            CurrentView = page;
        }


        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged(
            [CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(
                this,
                new PropertyChangedEventArgs(propertyName));
        }
    }
}