using MFMFMSF.Core.Interfaces;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Windows;
using System.Windows.Controls;

namespace MFMFMSF.UI.Features.Settings.Controls.MeetingCategories
{
    public partial class MeetingCategoriesList : UserControl
    {
        private IMeetingCategoryService? _meetingCategoryService;

        public ObservableCollection<MeetingCategoryItem> Categories { get; }
            = new();

        public MeetingCategoriesList()
        {
            InitializeComponent();

            CategoriesItemsControl.ItemsSource = Categories;
        }

        public void SetService(IMeetingCategoryService service)
        {
            _meetingCategoryService = service;
        }

        // ==========================================
        // LOAD CATEGORIES
        // ==========================================

        public async Task LoadAsync()
        {
            if (_meetingCategoryService == null)
                throw new InvalidOperationException(
                    "Meeting category service has not been configured.");

            try
            {
                var categories =
                    await _meetingCategoryService.GetAllAsync();

                Categories.Clear();

                int number = 1;

                foreach (var category in categories)
                {
                    Categories.Add(
                        new MeetingCategoryItem
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
                    "Unable to Load Meeting Categories",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
        }


        // ==========================================
        // EDIT
        // ==========================================

        private void EditButton_Click(
            object sender,
            RoutedEventArgs e)
        {
            if (sender is Button button &&
                button.Tag is MeetingCategoryItem item)
            {
                EditClicked?.Invoke(this, item);
            }
        }


        // ==========================================
        // DELETE
        // ==========================================

        private void DeleteButton_Click(
            object sender,
            RoutedEventArgs e)
        {
            if (sender is Button button &&
                button.Tag is MeetingCategoryItem item)
            {
                DeleteClicked?.Invoke(this, item);
            }
        }


        // ==========================================
        // EVENTS
        // ==========================================

        public event EventHandler<MeetingCategoryItem>? EditClicked;

        public event EventHandler<MeetingCategoryItem>? DeleteClicked;
    }
}