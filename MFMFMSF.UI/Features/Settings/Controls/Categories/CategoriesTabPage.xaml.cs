using MaterialDesignThemes.Wpf;
using MFMFMSF.Core.Interfaces;
using MFMFMSF.UI.Features.Settings.Controls.Categories.MeetingCategories;
using System;
using System.Net.Http;
using System.Windows;
using System.Windows.Controls;

namespace MFMFMSF.UI.Features.Settings.Controls.Categories
{
    public partial class CategoriesTabPage : UserControl
    {
        private readonly IMeetingCategoryService _meetingCategoryService;

        private Guid? _editingCategoryId;


        public CategoriesTabPage(
            IMeetingCategoryService meetingCategoryService)
        {
            InitializeComponent();

            _meetingCategoryService = meetingCategoryService;

            MeetingCategoriesListControl.SetService(
                _meetingCategoryService);

            Loaded += CategoriesTabPage_Loaded;
        }


        // ==========================================
        // LOAD CATEGORIES
        // ==========================================

        private async void CategoriesTabPage_Loaded(
            object sender,
            RoutedEventArgs e)
        {
            Loaded -= CategoriesTabPage_Loaded;

            await MeetingCategoriesListControl.LoadAsync();
        }


        // ==========================================
        // CREATE / UPDATE
        // ==========================================

        private async void CreateMeetingCategoryControl_ActionClicked(
            object? sender,
            EventArgs e)
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
                    MessageBox.Show(
                        $"Ready to update:\n\n{categoryName}",
                        "Meeting Category",
                        MessageBoxButton.OK,
                        MessageBoxImage.Information);

                    return;
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
        // EDIT
        // ==========================================

        private async void MeetingCategoriesListControl_EditClicked(
            object? sender,
            MeetingCategoryItem item)
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
    }
}