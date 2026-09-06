using System;
using System.Windows;
using System.Windows.Controls;

namespace MFMFMSF.UI.Features.Settings.Controls.MeetingCategories
{
    public partial class MeetingCategoriesList : UserControl
    {
        public MeetingCategoriesList()
        {
            InitializeComponent();
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