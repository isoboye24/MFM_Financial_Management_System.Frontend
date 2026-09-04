using MFMFMSF.UI.Commands;
using MFMFMSF.UI.Navigation;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace MFMFMSF.UI.Controls
{
    public partial class FormTitle : UserControl
    {
        public FormTitle()
        {
            InitializeComponent();

            BackCommand = new RelayCommand(_ => GoBack());
        }


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
                typeof(FormTitle),
                new PropertyMetadata(null));


        // =====================================================
        // BACK COMMAND
        // =====================================================

        public ICommand BackCommand
        {
            get => (ICommand)GetValue(BackCommandProperty);
            private set => SetValue(BackCommandPropertyKey, value);
        }

        private static readonly DependencyPropertyKey BackCommandPropertyKey =
            DependencyProperty.RegisterReadOnly(
                nameof(BackCommand),
                typeof(ICommand),
                typeof(FormTitle),
                new PropertyMetadata(null));

        public static readonly DependencyProperty BackCommandProperty =
            BackCommandPropertyKey.DependencyProperty;


        private void GoBack()
        {
            NavigationService?.GoBack();
        }


        // =====================================================
        // BREADCRUMB
        // =====================================================

        public string BreadcrumbText
        {
            get => (string)GetValue(BreadcrumbTextProperty);
            set => SetValue(BreadcrumbTextProperty, value);
        }

        public static readonly DependencyProperty BreadcrumbTextProperty =
            DependencyProperty.Register(
                nameof(BreadcrumbText),
                typeof(string),
                typeof(FormTitle),
                new PropertyMetadata(string.Empty));


        // =====================================================
        // CURRENT PAGE
        // =====================================================

        public string CurrentPageText
        {
            get => (string)GetValue(CurrentPageTextProperty);
            set => SetValue(CurrentPageTextProperty, value);
        }

        public static readonly DependencyProperty CurrentPageTextProperty =
            DependencyProperty.Register(
                nameof(CurrentPageText),
                typeof(string),
                typeof(FormTitle),
                new PropertyMetadata(string.Empty));


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
                typeof(FormTitle),
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
                typeof(FormTitle),
                new PropertyMetadata(string.Empty));
    }
}