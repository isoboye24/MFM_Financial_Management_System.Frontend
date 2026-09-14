using MFMFMSF.Core.Interfaces;
using MFMFMSF.Core.Models.Givings;
using MFMFMSF.UI.Commands;
using MFMFMSF.UI.Navigation;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace MFMFMSF.UI.Features.Givings.Views
{
    /// <summary>
    /// Interaction logic for GivingView.xaml
    /// </summary>
    public partial class GivingView : UserControl
    {
        public ICommand EditGivingCommand { get; }
        public ICommand DeleteGivingCommand { get; }
        public ICommand ViewGivingCommand { get; }

        public GivingView()
        {
            InitializeComponent();

            EditGivingCommand =new RelayCommandGeneric<GivingListItem>(EditGiving);
            DeleteGivingCommand = new RelayCommandGeneric<GivingListItem>(DeleteGiving);
            ViewGivingCommand = new RelayCommandGeneric<GivingListItem>(ViewSingleGiving);
        }

        public ObservableCollection<GivingListItem> Givings
        {
            get => (ObservableCollection<GivingListItem>) GetValue(GivingsProperty);
            set => SetValue(GivingsProperty, value);
        }


        public static readonly DependencyProperty GivingsProperty =
            DependencyProperty.Register(
                nameof(Givings),
                typeof(ObservableCollection<GivingListItem>),
                typeof(GivingView),
                new PropertyMetadata(null));



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
                typeof(GivingView),
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
                typeof(GivingView),
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
                typeof(GivingView),
                new PropertyMetadata(null));




        // =====================================================
        // ACTIONS
        // =====================================================

        private void EditGiving(GivingListItem giving)
        {
            if (NavigationService == null || GivingCategoryService == null || GivingService == null)
            {
                MessageBox.Show("Navigation services are not configured.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            NavigationService.Navigate(new EditGiving(giving.Id, NavigationService, GivingCategoryService, GivingService));
        }


        private async void DeleteGiving(GivingListItem giving)
        {
            var result = MessageBox.Show($"Are you sure you want to delete '{giving.MessageTitle}'?", "Delete Church Service", MessageBoxButton.YesNo, MessageBoxImage.Warning);

            if (result != MessageBoxResult.Yes)
                return;

            try
            {
                if (GivingService == null)
                {
                    MessageBox.Show("Giving service is not configured.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                await GivingService.DeleteAsync(giving.Id);
                Givings.Remove(giving);

                MessageBox.Show("Giving deleted successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"The giving could not be deleted.\n\n{ex.Message}", "Delete Failed", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ViewSingleGiving(GivingListItem giving)
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
