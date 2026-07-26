using MFMFMSF.UI.Commands;
using MFMFMSF.UI.Features.Dashboard.Views;
using MFMFMSF.UI.Features.Members.Views;
using System.Windows.Input;
using MFMFMSF.UI.Navigation;

namespace MFMFMSF.UI.Features.Dashboard.ViewModels
{
    class SidebarViewModels
    {
        public class SidebarViewModel
        {
            private readonly NavigationService _navigation;

            public ICommand NavigateDashboardCommand { get; }

            public ICommand NavigateMembersCommand { get; }

            public SidebarViewModel(NavigationService navigation)
            {
                _navigation = navigation;

                NavigateDashboardCommand =
                    new RelayCommand(_ =>
                        _navigation.Navigate(new DashboardView()));

                NavigateMembersCommand =
                    new RelayCommand(_ =>
                        _navigation.Navigate(new MembersView()));
            }
        }
    }
}
