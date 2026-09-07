using MFMFMSF.Core.Interfaces;
using System;
using System.Net.Http;
using System.Windows;
using System.Windows.Controls;

namespace MFMFMSF.UI.Features.Settings.Controls.Categories
{
    public partial class CategoriesTabPage : UserControl
    {
        private readonly IMeetingCategoryService _meetingCategoryService;

        public CategoriesTabPage(
            IMeetingCategoryService meetingCategoryService)
        {
            InitializeComponent();

            _meetingCategoryService = meetingCategoryService;

            MeetingCategoriesListControl.SetService(_meetingCategoryService);

            Loaded += CategoriesTabPage_Loaded;
        }

        private async void CategoriesTabPage_Loaded(object sender, RoutedEventArgs e)
        {
            Loaded -= CategoriesTabPage_Loaded;

            await MeetingCategoriesListControl.LoadAsync();
        }

        private async void CreateMeetingCategoryControl_CreateClicked(
            object sender,
            EventArgs e)
        {
            string categoryName =
                CreateMeetingCategoryControl.CategoryName.Trim();

            try
            {
                await _meetingCategoryService.CreateAsync(categoryName);

                MessageBox.Show(
                    "Meeting category created successfully.",
                    "Meeting Category",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information);

                CreateMeetingCategoryControl.CategoryName =
                    string.Empty;
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
                    "Unable to Create Meeting Category",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
        }
    }
}