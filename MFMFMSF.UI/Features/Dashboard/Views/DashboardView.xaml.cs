using MFMFMSF.Core.Interfaces;
using MFMFMSF.UI.Features.Dashboard.ViewModels;
using MFMFMSF.UI.Navigation;
using System.Windows;
using System.Windows.Controls;

namespace MFMFMSF.UI.Features.Dashboard.Views
{
    /// <summary>
    /// Interaction logic for DashboardView.xaml
    /// </summary>
    public partial class DashboardView : UserControl
    {
        private readonly DashboardViewModel _viewModel;
        public DashboardView(INavigationService navigationService, IGivingService givingService)
        {
            InitializeComponent();

            _viewModel = new DashboardViewModel(navigationService, givingService);

            DataContext = _viewModel;

            Loaded += DashboardView_Loaded;
        }

        private async void DashboardView_Loaded(object sender, RoutedEventArgs e)
        {
            await _viewModel.LoadDashboardDataAsync();
        }
    }
}
