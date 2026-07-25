using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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