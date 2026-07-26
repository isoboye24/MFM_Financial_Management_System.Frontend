using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;


namespace MFMFMSF.UI.Controls
{
    /// <summary>
    /// Interaction logic for TopBar.xaml
    /// </summary>
    public partial class TopBar : UserControl
    {
        public event EventHandler? MinimizeRequested;
        public event EventHandler? MaximizeRequested;
        public event EventHandler? CloseRequested;

        public TopBar()
        {
            InitializeComponent();
        }

        private void MinimizeButton_Click(object sender, RoutedEventArgs e)
        {
            MinimizeRequested?.Invoke(this, EventArgs.Empty);
        }

        private void MaximizeButton_Click(object sender, RoutedEventArgs e)
        {
            MaximizeRequested?.Invoke(this, EventArgs.Empty);
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            CloseRequested?.Invoke(this, EventArgs.Empty);
        }

        private void TitleBar_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Window? window = Window.GetWindow(this);

            if (window == null)
                return;

            if (e.ClickCount == 2)
            {
                window.WindowState =
                    window.WindowState == WindowState.Maximized
                    ? WindowState.Normal
                    : WindowState.Maximized;

                return;
            }

            window.DragMove();
        }
    }
}
