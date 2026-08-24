using System.Windows.Controls;

namespace MFMFMSF.UI.Navigation
{
    public interface INavigationService
    {
        void Navigate(UserControl page);
        event Action<UserControl>? PageChanged;
    }
}
