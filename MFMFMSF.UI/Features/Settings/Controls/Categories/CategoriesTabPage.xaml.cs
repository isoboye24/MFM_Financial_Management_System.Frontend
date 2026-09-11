using MaterialDesignThemes.Wpf;
using MFMFMSF.Core.Interfaces;
using MFMFMSF.UI.Features.Settings.Controls.Categories.MeetingCategories;
using System.Net.Http;
using System.Windows;
using System.Windows.Controls;

namespace MFMFMSF.UI.Features.Settings.Controls.Categories
{
    public partial class CategoriesTabPage : UserControl
    {
        private readonly IMeetingCategoryService _meetingCategoryService;
        private readonly IGivingCategoryService _givingCategoryService;

        private Guid? _editingCategoryId;
        private Guid? _editingGivingCategoryId;


        public CategoriesTabPage(
            IMeetingCategoryService meetingCategoryService,
            IGivingCategoryService givingCategoryService)
        {
            InitializeComponent();

            _meetingCategoryService = meetingCategoryService;
            _givingCategoryService = givingCategoryService;

            MeetingCategoriesListControl.SetService(_meetingCategoryService);

            Loaded += CategoriesTabPage_Loaded;
        }


        // ==========================================
        // LOAD CATEGORIES
        // ==========================================

        private async void CategoriesTabPage_Loaded(object sender, RoutedEventArgs e)
        {
            Loaded -= CategoriesTabPage_Loaded;

            await MeetingCategoriesListControl.LoadAsync();
        }


        // ==========================================
        // CREATE / UPDATE MEETING CATEGORY
        // ==========================================

        private async void CreateMeetingCategoryControl_ActionClicked(object? sender, EventArgs e)
        {
            string categoryName =
                CreateMeetingCategoryControl.CategoryName.Trim();

            try
            {
                if (_editingCategoryId == null)
                {
                    // CREATE
                    await _meetingCategoryService
                        .CreateAsync(categoryName);

                    MessageBox.Show(
                        "Meeting category created successfully.",
                        "Meeting Category",
                        MessageBoxButton.OK,
                        MessageBoxImage.Information);
                }
                else
                {
                    // UPDATE
                    await _meetingCategoryService.UpdateAsync(
                        _editingCategoryId.Value,
                        categoryName);

                    MessageBox.Show(
                        "Meeting category updated successfully.",
                        "Meeting Category",
                        MessageBoxButton.OK,
                        MessageBoxImage.Information);
                }


                // Reset form

                CreateMeetingCategoryControl.CategoryName =
                    string.Empty;

                CreateMeetingCategoryControl.ButtonText =
                    "Create";

                CreateMeetingCategoryControl.ButtonIcon =
                    PackIconKind.Plus;

                _editingCategoryId = null;


                // Refresh list

                await MeetingCategoriesListControl.LoadAsync();
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
                    "Meeting Category",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
        }


        // ==========================================
        // EDIT MEETING CATEGORY
        // ==========================================

        private async void MeetingCategoriesListControl_EditClicked(object? sender, MeetingCategoryItem item)
        {
            try
            {
                var category =
                    await _meetingCategoryService
                        .GetByIdAsync(item.Id);


                // Put category name into the form

                CreateMeetingCategoryControl.CategoryName =
                    category.Name;


                // Change button to UPDATE

                CreateMeetingCategoryControl.ButtonText =
                    "Update";

                CreateMeetingCategoryControl.ButtonIcon =
                    PackIconKind.Check;


                // Remember which category we are editing

                _editingCategoryId =
                    category.Id;
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
                    "Unable to Load Meeting Category",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
        }

        // ==========================================
        // CREATE / UPDATE GIVING CATEGORY
        // ==========================================

        private async void CreateGivingCategoryControl_ActionClicked(object? sender, EventArgs e)
        {
            string categoryName =
                CreateGivingCategoryControl.CategoryName.Trim();

            try
            {
                if (_editingGivingCategoryId == null)
                {
                    // CREATE
                    await _givingCategoryService.CreateAsync(categoryName);

                    MessageBox.Show(
                        "Giving category created successfully.",
                        "Giving Category",
                        MessageBoxButton.OK,
                        MessageBoxImage.Information);
                }
                else
                {
                    // UPDATE
                    //await _givingCategoryService.UpdateAsync(_editingGivingCategoryId.Value, categoryName);

                    //MessageBox.Show(
                    //    "Giving category updated successfully.",
                    //    "Giving Category",
                    //    MessageBoxButton.OK,
                    //    MessageBoxImage.Information);
                }


                // Reset form

                CreateGivingCategoryControl.CategoryName =
                    string.Empty;

                CreateGivingCategoryControl.ButtonText =
                    "Create";

                CreateGivingCategoryControl.ButtonIcon =
                    PackIconKind.Plus;

                _editingGivingCategoryId = null;

                // Refresh list

                //await GivingCategoriesListControl.LoadAsync();
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
                    "Giving Category",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
        }
    }
}