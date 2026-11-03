using MFMFMSF.Core.Interfaces;
using MFMFMSF.UI.Features.Dashboard.Controls;
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
        private readonly IPositionService _positionService;
        private readonly IWorkerService _workerService;
        private readonly IGenderService _genderService;
        private readonly IWorkerNoteService _workerNoteService;


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
            _positionService = App.PositionService;
            _workerService = App.WorkerService;
            _genderService = App.GenderService;
            _workerNoteService = App.WorkerNoteService;

            var incomeChart = new IncomeChart(_givingService);
            var incomeVsExpenditure = new IncomeVsExpenditure(_givingService, _expenditureService);

            DataContext = new MainViewModel(_navigation, _givingService, _expenditureService, incomeChart, incomeVsExpenditure, _meetingCategoryService, 
                _meetingService, _givingCategoryService, _reportService, _positionService, _workerService, _genderService);

            SidebarControl.SetNavigationService(_navigation, _meetingCategoryService, _meetingService, _givingCategoryService, _givingService, 
                _expenditureService, _reportService, incomeChart, incomeVsExpenditure, _positionService, _workerService, _genderService, _workerNoteService);
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