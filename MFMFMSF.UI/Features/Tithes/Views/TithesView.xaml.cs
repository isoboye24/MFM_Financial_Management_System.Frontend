using MFMFMSF.Core.Interfaces;
using MFMFMSF.UI.Controls;
using MFMFMSF.UI.Features.Tithes.ViewModels;
using MFMFMSF.UI.Navigation;
using System.Windows;
using System.Windows.Controls;

namespace MFMFMSF.UI.Features.Tithes.Views
{
    /// <summary>
    /// Interaction logic for TithesView.xaml
    /// </summary>
    public partial class TithesView : UserControl
    {
        private readonly TithesViewModel _viewModel;
        public event EventHandler<PeriodChangedEventArgs>? PeriodChanged;

        public TithesView(INavigationService navigationService, IGivingService givingService, IExpenditureService expenditureService, IGivingCategoryService givingCategoryService)
        {
            InitializeComponent();

            _viewModel = new TithesViewModel(navigationService, givingService, expenditureService, givingCategoryService);

            DataContext = _viewModel;

            TitheTableControl.NavigationService = navigationService;
            TitheTableControl.GivingService = givingService;
            TitheTableControl.GivingCategoryService = givingCategoryService;

            // The chart uses the same registered meeting service.
            TitheTrendControl.SetGivingService(givingService);

            TithesTopBar.PeriodChanged += MonthYearPicker_PeriodChanged;
            Loaded += TithesView_Loaded;
        }

        private async void MonthYearPicker_PeriodChanged(object? sender, PeriodChangedEventArgs e)
        {
            await _viewModel.SetSelectedPeriodAsync(e.Month, e.Year);
            await TitheTrendControl.SetSelectedPeriodAsync(e.Month, e.Year);

            PeriodChanged?.Invoke(this, e);
        }

        private async void TithesView_Loaded(object sender, RoutedEventArgs e)
        {
            await _viewModel.LoadDataAsync();
        }
    }
}
