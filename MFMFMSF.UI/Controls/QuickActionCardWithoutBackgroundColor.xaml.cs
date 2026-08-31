using MaterialDesignThemes.Wpf;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Input;

namespace MFMFMSF.UI.Controls
{
    public partial class QuickActionCardWithoutBackgroundColor : UserControl
    {
        public QuickActionCardWithoutBackgroundColor()
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
                typeof(QuickActionCardWithoutBackgroundColor),
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
                typeof(QuickActionCardWithoutBackgroundColor),
                new PropertyMetadata(PackIconKind.None));


        // ==========================================
        // ICON FOREGROUND
        // ==========================================

        public Brush IconForeground
        {
            get => (Brush)GetValue(IconForegroundProperty);
            set => SetValue(IconForegroundProperty, value);
        }

        public static readonly DependencyProperty IconForegroundProperty =
            DependencyProperty.Register(
                nameof(IconForeground),
                typeof(Brush),
                typeof(QuickActionCardWithoutBackgroundColor),
                new PropertyMetadata(Brushes.Black));


        // ==========================================
        // COMMAND
        // ==========================================

        public ICommand ActionCommand
        {
            get => (ICommand)GetValue(ActionCommandProperty);
            set => SetValue(ActionCommandProperty, value);
        }

        public static readonly DependencyProperty ActionCommandProperty =
            DependencyProperty.Register(
                nameof(ActionCommand),
                typeof(ICommand),
                typeof(QuickActionCardWithoutBackgroundColor),
                new PropertyMetadata(null));
    }
}