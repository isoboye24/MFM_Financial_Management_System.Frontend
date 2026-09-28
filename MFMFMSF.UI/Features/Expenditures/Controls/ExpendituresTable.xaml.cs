using MFMFMSF.Core.Interfaces;
using MFMFMSF.Core.Models.Expenditures;
using MFMFMSF.Core.Models.Meetings;
using MFMFMSF.Infrastructure.Service;
using MFMFMSF.UI.Commands;
using MFMFMSF.UI.Features.Expenditures.Views;
using MFMFMSF.UI.Navigation;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace MFMFMSF.UI.Features.Expenditures.Controls
{
    /// <summary>
    /// Interaction logic for ExpendituresTable.xaml
    /// </summary>
    public partial class ExpendituresTable : UserControl
    {
        public ICommand EditExpenditureCommand { get; }

        public ExpendituresTable()
        {
            InitializeComponent();

            EditExpenditureCommand = new RelayCommandGeneric<ExpendituresByMonthAndYear>(EditExpenditure);
        }

        // =====================================================
        // EXPENDITURES
        // =====================================================

        public IReadOnlyList<ExpendituresByMonthAndYear>? Expenditures
        {
            get => (IReadOnlyList<ExpendituresByMonthAndYear>?)GetValue(ExpendituresProperty);
            set => SetValue(ExpendituresProperty, value);
        }

        public static readonly DependencyProperty ExpendituresProperty =
            DependencyProperty.Register(
                nameof(Expenditures),
                typeof(IReadOnlyList<ExpendituresByMonthAndYear>),
                typeof(ExpendituresTable),
                new PropertyMetadata(null));


        // =====================================================
        // NAVIGATION SERVICE
        // =====================================================

        public INavigationService? NavigationService
        {
            get => (INavigationService?)GetValue(NavigationServiceProperty);
            set => SetValue(NavigationServiceProperty, value);
        }

        public static readonly DependencyProperty NavigationServiceProperty =
            DependencyProperty.Register(
                nameof(NavigationService),
                typeof(INavigationService),
                typeof(ExpendituresTable),
                new PropertyMetadata(null));
       
        
        // =====================================================
        // EXPENDITURE SERVICE
        // =====================================================

        public IExpenditureService? ExpenditureService
        {
            get => (IExpenditureService?)GetValue(ExpenditureServiceProperty);
            set => SetValue(ExpenditureServiceProperty, value);
        }

        public static readonly DependencyProperty ExpenditureServiceProperty =
            DependencyProperty.Register(
                nameof(ExpenditureService),
                typeof(IExpenditureService),
                typeof(ExpendituresTable),
                new PropertyMetadata(null));



        // =====================================================
        // ACTIONS
        // =====================================================

        private void EditExpenditure(ExpendituresByMonthAndYear expenditure)
        {
            if (NavigationService == null || ExpenditureService == null)
            {
                MessageBox.Show("Navigation services are not configured.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            NavigationService.Navigate(new EditExpenditure(expenditure.Id, ExpenditureService, NavigationService));
        }
    }
}
