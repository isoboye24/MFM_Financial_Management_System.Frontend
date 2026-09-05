using System.Windows;
using System.Windows.Controls;

namespace MFMFMSF.UI.Controls
{
    /// <summary>
    /// Interaction logic for PageTopBarWithoutButton.xaml
    /// </summary>
    public partial class PageTopBarWithoutButton : UserControl
    {
        public PageTopBarWithoutButton()
        {
            InitializeComponent();
        }

        // =====================================================
        // PAGE TITLE
        // =====================================================

        public static readonly DependencyProperty PageTitleProperty =
            DependencyProperty.Register(
                nameof(PageTitle),
                typeof(string),
                typeof(PageTopBarWithoutButton),
                new PropertyMetadata(string.Empty));

        public string PageTitle
        {
            get => (string)GetValue(PageTitleProperty);
            set => SetValue(PageTitleProperty, value);
        }


        // =====================================================
        // PAGE DESCRIPTION
        // =====================================================

        public static readonly DependencyProperty PageDescriptionProperty =
            DependencyProperty.Register(
                nameof(PageDescription),
                typeof(string),
                typeof(PageTopBarWithoutButton),
                new PropertyMetadata(string.Empty));

        public string PageDescription
        {
            get => (string)GetValue(PageDescriptionProperty);
            set => SetValue(PageDescriptionProperty, value);
        }
    }
}
