using MFMFMSF.UI.Controls;
using MFMFMSF.UI.Features.Expenditures.ViewModels;
using MFMFMSF.UI.Features.Reports.ViewModels;
using MFMFMSF.UI.Navigation;
using System.Windows;
using System.Windows.Controls;

namespace MFMFMSF.UI.Features.Reports.Views
{
    /// <summary>
    /// Interaction logic for ViewAllFSReports.xaml
    /// </summary>
    public partial class ViewAllFSReports : UserControl
    {
        private readonly FSReportViewModel _viewModel;

        public ViewAllFSReports(INavigationService navigationService)
        {
            InitializeComponent();

            NavigationService = navigationService;

            _viewModel = new FSReportViewModel();

            DataContext = _viewModel;
        }


        private async void Pagination_PageChanged(object sender, PageChangedEventArgs e)
        {
            await _viewModel.LoadReportsAsync(e.Page, e.PageSize);
        }


        // =====================================================
        // Navigation Service
        // =====================================================

        public INavigationService? NavigationService
        {
            get => (INavigationService?)GetValue(NavigationServiceProperty);
            set => SetValue(NavigationServiceProperty, value);
        }

        public static readonly DependencyProperty NavigationServiceProperty =
            DependencyProperty.Register(
                nameof(NavigationService),
                typeof(INavigationService),
                typeof(ViewAllFSReports),
                new PropertyMetadata(null));
    }
}
