using System;
using System.Windows;
using System.Windows.Controls;

namespace MFMFMSF.UI.Features.Settings.Controls.Categories.MeetingCategories
{
    public partial class CreateMeetingCategoryForm : UserControl
    {
        public CreateMeetingCategoryForm()
        {
            InitializeComponent();
        }


        // ==========================================
        // CATEGORY NAME
        // ==========================================

        public string CategoryName
        {
            get => (string)GetValue(CategoryNameProperty);
            set => SetValue(CategoryNameProperty, value);
        }

        public static readonly DependencyProperty CategoryNameProperty =
            DependencyProperty.Register(
                nameof(CategoryName),
                typeof(string),
                typeof(CreateMeetingCategoryForm),
                new PropertyMetadata(string.Empty));


        // ==========================================
        // CREATE EVENT
        // ==========================================

        public event EventHandler? CreateClicked;


        // ==========================================
        // CREATE BUTTON
        // ==========================================

        private void CreateButton_Click(
            object sender,
            RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(CategoryName))
            {
                MessageBox.Show(
                    "Please enter a meeting category name.",
                    "Meeting Category",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);

                return;
            }

            CreateClicked?.Invoke(this, EventArgs.Empty);
        }
    }
}