using MFMFMSF.Core.Interfaces;
using MFMFMSF.UI.Features.Expenditures.ViewModels;
using MFMFMSF.UI.Navigation;
using System.Windows;
using System.Windows.Controls;

namespace MFMFMSF.UI.Features.Expenditures.Views
{
    /// <summary>
    /// Interaction logic for CreateExpenditure.xaml
    /// </summary>
    public partial class CreateExpenditure : UserControl
    {
        private readonly CreateExpenditureViewModel _viewModel;

        public CreateExpenditure(INavigationService navigationService, IExpenditureService expenditureService)
        {
            InitializeComponent();

            NavigationService = navigationService;

            _viewModel = new CreateExpenditureViewModel(expenditureService);

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
                typeof(CreateExpenditure),
                new PropertyMetadata(null));
    }
}
