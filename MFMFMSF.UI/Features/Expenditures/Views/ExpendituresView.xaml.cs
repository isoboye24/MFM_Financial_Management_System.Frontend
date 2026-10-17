using MFMFMSF.Core.Interfaces;
using MFMFMSF.Infrastructure.Service;
using MFMFMSF.UI.Controls;
using MFMFMSF.UI.Features.Expenditures.ViewModels;
using MFMFMSF.UI.Navigation;
using System.Windows;
using System.Windows.Controls;

namespace MFMFMSF.UI.Features.Expenditures.Views
{
    /// <summary>
    /// Interaction logic for ExpendituresView.xaml
    /// </summary>
    public partial class ExpendituresView : UserControl
    {
        private readonly ExpendituresViewModel _viewModel;
        public event EventHandler<PeriodChangedEventArgs>? PeriodChanged;

        public ExpendituresView(INavigationService navigationService, IGivingService givingService, IExpenditureService expenditureService)
        {
            InitializeComponent();
             
            _viewModel = new ExpendituresViewModel(navigationService, givingService, expenditureService);

            DataContext = _viewModel;

            // The chart uses the same registered meeting service.
            ExpendituresTrendControl.SetExpenditureService(expenditureService);

            ExpenditurePageTopBar.PeriodChanged += MonthYearPicker_PeriodChanged;

            Loaded += ExpendituresView_Loaded;
        }

        private async void MonthYearPicker_PeriodChanged(object? sender, PeriodChangedEventArgs e)
        {
            await _viewModel.SetSelectedPeriodAsync(e.Month, e.Year);
            await ExpendituresTrendControl.SetSelectedPeriodAsync(e.Month, e.Year);

            PeriodChanged?.Invoke(this, e);
        }

        private async void ExpendituresView_Loaded(object sender, RoutedEventArgs e)
        {
            await _viewModel.LoadExpendituresAsync();
        }
    }
}
