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


            DataContext = new MainViewModel(_navigation, _givingService);

            SidebarControl.SetNavigationService(_navigation, _meetingCategoryService, _meetingService, _givingCategoryService, _givingService);
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