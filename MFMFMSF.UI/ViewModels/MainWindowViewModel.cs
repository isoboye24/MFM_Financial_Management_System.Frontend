using MFMFMSF.UI.Features.Dashboard.Views;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Controls;

namespace MFMFMSF.UI.ViewModels
{
    public class MainWindowViewModel : INotifyPropertyChanged
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
                if (_currentView != value)
                {
                    _currentView = value;
                    OnPropertyChanged();
                }
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}