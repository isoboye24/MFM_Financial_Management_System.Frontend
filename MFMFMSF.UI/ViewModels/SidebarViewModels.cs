using MFMFMSF.UI.Commands;
using MFMFMSF.UI.Features.Dashboard.Views;
using MFMFMSF.UI.Features.Members.Views;
using MFMFMSF.UI.Features.Offering.Views;
using MFMFMSF.UI.Features.Tithes.Views;
using MFMFMSF.UI.Navigation;
using System.Windows.Input;

namespace MFMFMSF.UI.Features.Dashboard.ViewModels
{
    public class SidebarViewModel
    {
        private readonly INavigationService _navigation;

        public ICommand NavigateDashboardCommand { get; }
        public ICommand NavigateOfferingsCommand { get; }
        public ICommand NavigateTithesCommand { get; }
        public ICommand NavigateMembersCommand { get; }


        public SidebarViewModel(INavigationService navigation)
        {
            _navigation = navigation;

            NavigateDashboardCommand =
                new RelayCommand(_ =>
                    _navigation.Navigate(new DashboardView()));

            NavigateOfferingsCommand =
                new RelayCommand(_ =>
                    _navigation.Navigate(new OfferingView()));

            NavigateTithesCommand =
                new RelayCommand(_ =>
                    _navigation.Navigate(new TithesView()));

            NavigateMembersCommand =
                new RelayCommand(_ =>
                    _navigation.Navigate(new MembersView()));
        }
    }
}