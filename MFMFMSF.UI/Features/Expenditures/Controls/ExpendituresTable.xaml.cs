using MFMFMSF.Core.Interfaces;
using MFMFMSF.Core.Models.Expenditures;
using MFMFMSF.UI.Commands;
using MFMFMSF.UI.Features.Expenditures.Views;
using MFMFMSF.UI.Navigation;
using System.Collections.ObjectModel;
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
        public ICommand DeleteExpenditureCommand { get; }

        public ExpendituresTable()
        {
            InitializeComponent();

            EditExpenditureCommand = new RelayCommandGeneric<ExpendituresByMonthAndYear>(EditExpenditure);
            DeleteExpenditureCommand = new RelayCommandGeneric<ExpendituresByMonthAndYear>(DeleteExpenditure);
        }

        // =====================================================
        // EXPENDITURES
        // =====================================================

        public ObservableCollection<ExpendituresByMonthAndYear> Expenditures
        {
            get => (ObservableCollection<ExpendituresByMonthAndYear>)GetValue(ExpendituresProperty);
            set => SetValue(ExpendituresProperty, value);
        }

        public static readonly DependencyProperty ExpendituresProperty =
            DependencyProperty.Register(
                nameof(Expenditures),
                typeof(ObservableCollection<ExpendituresByMonthAndYear>),
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
        
        private async void DeleteExpenditure(ExpendituresByMonthAndYear expenditure)
        {
            var result = MessageBox.Show($"Are you sure you want to delete '{expenditure.Summary}'?", "Delete Expenditure", MessageBoxButton.YesNo, MessageBoxImage.Warning);

            if (result != MessageBoxResult.Yes)
                return;

            try
            {
                if (ExpenditureService == null)
                {
                    MessageBox.Show("Expenditure is not configured.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                await ExpenditureService.DeleteAsync(expenditure.Id);

                Expenditures.Remove(expenditure);

                MessageBox.Show("Expenditure deleted successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"The expenditure could not be deleted.\n\n{ex.Message}", "Delete Failed", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
