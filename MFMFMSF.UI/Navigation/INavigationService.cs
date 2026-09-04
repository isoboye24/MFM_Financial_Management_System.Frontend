using System.Windows.Controls;

namespace MFMFMSF.UI.Navigation
{
    public interface INavigationService
    {
        void Navigate(UserControl page);

        void GoBack();

        bool CanGoBack { get; }

        event Action<UserControl>? PageChanged;
    }
}
