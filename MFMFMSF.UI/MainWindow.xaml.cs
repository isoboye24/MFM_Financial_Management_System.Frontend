using MFMFMSF.Core.Interfaces;
using MFMFMSF.UI.Navigation;
using MFMFMSF.UI.ViewModels;
using System.Windows;

namespace MFMFMSF.UI
{
    public partial class MainWindow : Window
    {
        private readonly NavigationService _navigation;
        private readonly IGivingService _givingService;
        private readonly IMeetingCategoryService _meetingCategoryService;
        private readonly IMeetingService _meetingService;
        private readonly IGivingCategoryService _givingCategoryService;
        private readonly IExpenditureService _expenditureService;
        private readonly IReportService _reportService;


        public MainWindow()
        {
            InitializeComponent();

            TopBarControl.MinimizeRequested += TopBar_MinimizeRequested;
            TopBarControl.MaximizeRequested += TopBar_MaximizeRequested;
            TopBarControl.CloseRequested += TopBar_CloseRequested;


            _navigation = new NavigationService();
            _givingService = App.GivingService;
            _meetingCategoryService = App.MeetingCategoryService;
            _meetingService = App.MeetingService;
            _givingCategoryService = App.GivingCategoryService;
            _expenditureService = App.ExpenditureService;
            _reportService = App.ReportService;

            DataContext = new MainViewModel(_navigation, _givingService, _expenditureService);

            SidebarControl.SetNavigationService(_navigation, _meetingCategoryService, _meetingService, _givingCategoryService, _givingService, _expenditureService, _reportService);
        }

        private void TopBar_MinimizeRequested(object? sender, EventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void TopBar_MaximizeRequested(object? sender, EventArgs e)
        {
            WindowState = WindowState == WindowState.Maximized
                ? WindowState.Normal
                : WindowState.Maximized;
        }

        private void TopBar_CloseRequested(object? sender, EventArgs e)
        {
            Close();
        }
    }
}