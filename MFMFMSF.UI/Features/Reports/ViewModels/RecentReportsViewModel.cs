using Microsoft.Win32;
using MFMFMSF.Core.Interfaces;
using MFMFMSF.Core.Models.Reports;
using MFMFMSF.UI.Commands;
using MFMFMSF.UI.Features.Reports.Views;
using MFMFMSF.UI.Navigation;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;

namespace MFMFMSF.UI.Features.Reports.ViewModels
{
    public class RecentReportsViewModel : INotifyPropertyChanged
    {
        private readonly INavigationService _navigationService;
        private readonly IReportService _reportService;

        private bool _isLoading;
        private bool _hasReports;

        public ObservableCollection<FinancialSummaryMonthlyReportListItem>RecentReports { get; } = new();

        public ICommand ViewAllReportsCommand { get; }
        public ICommand DownloadReportCommand { get; }

        public bool IsLoading
        {
            get => _isLoading;
            private set
            {
                if (_isLoading == value) return;
                _isLoading = value;
                OnPropertyChanged();
            }
        }

        public bool HasReports
        {
            get => _hasReports;
            private set
            {
                if (_hasReports == value) return;
                _hasReports = value;
                OnPropertyChanged();
            }
        }

        public RecentReportsViewModel(INavigationService navigationService, IReportService reportService)
        {
            _navigationService = navigationService;
            _reportService = reportService;

            ViewAllReportsCommand = new RelayCommand(_ => ViewAllReports());

            DownloadReportCommand = new RelayCommand(async parameter =>
            {
                if (parameter is FinancialSummaryMonthlyReportListItem report)
                    await DownloadReportAsync(report);
            });
        }

        public async Task LoadRecentReportsAsync()
        {
            if (IsLoading) return;

            IsLoading = true;

            try
            {
                var (reports, _) = await _reportService.GetAllAsync(page: 1, recordsPerPage: 3);

                RecentReports.Clear();

                foreach (var report in reports.OrderByDescending(r => r.Year).ThenByDescending(r => r.Month).Take(3))
                {
                    RecentReports.Add(report);
                }

                HasReports = RecentReports.Count > 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Unable to load recent reports.\n\n{ex.Message}",
                    "Recent Reports",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
            finally
            {
                IsLoading = false;
            }
        }

        private async Task DownloadReportAsync(
            FinancialSummaryMonthlyReportListItem report)
        {
            try
            {
                var dialog = new SaveFileDialog
                {
                    Title = "Download Financial Summary Report",
                    FileName = $"MFM_ServiceReport_{report.MonthName}_{report.Year}.pdf",
                    DefaultExt = ".pdf",
                    Filter = "PDF documents (*.pdf)|*.pdf",
                    AddExtension = true,
                    OverwritePrompt = true
                };

                if (dialog.ShowDialog() != true)
                    return;

                byte[] pdfBytes = await _reportService.GetPDFAsync(report.Id);

                if (pdfBytes.Length == 0)
                {
                    MessageBox.Show(
                        "The API returned an empty PDF file.",
                        "Download Report",
                        MessageBoxButton.OK,
                        MessageBoxImage.Warning);
                    return;
                }

                await File.WriteAllBytesAsync(dialog.FileName, pdfBytes);

                MessageBox.Show(
                    $"Report downloaded successfully:\n{dialog.FileName}",
                    "Download Report",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Unable to download the report.\n\n{ex.Message}",
                    "Download Report",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
        }

        private void ViewAllReports()
        {
            _navigationService.Navigate(
                new ViewAllFSReports(_navigationService, _reportService));
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        private void OnPropertyChanged(
            [CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(
                this,
                new PropertyChangedEventArgs(propertyName));
        }
    }
}