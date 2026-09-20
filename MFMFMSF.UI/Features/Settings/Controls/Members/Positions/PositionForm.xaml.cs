using MaterialDesignThemes.Wpf;
using MFMFMSF.UI.Features.Settings.Controls.Categories.MeetingCategories;
using System.Windows;
using System.Windows.Controls;

namespace MFMFMSF.UI.Features.Settings.Controls.Members.Positions
{
    /// <summary>
    /// Interaction logic for PositionForm.xaml
    /// </summary>
    public partial class PositionForm : UserControl
    {
        public PositionForm()
        {
            InitializeComponent();
        }

        // ==========================================
        // POSITION NAME
        // ==========================================

        public string PositionName
        {
            get => (string)GetValue(PositionNameProperty);
            set => SetValue(PositionNameProperty, value);
        }

        public static readonly DependencyProperty PositionNameProperty =
            DependencyProperty.Register(
                nameof(PositionName),
                typeof(string),
                typeof(PositionForm),
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
                typeof(PositionForm),
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
                typeof(PositionForm),
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
            if (string.IsNullOrWhiteSpace(PositionName))
            {
                MessageBox.Show(
                    "Please enter a position name.",
                    "Position",
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
