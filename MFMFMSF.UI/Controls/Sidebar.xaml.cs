using MFMFMSF.UI.Features.Dashboard.ViewModels;
using MFMFMSF.UI.Navigation;
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
    }
}