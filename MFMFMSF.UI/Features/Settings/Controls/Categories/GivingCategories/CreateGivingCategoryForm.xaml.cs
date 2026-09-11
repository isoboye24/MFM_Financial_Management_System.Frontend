using MaterialDesignThemes.Wpf;
using System.Windows;
using System.Windows.Controls;

namespace MFMFMSF.UI.Features.Settings.Controls.Categories.GivingCategories
{
    /// <summary>
    /// Interaction logic for CreateGivingCategoryForm.xaml
    /// </summary>
    public partial class CreateGivingCategoryForm : UserControl
    {
        public CreateGivingCategoryForm()
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
                typeof(CreateGivingCategoryForm),
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
                typeof(CreateGivingCategoryForm),
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
                typeof(CreateGivingCategoryForm),
                new PropertyMetadata(PackIconKind.Plus));


        // ==========================================
        // ACTION EVENT
        // ==========================================

        public event EventHandler? ActionClicked;


        // ==========================================
        // ACTION BUTTON
        // ==========================================

        private void GivingCategoryActionButton_Click(
            object sender,
            RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(CategoryName))
            {
                MessageBox.Show(
                    "Please enter a giving category name.",
                    "Giving Category",
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
