
using MFMFMSF.Core.Interfaces;
using MFMFMSF.Core.Models.Givings;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace MFMFMSF.UI.Features.Dashboard.Controls
{
    public partial class RecentRecords : UserControl, INotifyPropertyChanged
    {
        private readonly IGivingService _givingService;
        private bool _isLoading;

        public ObservableCollection<GivingByMonthAndYear> RecentGivings { get; }
            = new();

        private bool _hasGivings;

        public bool HasGivings
        {
            get => _hasGivings;
            private set
            {
                if (_hasGivings == value)
                    return;

                _hasGivings = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public RecentRecords(IGivingService givingService)
        {
            InitializeComponent();

            _givingService = givingService
                ?? throw new ArgumentNullException(nameof(givingService));

            DataContext = this;

            Loaded += RecentRecords_Loaded;
        }

        private async void RecentRecords_Loaded(
            object sender,
            RoutedEventArgs e)
        {
            Loaded -= RecentRecords_Loaded;

            var now = DateTime.Now;
            await SetSelectedPeriodAsync(now.Month, now.Year);
        }

        public async Task SetSelectedPeriodAsync(int month, int year)
        {
            if (_isLoading)
                return;

            _isLoading = true;

            try
            {
                var givings = await _givingService.GetByMonthAndYearAsync(
                    month,
                    year,
                    categoryName: null,
                    page: 1,
                    recordsPerPage: 4);

                RecentGivings.Clear();

                foreach (var giving in givings)
                {
                    System.Diagnostics.Debug.WriteLine(
        $"ID: {giving.Id} | Summary: [{giving.Summary}]");

                    RecentGivings.Add(giving);
                }

                HasGivings = RecentGivings.Count > 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Unable to load recent givings.\n\n{ex.Message}",
                    "Recent Givings",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
            finally
            {
                _isLoading = false;
            }
        }

        private void OnPropertyChanged(
            [CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(
                this,
                new PropertyChangedEventArgs(propertyName));
        }
    }
}
