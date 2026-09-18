using MFMFMSF.Core.Interfaces;
using MFMFMSF.UI.Controls;
using MFMFMSF.UI.Features.OtherIncome.ViewModels;
using MFMFMSF.UI.Navigation;
using System.Windows;
using System.Windows.Controls;

namespace MFMFMSF.UI.Features.OtherIncome.Views
{
    /// <summary>
    /// Interaction logic for OtherIncomeView.xaml
    /// </summary>
    public partial class OtherIncomeView : UserControl
    {
        private readonly OtherIncomeViewModel _viewModel;
        public event EventHandler<PeriodChangedEventArgs>? PeriodChanged;

        public OtherIncomeView(INavigationService navigationService, IGivingService givingService)
        {
            InitializeComponent();

            _viewModel = new OtherIncomeViewModel(navigationService, givingService);

            DataContext = _viewModel;

            OtherIncomeTopBar.PeriodChanged += MonthYearPicker_PeriodChanged;
            Loaded += OtherIncomeView_Loaded;
        }

        private async void MonthYearPicker_PeriodChanged(object? sender, PeriodChangedEventArgs e)
        {
            await _viewModel.SetSelectedPeriodAsync(e.Month, e.Year);
            PeriodChanged?.Invoke(this, e);
        }

        private async void OtherIncomeView_Loaded(object sender, RoutedEventArgs e)
        {
            await _viewModel.LoadDataAsync();
        }
    }
}
