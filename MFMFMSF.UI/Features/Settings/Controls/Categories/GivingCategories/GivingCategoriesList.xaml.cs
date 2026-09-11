using MFMFMSF.Core.Interfaces;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Windows;
using System.Windows.Controls;

namespace MFMFMSF.UI.Features.Settings.Controls.Categories.GivingCategories
{
    /// <summary>
    /// Interaction logic for GivingCategoriesList.xaml
    /// </summary>
    public partial class GivingCategoriesList : UserControl
    {
        private IGivingCategoryService? _givingCategoryService;

        public ObservableCollection<GivingCategoryItem> Categories { get; }
            = new();

        public GivingCategoriesList()
        {
            InitializeComponent();

            GivingCategoriesItemsControl.ItemsSource = Categories;
        }

        public void SetService(IGivingCategoryService service)
        {
            _givingCategoryService = service;
        }

        // ==========================================
        // LOAD CATEGORIES
        // ==========================================

        public async Task LoadAsync()
        {
            if (_givingCategoryService == null)
                throw new InvalidOperationException(
                    "Giving category service has not been configured.");

            try
            {
                var categories =
                    await _givingCategoryService.GetAllAsync();

                Categories.Clear();

                int number = 1;

                foreach (var category in categories)
                {
                    Categories.Add(
                        new GivingCategoryItem
                        {
                            Id = category.Id,
                            Number = number++,
                            Name = category.Name
                        });
                }
            }
            catch (HttpRequestException)
            {
                MessageBox.Show(
                    "Unable to connect to the server.",
                    "Connection Error",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.Message,
                    "Unable to Load Giving Categories",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
        }


        // ==========================================
        // EDIT
        // ==========================================

        //private void EditGivingButton_Click(object sender, RoutedEventArgs e)
        //{
        //    if (sender is Button button &&
        //        button.Tag is GivingCategoryItem item)
        //    {
        //        EditGivingClicked?.Invoke(this, item);
        //    }
        //}


        // ==========================================
        // DELETE
        // ==========================================

        //private async void DeleteGivingButton_Click(object sender, RoutedEventArgs e)
        //{
        //    if (sender is not Button button ||
        //        button.Tag is not GivingCategoryItem item)
        //    {
        //        return;
        //    }

        //    var result = MessageBox.Show(
        //        $"Are you sure you want to delete '{item.Name}'?",
        //        "Delete Giving Category",
        //        MessageBoxButton.YesNo,
        //        MessageBoxImage.Warning);

        //    if (result != MessageBoxResult.Yes)
        //        return;

        //    if (_givingCategoryService == null)
        //    {
        //        MessageBox.Show(
        //            "Giving category service has not been configured.",
        //            "Delete Giving Category",
        //            MessageBoxButton.OK,
        //            MessageBoxImage.Error);

        //        return;
        //    }

        //    try
        //    {
        //        //await _givingCategoryService.DeleteAsync(item.Id);

        //        //Categories.Remove(item);

        //        //RenumberCategories();
        //    }
        //    catch (HttpRequestException)
        //    {
        //        MessageBox.Show(
        //            "Unable to connect to the server.",
        //            "Connection Error",
        //            MessageBoxButton.OK,
        //            MessageBoxImage.Error);
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(
        //            ex.Message,
        //            "Unable to Delete Meeting Category",
        //            MessageBoxButton.OK,
        //            MessageBoxImage.Error);
        //    }
        //}

        private void RenumberCategories()
        {
            int number = 1;

            foreach (var category in Categories)
            {
                category.Number = number++;
            }
        }


        // ==========================================
        // EVENTS
        // ==========================================

        public event EventHandler<GivingCategoryItem>? EditGivingClicked;
    }
}
