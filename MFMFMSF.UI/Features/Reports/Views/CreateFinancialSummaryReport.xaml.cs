using MFMFMSF.Core.Interfaces;
using MFMFMSF.UI.Features.Reports.ViewModels;
using MFMFMSF.UI.Navigation;
using System.Windows;
using System.Windows.Controls;

namespace MFMFMSF.UI.Features.Reports.Views
{
    /// <summary>
    /// Interaction logic for CreateFinancialSummaryReport.xaml
    /// </summary>
    public partial class CreateFinancialSummaryReport : UserControl
    {
        private readonly CreateReportViewModel _viewModel;
        public CreateFinancialSummaryReport(IReportService reportService, INavigationService navigationService)
        {
            InitializeComponent();

            NavigationService = navigationService;

            _viewModel = new CreateReportViewModel(reportService, navigationService);

            DataContext = _viewModel;
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
                typeof(CreateFinancialSummaryReport),
                new PropertyMetadata(null));
    }
}
