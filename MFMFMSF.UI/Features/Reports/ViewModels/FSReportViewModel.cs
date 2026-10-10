using MFMFMSF.Core.Interfaces;
using MFMFMSF.Core.Models.Reports;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MFMFMSF.UI.Features.Reports.ViewModels
{
    public class FSReportViewModel : INotifyPropertyChanged
    {
        private readonly IReportService _reportService;

        public ObservableCollection<FinancialSummaryMonthlyReportListItem> Reports { get; }
            = new();


        private int _currentPage = 1;

        public int CurrentPage
        {
            get => _currentPage;
            set
            {
                if (_currentPage == value)
                    return;

                _currentPage = value;
                OnPropertyChanged();
            }
        }


        private int _totalReports;

        public int TotalReports
        {
            get => _totalReports;
            set
            {
                if (_totalReports == value)
                    return;

                _totalReports = value;
                OnPropertyChanged();
            }
        }


        public FSReportViewModel(IReportService reportService)
        {
            _reportService = reportService;
        }


        public async Task LoadReportsAsync(int page, int pageSize)
        {
            var result = await _reportService.GetAllAsync(page, pageSize);

            Reports.Clear();

            foreach (var report in result.Reports)
            {
                Reports.Add(report);
            }

            CurrentPage = page;
            TotalReports = result.TotalItems;
        }


        private void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
