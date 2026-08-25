using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace MFMFMSF.UI.Controls
{
    public partial class PageTopBar : UserControl
    {
        public PageTopBar()
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
                typeof(PageTopBar),
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
                typeof(PageTopBar),
                new PropertyMetadata(string.Empty));

        public string PageDescription
        {
            get => (string)GetValue(PageDescriptionProperty);
            set => SetValue(PageDescriptionProperty, value);
        }


        // =====================================================
        // DATE RANGE TEXT
        // =====================================================

        public static readonly DependencyProperty DateRangeTextProperty =
            DependencyProperty.Register(
                nameof(DateRangeText),
                typeof(string),
                typeof(PageTopBar),
                new PropertyMetadata(string.Empty));

        public string DateRangeText
        {
            get => (string)GetValue(DateRangeTextProperty);
            set => SetValue(DateRangeTextProperty, value);
        }


        // =====================================================
        // ACTION BUTTON TEXT
        // =====================================================

        public static readonly DependencyProperty ActionTextProperty =
            DependencyProperty.Register(
                nameof(ActionText),
                typeof(string),
                typeof(PageTopBar),
                new PropertyMetadata(string.Empty));

        public string ActionText
        {
            get => (string)GetValue(ActionTextProperty);
            set => SetValue(ActionTextProperty, value);
        }


        // =====================================================
        // SHOW ACTION BUTTON
        // =====================================================

        public static readonly DependencyProperty ShowActionButtonProperty =
            DependencyProperty.Register(
                nameof(ShowActionButton),
                typeof(bool),
                typeof(PageTopBar),
                new PropertyMetadata(true));

        public bool ShowActionButton
        {
            get => (bool)GetValue(ShowActionButtonProperty);
            set => SetValue(ShowActionButtonProperty, value);
        }


        // =====================================================
        // ACTION COMMAND
        // =====================================================

        public static readonly DependencyProperty ActionCommandProperty =
            DependencyProperty.Register(
                nameof(ActionCommand),
                typeof(ICommand),
                typeof(PageTopBar),
                new PropertyMetadata(null));

        public ICommand ActionCommand
        {
            get => (ICommand)GetValue(ActionCommandProperty);
            set => SetValue(ActionCommandProperty, value);
        }


        // =====================================================
        // DATE RANGE COMMAND
        // =====================================================

        public static readonly DependencyProperty DateRangeCommandProperty =
            DependencyProperty.Register(
                nameof(DateRangeCommand),
                typeof(ICommand),
                typeof(PageTopBar),
                new PropertyMetadata(null));

        public ICommand DateRangeCommand
        {
            get => (ICommand)GetValue(DateRangeCommandProperty);
            set => SetValue(DateRangeCommandProperty, value);
        }
    }
}