using MFMFMSF.Core.Interfaces;
using MFMFMSF.Core.Models.Meetings;
using MFMFMSF.UI.Commands;
using MFMFMSF.UI.Features.Meetings.Controls;
using MFMFMSF.UI.Features.Meetings.Views;
using MFMFMSF.UI.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MFMFMSF.UI.Features.Reports.Controls.FinancialSummaryReports
{
    /// <summary>
    /// Interaction logic for ReportsTable.xaml
    /// </summary>
    public partial class ReportsTable : UserControl
    {

        public ReportsTable()
        {
            InitializeComponent();
            
        }


        //// =====================================================
        //// REPORTS
        //// =====================================================

        //public ObservableCollection<MeetingsByMonthAndYear> Meetings
        //{
        //    get => (ObservableCollection<MeetingsByMonthAndYear>)GetValue(MeetingsProperty);
        //    set => SetValue(MeetingsProperty, value);
        //}

        //public static readonly DependencyProperty MeetingsProperty =
        //    DependencyProperty.Register(
        //        nameof(Meetings),
        //        typeof(ObservableCollection<MeetingsByMonthAndYear>),
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
        
    }
}
