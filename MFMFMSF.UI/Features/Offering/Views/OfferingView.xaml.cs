using MFMFMSF.Core.Interfaces;
using MFMFMSF.UI.Controls;
using MFMFMSF.UI.Features.Offering.ViewModels;
using MFMFMSF.UI.Navigation;
using System.Windows;
using System.Windows.Controls;

namespace MFMFMSF.UI.Features.Offering.Views
{
    /// <summary>
    /// Interaction logic for OfferingView.xaml
    /// </summary>
    public partial class OfferingView : UserControl
    {
        private readonly OfferingViewModel _viewModel;
        public event EventHandler<PeriodChangedEventArgs>? PeriodChanged;

        public OfferingView(INavigationService navigationService, IGivingService givingService, IExpenditureService expenditureService, IGivingCategoryService givingCategoryService)
        {
            InitializeComponent();

            _viewModel = new OfferingViewModel(navigationService, givingService, expenditureService, givingCategoryService);
            DataContext = _viewModel;

            OfferingTableControl.NavigationService = navigationService;
            OfferingTableControl.GivingService = givingService;
            OfferingTableControl.GivingCategoryService = givingCategoryService;

            OfferingTopBar.PeriodChanged += MonthYearPicker_PeriodChanged;

            Loaded += OfferingsView_Loaded;
        }


        private async void MonthYearPicker_PeriodChanged(object? sender, PeriodChangedEventArgs e)
        {
            await _viewModel.SetSelectedPeriodAsync(e.Month, e.Year);
            PeriodChanged?.Invoke(this, e);
        }

        private async void OfferingsView_Loaded(object sender, RoutedEventArgs e)
        {
            await _viewModel.LoadDataAsync();
        }
    }
}
