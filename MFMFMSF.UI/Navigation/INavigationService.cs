using System.Windows.Controls;

namespace MFMFMSF.UI.Navigation
{
    interface INavigationService
    {
        void Navigate(UserControl page);
        event Action<UserControl>? PageChanged;
    }
}
