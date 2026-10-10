using MFMFMSF.Core.Interfaces;
using MFMFMSF.Core.Models.Meetings;
using MFMFMSF.Core.Models.Reports;
using MFMFMSF.Infrastructure.Service;
using MFMFMSF.UI.Commands;
using MFMFMSF.UI.Features.Meetings.Controls;
using MFMFMSF.UI.Navigation;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace MFMFMSF.UI.Features.Reports.Controls.FinancialSummaryReports
{
    /// <summary>
    /// Interaction logic for ReportsTable.xaml
    /// </summary>
    public partial class ReportsTable : UserControl
    {
        public ICommand DeleteReportCommand { get; }

        public ReportsTable()
        {
            InitializeComponent();
            
            //DeleteReportCommand =  new RelayCommandGeneric<FinancialSummaryMonthlyReportListItem>(DeleteReport);

            //DataContext = this;
        }


        //// =====================================================
        //// REORTS
        //// =====================================================

        //public ObservableCollection<FinancialSummaryMonthlyReportListItem> Reports
        //{
        //    get => (ObservableCollection<FinancialSummaryMonthlyReportListItem>)GetValue(ReportsProperty);
        //    set => SetValue(ReportsProperty, value);
        //}

        //public static readonly DependencyProperty ReportsProperty =
        //    DependencyProperty.Register(
        //        nameof(Reports),
        //        typeof(ObservableCollection<FinancialSummaryMonthlyReportListItem>),
        //        typeof(ReportsTable),
        //        new PropertyMetadata(null));


        //// =====================================================
        //// NAVIGATION SERVICE
        //// =====================================================

        //public INavigationService? NavigationService
        //{
        //    get => (INavigationService?)GetValue(NavigationServiceProperty);
        //    set => SetValue(NavigationServiceProperty, value);
        //}

        //public static readonly DependencyProperty NavigationServiceProperty =
        //    DependencyProperty.Register(
        //        nameof(NavigationService),
        //        typeof(INavigationService),
        //        typeof(ReportsTable),
        //        new PropertyMetadata(null));
        
        
        //// =====================================================
        //// NAVIGATION SERVICE
        //// =====================================================

        //public IReportService? ReportService
        //{
        //    get => (IReportService?)GetValue(ReportServiceProperty);
        //    set => SetValue(ReportServiceProperty, value);
        //}

        //public static readonly DependencyProperty ReportServiceProperty =
        //    DependencyProperty.Register(
        //        nameof(ReportService),
        //        typeof(IReportService),
        //        typeof(ReportsTable),
        //        new PropertyMetadata(null));



        //// =====================================================
        //// ACTIONS
        //// =====================================================

        //private async void DeleteReport(FinancialSummaryMonthlyReportListItem report)
        //{
        //    var result = MessageBox.Show($"Are you sure you want to delete '{report.MonthName}'?", "Delete Church Service", MessageBoxButton.YesNo, MessageBoxImage.Warning);

        //    if (result != MessageBoxResult.Yes)
        //        return;

        //    try
        //    {
        //        if (ReportService == null)
        //        {
        //            MessageBox.Show("Report service is not configured.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        //            return;
        //        }

        //        await ReportService.DeleteAsync(report.Id);
        //        Reports.Remove(report);

        //        MessageBox.Show("Report deleted successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show($"The report could not be deleted.\n\n{ex.Message}", "Delete Failed", MessageBoxButton.OK, MessageBoxImage.Error);
        //    }
        //}
    }
}
