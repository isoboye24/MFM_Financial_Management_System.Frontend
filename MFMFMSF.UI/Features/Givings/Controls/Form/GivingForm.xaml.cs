using MFMFMSF.Core.Models.GivingCategories;
using MFMFMSF.UI.Features.Meetings.Controls.Form;
using System.Windows;
using System.Windows.Controls;

namespace MFMFMSF.UI.Features.Givings.Controls.Form
{
    /// <summary>
    /// Interaction logic for GivingForm.xaml
    /// </summary>
    public partial class GivingForm : UserControl
    {
        public GivingForm()
        {
            InitializeComponent();
        }

        // =====================================================
        // GIVING CATEGORIES
        // =====================================================

        public IEnumerable<GivingCategoryListItem>? GivingCategories
        {
            get => (IEnumerable<GivingCategoryListItem>?)GetValue(GivingCategoriesProperty);
            set => SetValue(GivingCategoriesProperty, value);
        }

        public static readonly DependencyProperty GivingCategoriesProperty =
            DependencyProperty.Register(
                nameof(GivingCategories),
                typeof(IEnumerable<GivingCategoryListItem>),
                typeof(GivingForm),
                new PropertyMetadata(null));


        // =====================================================
        // GIVING CATEGORY
        // =====================================================

        public GivingCategoryListItem? SelectedGivingCategory
        {
            get => (GivingCategoryListItem?)GetValue(SelectedGivingCategoryProperty);
            set => SetValue(SelectedGivingCategoryProperty, value);
        }

        public static readonly DependencyProperty SelectedGivingCategoryProperty =
            DependencyProperty.Register(
                nameof(SelectedGivingCategory),
                typeof(GivingCategoryListItem),
                typeof(GivingForm),
                new PropertyMetadata(null));


        // =====================================================
        // DATE
        // =====================================================

        public DateTime? GivingDate
        {
            get => (DateTime?)GetValue(GivingDateProperty);
            set => SetValue(GivingDateProperty, value);
        }

        public static readonly DependencyProperty GivingDateProperty =
            DependencyProperty.Register(
                nameof(GivingDate),
                typeof(DateTime?),
                typeof(GivingForm),
                new PropertyMetadata(null));

        // =====================================================
        // AMOUNT
        // =====================================================

        public string Amount
        {
            get => (string)GetValue(AmountProperty);
            set => SetValue(AmountProperty, value);
        }

        public static readonly DependencyProperty AmountProperty =
            DependencyProperty.Register(
                nameof(Amount),
                typeof(string),
                typeof(GivingForm),
                new PropertyMetadata(string.Empty));


        // =====================================================
        // MEETING ID
        // =====================================================

        public string MeetingId
        {
            get => (string)GetValue(MeetingIdProperty);
            set => SetValue(MeetingIdProperty, value);
        }

        public static readonly DependencyProperty MeetingIdProperty =
            DependencyProperty.Register(
                nameof(MeetingId),
                typeof(string),
                typeof(GivingForm),
                new PropertyMetadata(string.Empty));


        // =====================================================
        // SUMMARY
        // =====================================================

        public string Summary
        {
            get => (string)GetValue(SummaryProperty);
            set => SetValue(SummaryProperty, value);
        }

        public static readonly DependencyProperty SummaryProperty =
            DependencyProperty.Register(
                nameof(Summary),
                typeof(string),
                typeof(GivingForm),
                new PropertyMetadata(string.Empty));
    }
}
