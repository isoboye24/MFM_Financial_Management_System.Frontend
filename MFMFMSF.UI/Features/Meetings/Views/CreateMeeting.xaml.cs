using MFMFMSF.UI.Features.Meetings.ViewModels;
using MFMFMSF.UI.Navigation;
using System.Windows.Controls;

namespace MFMFMSF.UI.Features.Meetings.Views
{
    /// <summary>
    /// Interaction logic for CreateMeeting.xaml
    /// </summary>
    public partial class CreateMeeting : UserControl
    {
        public INavigationService NavigationService { get; }
        public CreateMeeting(INavigationService navigationService)
        {
            InitializeComponent();

            NavigationService = navigationService;

            DataContext = new MeetingsViewModel(navigationService);
        }
    }
}
