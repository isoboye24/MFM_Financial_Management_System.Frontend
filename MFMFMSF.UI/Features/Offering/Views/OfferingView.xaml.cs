using MFMFMSF.Core.Interfaces;
using MFMFMSF.UI.Features.Offering.ViewModels;
using MFMFMSF.UI.Navigation;
using System.Windows.Controls;

namespace MFMFMSF.UI.Features.Offering.Views
{
    /// <summary>
    /// Interaction logic for OfferingView.xaml
    /// </summary>
    public partial class OfferingView : UserControl
    {
        private readonly OfferingViewModel _viewModel;
        public OfferingView(INavigationService navigationService, IGivingService givingService)
        {
            InitializeComponent();

            _viewModel = new OfferingViewModel(navigationService, givingService);

            DataContext = _viewModel;
        }
    }
}
