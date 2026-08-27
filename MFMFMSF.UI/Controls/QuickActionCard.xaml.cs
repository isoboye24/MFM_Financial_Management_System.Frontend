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


        // ==========================================
        // TITLE
        // ==========================================

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


        // ==========================================
        // ICON
        // ==========================================

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


        // ==========================================
        // ICON BACKGROUND
        // ==========================================

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

    }
}