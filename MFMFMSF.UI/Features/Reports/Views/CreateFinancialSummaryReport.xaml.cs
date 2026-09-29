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
        public CreateFinancialSummaryReport(INavigationService navigationService)
        {
            InitializeComponent();

            NavigationService = navigationService;
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
