using MFMFMSF.Core.Interfaces;
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

        public ViewAllFSReports(INavigationService navigationService, IReportService reportService)
        {
            InitializeComponent();

            NavigationService = navigationService;

            _viewModel = new FSReportViewModel(reportService);

            DataContext = _viewModel;

            Loaded += ViewAllFSReports_Loaded;
        }

        private async void ViewAllFSReports_Loaded(object sender, RoutedEventArgs e)
        {
            await _viewModel.LoadReportsAsync(1, 1000000);
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
