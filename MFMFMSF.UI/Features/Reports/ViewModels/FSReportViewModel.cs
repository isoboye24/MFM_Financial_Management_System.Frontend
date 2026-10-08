using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MFMFMSF.UI.Features.Reports.ViewModels
{
    public class FSReportViewModel : INotifyPropertyChanged
    {

        public FSReportViewModel()
        {
            
        }

        public async Task LoadReportsAsync(int page, int pageSize)
        {
            // Implement the logic to load reports based on the page and pageSize
            // For example, you might call a service to fetch the reports from a database or API
            // Example:
            // Reports = await _reportService.GetReportsAsync(page, pageSize);
        }

        private void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(
                this,
                new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
