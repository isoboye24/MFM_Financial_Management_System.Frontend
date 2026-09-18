using MFMFMSF.Core.Interfaces;
using MFMFMSF.UI.Controls;
using MFMFMSF.UI.Features.Seeds.ViewModels;
using MFMFMSF.UI.Features.Tithes.ViewModels;
using MFMFMSF.UI.Navigation;
using System.Windows;
using System.Windows.Controls;

namespace MFMFMSF.UI.Features.Seeds.Views
{
    /// <summary>
    /// Interaction logic for SeedsView.xaml
    /// </summary>
    public partial class SeedsView : UserControl
    {
        private readonly SeedsViewModel _viewModel;
        public event EventHandler<PeriodChangedEventArgs>? PeriodChanged;

        public SeedsView(INavigationService navigationService, IGivingService givingService)
        {
            InitializeComponent();

            _viewModel = new SeedsViewModel(navigationService, givingService);

            DataContext = _viewModel;

            SeedsTopBar.PeriodChanged += MonthYearPicker_PeriodChanged;
            Loaded += SeedsView_Loaded;
        }

        private async void MonthYearPicker_PeriodChanged(object? sender, PeriodChangedEventArgs e)
        {
            await _viewModel.SetSelectedPeriodAsync(e.Month, e.Year);
            PeriodChanged?.Invoke(this, e);
        }

        private async void SeedsView_Loaded(object sender, RoutedEventArgs e)
        {
            await _viewModel.LoadDataAsync();
        }
    }
}
