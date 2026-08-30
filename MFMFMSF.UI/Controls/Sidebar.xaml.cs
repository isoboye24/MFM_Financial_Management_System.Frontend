using MFMFMSF.UI.Features.Dashboard.ViewModels;
using MFMFMSF.UI.Navigation;
using System.Windows;
using System.Windows.Controls;

namespace MFMFMSF.UI.Controls
{
    public partial class Sidebar : UserControl
    {
        public Sidebar()
        {
            InitializeComponent();
        }

        public void SetNavigationService(INavigationService navigation)
        {
            DataContext = new SidebarViewModel(navigation);
        }

        private void SidebarMenuItem_Click(
            object sender,
            RoutedEventArgs e)
        {
            // Deselect every menu item
            foreach (var child in NavigationMenu.Children)
            {
                if (child is SidebarMenuItem menuItem)
                {
                    menuItem.IsSelected = false;
                }
            }

            // Select the clicked item
            if (sender is SidebarMenuItem clickedItem)
            {
                clickedItem.IsSelected = true;
            }
        }
    }
}