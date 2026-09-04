using System.Windows.Controls;

namespace MFMFMSF.UI.Navigation
{
    public class NavigationService : INavigationService
    {
        private readonly Stack<UserControl> _navigationStack = new();

        private UserControl? _currentPage;

        public event Action<UserControl>? PageChanged;

        public bool CanGoBack => _navigationStack.Count > 0;

        public void Navigate(UserControl page)
        {
            if (_currentPage != null)
            {
                _navigationStack.Push(_currentPage);
            }

            _currentPage = page;

            PageChanged?.Invoke(page);
        }

        public void GoBack()
        {
            if (!CanGoBack)
                return;

            _currentPage = _navigationStack.Pop();

            PageChanged?.Invoke(_currentPage);
        }
    }
}