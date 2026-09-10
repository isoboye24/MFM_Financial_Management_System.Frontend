using MFMFMSF.UI.Navigation;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using MaterialDesignThemes.Wpf;

namespace MFMFMSF.UI.Controls
{
    public partial class PageTitleBarWithCreateButton : UserControl
    {
        public PageTitleBarWithCreateButton()
        {
            InitializeComponent();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            if (NavigationService?.CanGoBack == true)
            {
                NavigationService.GoBack();
            }
        }

        // -----------------------------------------
        // Breadcrumb
        // -----------------------------------------

        public string BreadcrumbText
        {
            get => (string)GetValue(BreadcrumbTextProperty);
            set => SetValue(BreadcrumbTextProperty, value);
        }

        public static readonly DependencyProperty BreadcrumbTextProperty =
            DependencyProperty.Register(
                nameof(BreadcrumbText),
                typeof(string),
                typeof(PageTitleBarWithCreateButton),
                new PropertyMetadata(string.Empty));


        // -----------------------------------------
        // Current Page
        // -----------------------------------------

        public string CurrentPageText
        {
            get => (string)GetValue(CurrentPageTextProperty);
            set => SetValue(CurrentPageTextProperty, value);
        }

        public static readonly DependencyProperty CurrentPageTextProperty =
            DependencyProperty.Register(
                nameof(CurrentPageText),
                typeof(string),
                typeof(PageTitleBarWithCreateButton),
                new PropertyMetadata(string.Empty));


        // -----------------------------------------
        // Title
        // -----------------------------------------

        public string Title
        {
            get => (string)GetValue(TitleProperty);
            set => SetValue(TitleProperty, value);
        }

        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register(
                nameof(Title),
                typeof(string),
                typeof(PageTitleBarWithCreateButton),
                new PropertyMetadata(string.Empty));


        // -----------------------------------------
        // Description
        // -----------------------------------------

        public string Description
        {
            get => (string)GetValue(DescriptionProperty);
            set => SetValue(DescriptionProperty, value);
        }

        public static readonly DependencyProperty DescriptionProperty =
            DependencyProperty.Register(
                nameof(Description),
                typeof(string),
                typeof(PageTitleBarWithCreateButton),
                new PropertyMetadata(string.Empty));


        // -----------------------------------------
        // Action Text
        // -----------------------------------------

        public string ActionText
        {
            get => (string)GetValue(ActionTextProperty);
            set => SetValue(ActionTextProperty, value);
        }

        public static readonly DependencyProperty ActionTextProperty =
            DependencyProperty.Register(
                nameof(ActionText),
                typeof(string),
                typeof(PageTitleBarWithCreateButton),
                new PropertyMetadata("Create"));


        // -----------------------------------------
        // Action Icon
        // -----------------------------------------

        public PackIconKind ActionIcon
        {
            get => (PackIconKind)GetValue(ActionIconProperty);
            set => SetValue(ActionIconProperty, value);
        }

        public static readonly DependencyProperty ActionIconProperty =
            DependencyProperty.Register(
                nameof(ActionIcon),
                typeof(PackIconKind),
                typeof(PageTitleBarWithCreateButton),
                new PropertyMetadata(PackIconKind.Plus));


        // -----------------------------------------
        // Action Command
        // -----------------------------------------

        public ICommand? ActionCommand
        {
            get => (ICommand?)GetValue(ActionCommandProperty);
            set => SetValue(ActionCommandProperty, value);
        }

        public static readonly DependencyProperty ActionCommandProperty =
            DependencyProperty.Register(
                nameof(ActionCommand),
                typeof(ICommand),
                typeof(PageTitleBarWithCreateButton),
                new PropertyMetadata(null));


        // -----------------------------------------
        // Navigation Service
        // -----------------------------------------

        public INavigationService? NavigationService
        {
            get => (INavigationService?)GetValue(NavigationServiceProperty);
            set => SetValue(NavigationServiceProperty, value);
        }

        public static readonly DependencyProperty NavigationServiceProperty =
            DependencyProperty.Register(
                nameof(NavigationService),
                typeof(INavigationService),
                typeof(PageTitleBarWithCreateButton),
                new PropertyMetadata(null));
    }
}