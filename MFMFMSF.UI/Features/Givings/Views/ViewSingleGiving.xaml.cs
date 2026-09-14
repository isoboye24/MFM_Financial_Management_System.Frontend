using MFMFMSF.Core.Interfaces;
using MFMFMSF.UI.Features.Givings.ViewModels;
using MFMFMSF.UI.Navigation;
using System.Windows;
using System.Windows.Controls;


namespace MFMFMSF.UI.Features.Givings.Views
{
    /// <summary>
    /// Interaction logic for ViewSingleGiving.xaml
    /// </summary>
    public partial class ViewSingleGiving : UserControl
    {
        private readonly ViewGivingViewModel _viewModel;

        public ViewSingleGiving(Guid givingId, INavigationService navigationService, IGivingCategoryService givingCategoryService, IGivingService givingService)
        {
            InitializeComponent();

            NavigationService = navigationService;

            _viewModel = new ViewGivingViewModel(givingId, navigationService, givingCategoryService, givingService);

            DataContext = _viewModel;

            Loaded += ViewSingleGiving_Loaded;
        }

        private async void ViewSingleGiving_Loaded(object sender, RoutedEventArgs e)
        {
            Loaded -= ViewSingleGiving_Loaded;

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
                typeof(ViewSingleGiving),
                new PropertyMetadata(null));

    }
}
