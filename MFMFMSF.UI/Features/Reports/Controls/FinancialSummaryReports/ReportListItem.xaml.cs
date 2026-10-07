using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace MFMFMSF.UI.Features.Reports.Controls.FinancialSummaryReports
{
    public partial class ReportListItem : UserControl
    {
        public ReportListItem()
        {
            InitializeComponent();
        }


        // ============================
        // TITLE
        // ============================

        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register(
                nameof(Title),
                typeof(string),
                typeof(ReportListItem),
                new PropertyMetadata(string.Empty));

        public string Title
        {
            get => (string)GetValue(TitleProperty);
            set => SetValue(TitleProperty, value);
        }


        // ============================
        // GENERATED TEXT
        // ============================

        public static readonly DependencyProperty GeneratedTextProperty =
            DependencyProperty.Register(
                nameof(GeneratedText),
                typeof(string),
                typeof(ReportListItem),
                new PropertyMetadata(string.Empty));

        public string GeneratedText
        {
            get => (string)GetValue(GeneratedTextProperty);
            set => SetValue(GeneratedTextProperty, value);
        }


        // ============================
        // PREVIEW COMMAND
        // ============================

        public static readonly DependencyProperty PreviewCommandProperty =
            DependencyProperty.Register(
                nameof(PreviewCommand),
                typeof(ICommand),
                typeof(ReportListItem),
                new PropertyMetadata(null));

        public ICommand PreviewCommand
        {
            get => (ICommand)GetValue(PreviewCommandProperty);
            set => SetValue(PreviewCommandProperty, value);
        }


        // ============================
        // DOWNLOAD COMMAND
        // ============================

        public static readonly DependencyProperty DownloadCommandProperty =
            DependencyProperty.Register(
                nameof(DownloadCommand),
                typeof(ICommand),
                typeof(ReportListItem),
                new PropertyMetadata(null));

        public ICommand DownloadCommand
        {
            get => (ICommand)GetValue(DownloadCommandProperty);
            set => SetValue(DownloadCommandProperty, value);
        }


        // ============================
        // DELETE COMMAND
        // ============================

        public static readonly DependencyProperty DeleteCommandProperty =
            DependencyProperty.Register(
                nameof(DeleteCommand),
                typeof(ICommand),
                typeof(ReportListItem),
                new PropertyMetadata(null));

        public ICommand DeleteCommand
        {
            get => (ICommand)GetValue(DeleteCommandProperty);
            set => SetValue(DeleteCommandProperty, value);
        }
    }
}