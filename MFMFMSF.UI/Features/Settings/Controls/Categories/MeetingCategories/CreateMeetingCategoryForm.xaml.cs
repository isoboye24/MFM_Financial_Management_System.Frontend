using System;
using System.Windows;
using System.Windows.Controls;
using MaterialDesignThemes.Wpf;

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
        // BUTTON TEXT
        // ==========================================

        public string ButtonText
        {
            get => (string)GetValue(ButtonTextProperty);
            set => SetValue(ButtonTextProperty, value);
        }

        public static readonly DependencyProperty ButtonTextProperty =
            DependencyProperty.Register(
                nameof(ButtonText),
                typeof(string),
                typeof(CreateMeetingCategoryForm),
                new PropertyMetadata("Create"));


        // ==========================================
        // BUTTON ICON
        // ==========================================

        public PackIconKind ButtonIcon
        {
            get => (PackIconKind)GetValue(ButtonIconProperty);
            set => SetValue(ButtonIconProperty, value);
        }

        public static readonly DependencyProperty ButtonIconProperty =
            DependencyProperty.Register(
                nameof(ButtonIcon),
                typeof(PackIconKind),
                typeof(CreateMeetingCategoryForm),
                new PropertyMetadata(PackIconKind.Plus));


        // ==========================================
        // ACTION EVENT
        // ==========================================

        public event EventHandler? ActionClicked;


        // ==========================================
        // ACTION BUTTON
        // ==========================================

        private void ActionButton_Click(
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

            ActionClicked?.Invoke(
                this,
                EventArgs.Empty);
        }
    }
}