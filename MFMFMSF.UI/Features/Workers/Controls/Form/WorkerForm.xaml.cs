using MFMFMSF.Core.Models.Genders;
using MFMFMSF.Core.Models.Positions;
using System.Windows;
using System.Windows.Controls;

namespace MFMFMSF.UI.Features.Workers.Controls.Form
{
    /// <summary>
    /// Interaction logic for WorkerForm.xaml
    /// </summary>
    public partial class WorkerForm : UserControl
    {
        public WorkerForm()
        {
            InitializeComponent();
        }

        // =====================================================
        // FIRST NAME
        // =====================================================

        public string FirstName
        {
            get => (string)GetValue(FirstNameProperty);
            set => SetValue(FirstNameProperty, value);
        }

        public static readonly DependencyProperty FirstNameProperty =
            DependencyProperty.Register(
                nameof(FirstName),
                typeof(string),
                typeof(WorkerForm),
                new PropertyMetadata(string.Empty));


        
        // =====================================================
        // LAST NAME
        // =====================================================

        public string LastName
        {
            get => (string)GetValue(LastNameProperty);
            set => SetValue(LastNameProperty, value);
        }

        public static readonly DependencyProperty LastNameProperty =
            DependencyProperty.Register(
                nameof(LastName),
                typeof(string),
                typeof(WorkerForm),
                new PropertyMetadata(string.Empty));



        // =====================================================
        // ADDRESS
        // =====================================================

        public string Address
        {
            get => (string)GetValue(AddressProperty);
            set => SetValue(AddressProperty, value);
        }

        public static readonly DependencyProperty AddressProperty =
            DependencyProperty.Register(
                nameof(Address),
                typeof(string),
                typeof(WorkerForm),
                new PropertyMetadata(string.Empty));


        // =====================================================
        // PHONE NUMBER
        // =====================================================

        public string PhoneNumber
        {
            get => (string)GetValue(PhoneNumberProperty);
            set => SetValue(PhoneNumberProperty, value);
        }

        public static readonly DependencyProperty PhoneNumberProperty =
            DependencyProperty.Register(
                nameof(PhoneNumber),
                typeof(string),
                typeof(WorkerForm),
                new PropertyMetadata(string.Empty));



        // =====================================================
        // POSITIONS
        // =====================================================

        public IEnumerable<PositionListItem>? Positions
        {
            get => (IEnumerable<PositionListItem>?)GetValue(PositionsProperty);
            set => SetValue(PositionsProperty, value);
        }

        public static readonly DependencyProperty PositionsProperty =
            DependencyProperty.Register(
                nameof(Positions),
                typeof(IEnumerable<PositionListItem>),
                typeof(WorkerForm),
                new PropertyMetadata(null));


        // =====================================================
        // POSITION ID
        // =====================================================

        public PositionListItem? SelectedPosition
        {
            get => (PositionListItem?)GetValue(SelectedPositionProperty);
            set => SetValue(SelectedPositionProperty, value);
        }

        public static readonly DependencyProperty SelectedPositionProperty =
            DependencyProperty.Register(
                nameof(SelectedPosition),
                typeof(PositionListItem),
                typeof(WorkerForm),
                new PropertyMetadata(null));
        
        
        
        // =====================================================
        // GENDERS
        // =====================================================

        public IEnumerable<GenderListItem>? Genders
        {
            get => (IEnumerable<GenderListItem>?)GetValue(GendersProperty);
            set => SetValue(GendersProperty, value);
        }

        public static readonly DependencyProperty GendersProperty =
            DependencyProperty.Register(
                nameof(Genders),
                typeof(IEnumerable<GenderListItem>),
                typeof(WorkerForm),
                new PropertyMetadata(null));


        // =====================================================
        // GENDER ID
        // =====================================================

        public GenderListItem? SelectedGender
        {
            get => (GenderListItem?)GetValue(SelectedGenderProperty);
            set => SetValue(SelectedGenderProperty, value);
        }

        public static readonly DependencyProperty SelectedGenderProperty =
            DependencyProperty.Register(
                nameof(SelectedGender),
                typeof(GenderListItem),
                typeof(WorkerForm),
                new PropertyMetadata(null));
    }
}
