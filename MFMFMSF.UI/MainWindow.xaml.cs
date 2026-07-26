using MFMFMSF.UI.ViewModels;
using System.Windows;

namespace MFMFMSF.UI
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            TopBarControl.MinimizeRequested += TopBar_MinimizeRequested;
            TopBarControl.MaximizeRequested += TopBar_MaximizeRequested;
            TopBarControl.CloseRequested += TopBar_CloseRequested;

            DataContext = new MainWindowViewModel();
        }

        private void TopBar_MinimizeRequested(object? sender, EventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void TopBar_MaximizeRequested(object? sender, EventArgs e)
        {
            WindowState = WindowState == WindowState.Maximized
                ? WindowState.Normal
                : WindowState.Maximized;
        }

        private void TopBar_CloseRequested(object? sender, EventArgs e)
        {
            Close();
        }
    }
}