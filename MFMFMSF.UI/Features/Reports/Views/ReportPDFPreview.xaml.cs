using MFMFMSF.Core.Interfaces;
using Microsoft.Win32;
using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace MFMFMSF.UI.Features.Reports.Views
{
    public partial class ReportPDFPreview : UserControl
    {
        private readonly IReportService _reportService;
        private readonly Guid _reportId;
        private string? _temporaryPdfPath;

        public ReportPDFPreview(IReportService reportService, Guid reportId)
        {
            InitializeComponent();

            _reportService = reportService;
            _reportId = reportId;

            Loaded += ReportPDFPreview_Loaded;
            Unloaded += ReportPDFPreview_Unloaded;
        }

        private async void ReportPDFPreview_Loaded(object sender, RoutedEventArgs e)
        {
            await LoadPdfAsync();
        }

        private async Task LoadPdfAsync()
        {
            try
            {
                LoadingText.Text = "Loading PDF...";
                LoadingText.Visibility = Visibility.Visible;

                await PdfWebView.EnsureCoreWebView2Async();

                byte[] pdfBytes =
                    await _reportService.GetPDFAsync(_reportId);

                _temporaryPdfPath = Path.Combine(
                    Path.GetTempPath(),
                    $"MFM_ServiceReport_{_reportId:N}.pdf");

                await File.WriteAllBytesAsync(
                    _temporaryPdfPath,
                    pdfBytes);

                PdfWebView.CoreWebView2.Navigate(
                    new Uri(_temporaryPdfPath).AbsoluteUri);

                LoadingText.Visibility = Visibility.Collapsed;
            }
            catch (Exception ex)
            {
                LoadingText.Text = $"Unable to load PDF: {ex.Message}";
                LoadingText.Visibility = Visibility.Visible;
            }
        }

        private void ReportPDFPreview_Unloaded(object sender, RoutedEventArgs e)
        {
            // Keep the temporary PDF available for the viewer.
            // It will be reused if this page is opened again.
        }

        private async void DownloadPdfButton_Click(object sender, RoutedEventArgs e)
        {
            await DownloadPdfAsync();
        }


        private async Task DownloadPdfAsync()
        {
            try
            {
                var dialog = new SaveFileDialog
                {
                    Title = "Download Church Service Report",
                    FileName = $"MFM_ServiceReport_{_reportId:N}.pdf",
                    DefaultExt = ".pdf",
                    Filter = "PDF documents (*.pdf)|*.pdf",
                    AddExtension = true,
                    OverwritePrompt = true
                };

                if (dialog.ShowDialog() != true)
                    return;

                byte[] pdfBytes = await _reportService.GetPDFAsync(_reportId);

                if (pdfBytes.Length == 0)
                {
                    MessageBox.Show(
                        "The API returned an empty PDF file.",
                        "Download Failed",
                        MessageBoxButton.OK,
                        MessageBoxImage.Error);

                    return;
                }

                await File.WriteAllBytesAsync(dialog.FileName, pdfBytes);

                MessageBox.Show(
                    $"PDF saved successfully:\n{dialog.FileName}",
                    "Download Complete",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Unable to download the PDF.\n\n{ex.Message}",
                    "Download Failed",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
        }



        // =====================================================
        // NAVIGATION SERVICE
        // =====================================================

        public IReportService? ReportService
        {
            get => (IReportService?)GetValue(ReportServiceProperty);
            set => SetValue(ReportServiceProperty, value);
        }

        public static readonly DependencyProperty ReportServiceProperty =
            DependencyProperty.Register(
                nameof(ReportService),
                typeof(IReportService),
                typeof(ReportPDFPreview),
                new PropertyMetadata(null));
    }
}
