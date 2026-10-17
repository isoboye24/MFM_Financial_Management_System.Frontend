using MFMFMSF.Core.Interfaces;
using MFMFMSF.UI.Features.Expenditures.ViewModels;
using MFMFMSF.UI.Navigation;
using System.Windows;
using System.Windows.Controls;

namespace MFMFMSF.UI.Features.Expenditures.Views
{
    /// <summary>
    /// Interaction logic for ViewSingleExpenditure.xaml
    /// </summary>
    public partial class ViewSingleExpenditure : UserControl
    {
        private readonly ViewExpenditureViewModel _viewModel;
        public ViewSingleExpenditure(Guid expenditureId, INavigationService navigationService, IExpenditureService expenditureService)
        {
            InitializeComponent();

            NavigationService = navigationService;

            _viewModel = new ViewExpenditureViewModel(expenditureId, navigationService, expenditureService);

            DataContext = _viewModel;

            Loaded += ViewSingleExpenditure_Loaded;
        }

        private async void ViewSingleExpenditure_Loaded(object sender, RoutedEventArgs e)
        {
            Loaded -= ViewSingleExpenditure_Loaded;

            try
            {
                await _viewModel.LoadAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.Message,
                    "Unable to Load Giving",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
        }


        public INavigationService? NavigationService
        {
            get => (INavigationService?)GetValue(NavigationServiceProperty);
            set => SetValue(NavigationServiceProperty, value);
        }

        public static readonly DependencyProperty NavigationServiceProperty =
            DependencyProperty.Register(
                nameof(NavigationService),
                typeof(INavigationService),
                typeof(ViewSingleExpenditure),
                new PropertyMetadata(null));
    }
}
