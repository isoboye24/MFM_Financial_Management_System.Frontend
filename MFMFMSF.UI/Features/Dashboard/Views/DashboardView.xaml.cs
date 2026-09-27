using MFMFMSF.Core.Interfaces;
using MFMFMSF.UI.Controls;
using MFMFMSF.UI.Features.Dashboard.ViewModels;
using MFMFMSF.UI.Navigation;
using System.Windows;
using System.Windows.Controls;

namespace MFMFMSF.UI.Features.Dashboard.Views
{
    public partial class DashboardView : UserControl
    {
        private readonly DashboardViewModel _viewModel;

        public DashboardView(INavigationService navigationService, IGivingService givingService, IExpenditureService expenditureService)
        {
            InitializeComponent();

            _viewModel = new DashboardViewModel(navigationService, givingService, expenditureService);
            DataContext = _viewModel;

            DashboardTopWelcomeBar.PeriodChanged += DashboardTopWelcomeBar_PeriodChanged;

            Loaded += DashboardView_Loaded;
        }

        private async void DashboardView_Loaded(object sender, RoutedEventArgs e)
        {
            await _viewModel.LoadDashboardDataAsync();
        }

        private async void DashboardTopWelcomeBar_PeriodChanged(object? sender, PeriodChangedEventArgs e)
        {
            await _viewModel.SetSelectedPeriodAsync(e.Month, e.Year);
        }
    }
}