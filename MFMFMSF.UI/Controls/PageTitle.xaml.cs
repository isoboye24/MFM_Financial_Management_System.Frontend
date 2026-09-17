using System.Windows;
using System.Windows.Controls;

namespace MFMFMSF.UI.Controls
{
    public partial class PageTitle : UserControl
    {
        public PageTitle()
        {
            InitializeComponent();
        }


        // =====================================================
        // TITLE
        // =====================================================

        public string Title
        {
            get => (string)GetValue(TitleProperty);
            set => SetValue(TitleProperty, value);
        }

        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register(
                nameof(Title),
                typeof(string),
                typeof(PageTitle),
                new PropertyMetadata(string.Empty));


        // =====================================================
        // DESCRIPTION
        // =====================================================

        public string Description
        {
            get => (string)GetValue(DescriptionProperty);
            set => SetValue(DescriptionProperty, value);
        }

        public static readonly DependencyProperty DescriptionProperty =
            DependencyProperty.Register(
                nameof(Description),
                typeof(string),
                typeof(PageTitle),
                new PropertyMetadata(string.Empty));
    }
}