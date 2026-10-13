
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using MaterialDesignThemes.Wpf;

namespace MFMFMSF.UI.Controls
{
    public partial class QuickActionCard : UserControl
    {
        public QuickActionCard()
        {
            InitializeComponent();
        }

        // Title
        public string Title
        {
            get => (string)GetValue(TitleProperty);
            set => SetValue(TitleProperty, value);
        }

        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register(
                nameof(Title),
                typeof(string),
                typeof(QuickActionCard),
                new PropertyMetadata(string.Empty));

        // Icon
        public PackIconKind Icon
        {
            get => (PackIconKind)GetValue(IconProperty);
            set => SetValue(IconProperty, value);
        }

        public static readonly DependencyProperty IconProperty =
            DependencyProperty.Register(
                nameof(Icon),
                typeof(PackIconKind),
                typeof(QuickActionCard),
                new PropertyMetadata(PackIconKind.None));

        // Icon background
        public Brush IconBackground
        {
            get => (Brush)GetValue(IconBackgroundProperty);
            set => SetValue(IconBackgroundProperty, value);
        }

        public static readonly DependencyProperty IconBackgroundProperty =
            DependencyProperty.Register(
                nameof(IconBackground),
                typeof(Brush),
                typeof(QuickActionCard),
                new PropertyMetadata(Brushes.Gray));

        // Click event exposed to the parent control
        public event RoutedEventHandler? Click;

        private void QuickActionButton_Click(
            object sender,
            RoutedEventArgs e)
        {
            Click?.Invoke(this, e);
        }
    }
}
