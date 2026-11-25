using MFMFMSF.Core.Interfaces;
using MFMFMSF.UI.Features.Dashboard.Controls;
using MFMFMSF.UI.Features.Dashboard.ViewModels;
using MFMFMSF.UI.Navigation;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace MFMFMSF.UI.Controls
{
    public partial class Sidebar : UserControl
    {
        public Sidebar()
        {
            InitializeComponent();
        }

        public void SetNavigationService(INavigationService navigation, IMeetingCategoryService meetingCategoryService, IMeetingService meetingService, 
            IGivingCategoryService givingCategoryService, IGivingService givingService, IExpenditureService expenditureService, IReportService reportService, 
            IncomeChart incomeChart, IncomeVsExpenditure incomeVsExpenditure, IPositionService positionService, IWorkerService workerService, 
            IGenderService genderService, IWorkerNoteService workerNoteService)
        {
            DataContext = new SidebarViewModel(navigation, meetingCategoryService, meetingService, givingCategoryService, givingService, expenditureService, reportService, 
                incomeChart, incomeVsExpenditure, positionService, workerService, genderService, workerNoteService);
        }

        private void SidebarMenuItem_Click(object sender, RoutedEventArgs e)
        {
            // Deselect every menu item
            foreach (var child in NavigationMenu.Children)
            {
                if (child is SidebarMenuItem menuItem)
                {
                    menuItem.IsSelected = false;
                }
            }

            // Deselect Settings separately
            SettingsMenuItem.IsSelected = false;


            // Select the clicked item
            if (sender is SidebarMenuItem clickedItem)
            {
                clickedItem.IsSelected = true;
            }
        }

        private void ChurchLogo_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (DataContext is SidebarViewModel viewModel)
            {
                viewModel.NavigateDashboardCommand.Execute(null);
            }

            e.Handled = true;
        }
    }
}