using MFMFMSF.Core.Interfaces;
using MFMFMSF.UI.Commands;
using MFMFMSF.UI.Features.Dashboard.Views;
using MFMFMSF.UI.Features.Expenditures.Views;
using MFMFMSF.UI.Features.Meetings.Views;
using MFMFMSF.UI.Features.Offering.Views;
using MFMFMSF.UI.Features.OtherIncome.Views;
using MFMFMSF.UI.Features.Projects.Views;
using MFMFMSF.UI.Features.Reports.Views;
using MFMFMSF.UI.Features.Seeds.Views;
using MFMFMSF.UI.Features.Settings.Views;
using MFMFMSF.UI.Features.Tithes.Views;
using MFMFMSF.UI.Features.Workers.Views;
using MFMFMSF.UI.Navigation;
using System.Windows.Input;

namespace MFMFMSF.UI.Features.Dashboard.ViewModels
{
    public class SidebarViewModel
    {
        private readonly INavigationService _navigation;
        private readonly IMeetingCategoryService _meetingCategoryService;
        private readonly IMeetingService _meetingService;

        public ICommand NavigateDashboardCommand { get; }
        public ICommand NavigateMeetingsCommand { get; }
        public ICommand NavigateOfferingsCommand { get; }
        public ICommand NavigateTithesCommand { get; }
        public ICommand NavigateSeedsCommand { get; }
        public ICommand NavigateOtherIncomeCommand { get; }
        public ICommand NavigateExpendituresCommand { get; }
        public ICommand NavigateProjectsCommand { get; }
        public ICommand NavigateWorkersCommand { get; }
        public ICommand NavigateReportsCommand { get; }
        public ICommand NavigateSettingsCommand { get; }


        public SidebarViewModel(INavigationService navigation, IMeetingCategoryService meetingCategoryService, IMeetingService meetingService)
        {
            _navigation = navigation;
            _meetingCategoryService = meetingCategoryService;
            _meetingService = meetingService;

            NavigateDashboardCommand = new RelayCommand(_ =>  _navigation.Navigate(new DashboardView()));

            NavigateMeetingsCommand = new RelayCommand(_ =>  _navigation.Navigate(new MeetingsView(_navigation, _meetingCategoryService, meetingService)));
            
            NavigateOfferingsCommand = new RelayCommand(_ =>  _navigation.Navigate(new OfferingView()));

            NavigateTithesCommand = new RelayCommand(_ =>  _navigation.Navigate(new TithesView()));

            NavigateSeedsCommand = new RelayCommand(_ =>  _navigation.Navigate(new SeedsView()));

            NavigateOtherIncomeCommand = new RelayCommand(_ =>  _navigation.Navigate(new OtherIncomeView()));

            NavigateExpendituresCommand = new RelayCommand(_ =>  _navigation.Navigate(new ExpendituresView()));

            NavigateProjectsCommand = new RelayCommand(_ =>  _navigation.Navigate(new ProjectsView()));

            NavigateWorkersCommand = new RelayCommand(_ =>  _navigation.Navigate(new WorkersView()));

            NavigateReportsCommand = new RelayCommand(_ =>  _navigation.Navigate(new ReportsView()));

            NavigateSettingsCommand = new RelayCommand(_ =>  _navigation.Navigate(new SettingsView()));
        }
    }
}