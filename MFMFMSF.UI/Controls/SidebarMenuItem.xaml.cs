using MaterialDesignThemes.Wpf;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace MFMFMSF.UI.Controls
{
    /// <summary>
    /// Interaction logic for SidebarMenuItem.xaml
    /// </summary>
    public partial class SidebarMenuItem : UserControl
    {
        public event RoutedEventHandler? Click;

        public SidebarMenuItem()
        {
            InitializeComponent();
        }

        public string Title
        {
            get => (string)GetValue(TitleProperty);
            set => SetValue(TitleProperty, value);
        }

        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register(
                nameof(Title),
                typeof(string),
                typeof(SidebarMenuItem),
                new PropertyMetadata(string.Empty));

        public PackIconKind Icon
        {
            get => (PackIconKind)GetValue(IconProperty);
            set => SetValue(IconProperty, value);
        }

        public static readonly DependencyProperty IconProperty =
            DependencyProperty.Register(
                nameof(Icon),
                typeof(PackIconKind),
                typeof(SidebarMenuItem),
                new PropertyMetadata(PackIconKind.ViewDashboard));

        public bool IsSelected
        {
            get => (bool)GetValue(IsSelectedProperty);
            set => SetValue(IsSelectedProperty, value);
        }

        public static readonly DependencyProperty IsSelectedProperty =
            DependencyProperty.Register(
                nameof(IsSelected),
                typeof(bool),
                typeof(SidebarMenuItem),
                new PropertyMetadata(false, OnSelectedChanged));

        private static void OnSelectedChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = (SidebarMenuItem)d;

            control.MenuBorder.Background =
                (bool)e.NewValue
                    ? new System.Windows.Media.SolidColorBrush(
                        (System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#5B4CF3"))
                    : System.Windows.Media.Brushes.Transparent;
        }

        private void MenuBorder_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (Command?.CanExecute(null) == true)
            {
                Command.Execute(null);
            }

            Click?.Invoke(this, new RoutedEventArgs());
        }

        public ICommand? Command
        {
            get => (ICommand?)GetValue(CommandProperty);
            set => SetValue(CommandProperty, value);
        }

        public static readonly DependencyProperty CommandProperty =
            DependencyProperty.Register(
                nameof(Command),
                typeof(ICommand),
                typeof(SidebarMenuItem));
    }
}
