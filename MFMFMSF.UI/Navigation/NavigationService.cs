using System.Windows.Controls;

namespace MFMFMSF.UI.Navigation
{
    public class NavigationService : INavigationService
    {
        public event Action<UserControl>? PageChanged;

        public void Navigate(UserControl page)
        {
            PageChanged?.Invoke(page);
        }
    }
}
