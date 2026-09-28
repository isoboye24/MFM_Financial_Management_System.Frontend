using MFMFMSF.Core.Interfaces;
using MFMFMSF.UI.Features.Expenditures.ViewModels;
using MFMFMSF.UI.Navigation;
using System.Windows;
using System.Windows.Controls;

namespace MFMFMSF.UI.Features.Expenditures.Views
{
    /// <summary>
    /// Interaction logic for EditExpenditure.xaml
    /// </summary>
    public partial class EditExpenditure : UserControl
    {
        private readonly EditExpenditureViewModel _viewModel;
        public EditExpenditure(Guid ExpenditureId, IExpenditureService expenditureService, INavigationService navigationService)
        {
            InitializeComponent();

            NavigationService = navigationService;

            _viewModel = new EditExpenditureViewModel(ExpenditureId, expenditureService, navigationService);

            DataContext = _viewModel;

            Loaded += EditExpenditure_Loaded;
        }


        private async void EditExpenditure_Loaded(object sender, RoutedEventArgs e)
        {
            Loaded -= EditExpenditure_Loaded;

            await _viewModel.LoadAsync();
        }


        // =====================================================
        // NAVIGATION SERVICE
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
                typeof(EditExpenditure),
                new PropertyMetadata(null));
    }
}
