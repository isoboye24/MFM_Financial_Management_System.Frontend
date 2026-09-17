using MFMFMSF.Core.Interfaces;
using MFMFMSF.UI.Navigation;

namespace MFMFMSF.UI.Features.Offering.ViewModels
{
    public class OfferingViewModel
    {
        private readonly INavigationService _navigationService;
        private readonly IGivingService _givingService;

        public OfferingViewModel(INavigationService navigationService, IGivingService givingService)
        {
            _navigationService = navigationService;
            _givingService = givingService;
        }
    }
}
