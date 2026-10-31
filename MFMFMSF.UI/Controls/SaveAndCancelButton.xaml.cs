using MaterialDesignThemes.Wpf;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace MFMFMSF.UI.Controls
{
    public partial class SaveAndCancelButton : UserControl
    {
        public SaveAndCancelButton()
        {
            InitializeComponent();
        }

        // Save button text
        public static readonly DependencyProperty SaveTextProperty =
            DependencyProperty.Register(
                nameof(SaveText),
                typeof(string),
                typeof(SaveAndCancelButton),
                new PropertyMetadata("Save"));

        public string SaveText
        {
            get => (string)GetValue(SaveTextProperty);
            set => SetValue(SaveTextProperty, value);
        }

        // Cancel button text
        public static readonly DependencyProperty CancelTextProperty =
            DependencyProperty.Register(
                nameof(CancelText),
                typeof(string),
                typeof(SaveAndCancelButton),
                new PropertyMetadata("Cancel"));

        public string CancelText
        {
            get => (string)GetValue(CancelTextProperty);
            set => SetValue(CancelTextProperty, value);
        }

        // Save button icon
        public static readonly DependencyProperty SaveIconKindProperty =
        DependencyProperty.Register(
        nameof(SaveIconKind),
        typeof(PackIconKind),
        typeof(SaveAndCancelButton),
        new PropertyMetadata(PackIconKind.AccountPlusOutline));

        public PackIconKind SaveIconKind
        {
            get => (PackIconKind)GetValue(SaveIconKindProperty);
            set => SetValue(SaveIconKindProperty, value);
        }

        // Save command
        public static readonly DependencyProperty SaveCommandProperty =
            DependencyProperty.Register(
                nameof(SaveCommand),
                typeof(ICommand),
                typeof(SaveAndCancelButton),
                new PropertyMetadata(null));

        public ICommand? SaveCommand
        {
            get => (ICommand?)GetValue(SaveCommandProperty);
            set => SetValue(SaveCommandProperty, value);
        }

        // Cancel command
        public static readonly DependencyProperty CancelCommandProperty =
            DependencyProperty.Register(
                nameof(CancelCommand),
                typeof(ICommand),
                typeof(SaveAndCancelButton),
                new PropertyMetadata(null));

        public ICommand? CancelCommand
        {
            get => (ICommand?)GetValue(CancelCommandProperty);
            set => SetValue(CancelCommandProperty, value);
        }
    }
}
