using MFMFMSF.Core.Interfaces;
using MFMFMSF.UI.Features.Givings.ViewModels;
using MFMFMSF.UI.Navigation;
using System.Windows;
using System.Windows.Controls;

namespace MFMFMSF.UI.Features.Givings.Views
{
    /// <summary>
    /// Interaction logic for EditGiving.xaml
    /// </summary>
    public partial class EditGiving : UserControl
    {
        private readonly EditGivingViewModel _viewModel;

        public EditGiving(Guid givingId, INavigationService navigationService, IGivingCategoryService givingCategoryService, IGivingService givingService)
        {
            InitializeComponent();

            NavigationService = navigationService;

            _viewModel = new EditGivingViewModel(givingId, givingCategoryService, givingService, navigationService);

            DataContext = _viewModel;

            Loaded += EditGiving_Loaded;
        }

        private async void EditGiving_Loaded(object sender, RoutedEventArgs e)
        {
            Loaded -= EditGiving_Loaded;
            await _viewModel.LoadAsync();
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
                typeof(EditGiving),
                new PropertyMetadata(null));
    }
}
