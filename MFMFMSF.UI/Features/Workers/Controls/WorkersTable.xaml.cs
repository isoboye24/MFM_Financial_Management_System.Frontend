using MFMFMSF.Core.Interfaces;
using MFMFMSF.Core.Models.Workers;
using MFMFMSF.UI.Navigation;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace MFMFMSF.UI.Features.Workers.Controls
{
    /// <summary>
    /// Interaction logic for WorkersTable.xaml
    /// </summary>
    public partial class WorkersTable : UserControl
    {
        public WorkersTable()
        {
            InitializeComponent();
        }

        // =====================================================
        // WORKERS
        // =====================================================

        public ObservableCollection<WorkersListItem> Workers
        {
            get => (ObservableCollection<WorkersListItem>)GetValue(WorkersProperty);
            set => SetValue(WorkersProperty, value);
        }

        public static readonly DependencyProperty WorkersProperty =
            DependencyProperty.Register(
                nameof(Workers),
                typeof(ObservableCollection<WorkersListItem>),
                typeof(WorkersTable),
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
                typeof(WorkersTable),
                new PropertyMetadata(null));


        // =====================================================
        // POSITION SERVICE
        // =====================================================

        public IPositionService? PositionService
        {
            get => (IPositionService?)GetValue(PositionServiceProperty);
            set => SetValue(PositionServiceProperty, value);
        }

        public static readonly DependencyProperty PositionServiceProperty =
            DependencyProperty.Register(
                nameof(PositionService),
                typeof(IPositionService),
                typeof(WorkersTable),
                new PropertyMetadata(null));
        
        
        // =====================================================
        // WORKER SERVICE
        // =====================================================

        public IWorkerService? WorkerService
        {
            get => (IWorkerService?)GetValue(WorkerServiceProperty);
            set => SetValue(WorkerServiceProperty, value);
        }

        public static readonly DependencyProperty WorkerServiceProperty =
            DependencyProperty.Register(
                nameof(WorkerService),
                typeof(IWorkerService),
                typeof(WorkersTable),
                new PropertyMetadata(null));


        // =====================================================
        // GENDER SERVICE
        // =====================================================

        public IGenderService? GenderService
        {
            get => (IGenderService?)GetValue(GenderServiceProperty);
            set => SetValue(GenderServiceProperty, value);
        }

        public static readonly DependencyProperty GenderServiceProperty =
            DependencyProperty.Register(
                nameof(GenderService),
                typeof(IGenderService),
                typeof(WorkersTable),
                new PropertyMetadata(null));
    }
}
