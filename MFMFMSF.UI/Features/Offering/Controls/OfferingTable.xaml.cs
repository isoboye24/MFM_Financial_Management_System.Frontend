using MFMFMSF.Core.Interfaces;
using MFMFMSF.Core.Models.Givings;
using MFMFMSF.UI.Commands;
using MFMFMSF.UI.Features.Givings.Views;
using MFMFMSF.UI.Navigation;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace MFMFMSF.UI.Features.Offering.Controls
{
    /// <summary>
    /// Interaction logic for OfferingTable.xaml
    /// </summary>
    public partial class OfferingTable : UserControl
    {
        public ICommand ViewGivingCommand { get; }
        public OfferingTable()
        {
            InitializeComponent();

            ViewGivingCommand = new RelayCommandGeneric<GivingByMonthAndYear>(ViewGiving);
        }

        private void ViewGivingButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is not Button button ||
                button.DataContext is not GivingByMonthAndYear giving)
            {
                MessageBox.Show(
                    "Unable to identify the selected giving record.",
                    "View Giving",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);

                return;
            }

            ViewGiving(giving);
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
                typeof(OfferingTable),
                new PropertyMetadata(null));
        
        
        // =====================================================
        // GIVING SERVICE
        // =====================================================

        public IGivingService? GivingService
        {
            get => (IGivingService?)GetValue(GivingServiceProperty);
            set => SetValue(GivingServiceProperty, value);
        }

        public static readonly DependencyProperty GivingServiceProperty =
            DependencyProperty.Register(
                nameof(GivingService),
                typeof(IGivingService),
                typeof(OfferingTable),
                new PropertyMetadata(null));
        
        
        // =====================================================
        // GIVING CATEGORY SERVICE
        // =====================================================

        public IGivingCategoryService? GivingCategoryService
        {
            get => (IGivingCategoryService?)GetValue(GivingCategoryServiceProperty);
            set => SetValue(GivingCategoryServiceProperty, value);
        }

        public static readonly DependencyProperty GivingCategoryServiceProperty =
            DependencyProperty.Register(
                nameof(GivingCategoryService),
                typeof(IGivingCategoryService),
                typeof(OfferingTable),
                new PropertyMetadata(null));


        // =====================================================
        // ACTIONS
        // =====================================================

        private void ViewGiving(GivingByMonthAndYear giving)
        {
            if (NavigationService == null || GivingService == null || GivingCategoryService == null)
            {
                MessageBox.Show(
                    "Navigation services are not configured.",
                    "Error",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);

                return;
            }

            NavigationService.Navigate(new ViewSingleGiving(giving.Id, NavigationService, GivingCategoryService, GivingService));
        }
    }
}
