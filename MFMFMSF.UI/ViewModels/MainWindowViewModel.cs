using MFMFMSF.UI.Features.Dashboard.Views;
using System.Windows.Controls;

namespace MFMFMSF.UI.ViewModels
{
    class MainWindowViewModel : ViewModelBase
    {
        private UserControl _currentView;

        public MainWindowViewModel()
        {
            CurrentView = new DashboardView();
        }

        public UserControl CurrentView
        {
            get => _currentView;
            set
            {
                _currentView = value;
                OnPropertyChanged();
            }
        }
    }
}
