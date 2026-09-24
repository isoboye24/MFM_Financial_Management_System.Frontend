using MFMFMSF.Core.Models.Expenditures;
using MFMFMSF.UI.Navigation;
using System.Windows;
using System.Windows.Controls;

namespace MFMFMSF.UI.Features.Expenditures.Controls
{
    /// <summary>
    /// Interaction logic for ExpendituresTable.xaml
    /// </summary>
    public partial class ExpendituresTable : UserControl
    {
        public ExpendituresTable()
        {
            InitializeComponent();
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
    }
}
