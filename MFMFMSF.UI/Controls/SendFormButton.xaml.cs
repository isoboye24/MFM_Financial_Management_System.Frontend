using MaterialDesignThemes.Wpf;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace MFMFMSF.UI.Controls
{
    public partial class SendFormButton : UserControl
    {
        public SendFormButton()
        {
            InitializeComponent();
        }


        // =====================================================
        // TEXT
        // =====================================================

        public string Text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }

        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register(
                nameof(Text),
                typeof(string),
                typeof(SendFormButton),
                new PropertyMetadata("Save"));


        // =====================================================
        // COMMAND
        // =====================================================

        public ICommand? Command
        {
            get => (ICommand?)GetValue(CommandProperty);
            set => SetValue(CommandProperty, value);
        }

        public static readonly DependencyProperty CommandProperty =
            DependencyProperty.Register(
                nameof(Command),
                typeof(ICommand),
                typeof(SendFormButton),
                new PropertyMetadata(null));


        // =====================================================
        // ICON
        // =====================================================

        public PackIconKind Icon
        {
            get => (PackIconKind)GetValue(IconProperty);
            set => SetValue(IconProperty, value);
        }

        public static readonly DependencyProperty IconProperty =
            DependencyProperty.Register(
                nameof(Icon),
                typeof(PackIconKind),
                typeof(SendFormButton),
                new PropertyMetadata(PackIconKind.ContentSave));


        // =====================================================
        // IS ENABLED
        // =====================================================

        public bool IsEnabled
        {
            get => (bool)GetValue(IsEnabledProperty);
            set => SetValue(IsEnabledProperty, value);
        }

        public static readonly DependencyProperty IsEnabledProperty =
            DependencyProperty.Register(
                nameof(IsEnabled),
                typeof(bool),
                typeof(SendFormButton),
                new PropertyMetadata(true));
    }
}