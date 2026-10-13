using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using MFMFMSF.Core.Interfaces;
using MFMFMSF.Core.Models.Givings;
using MFMFMSF.UI.Features.Dashboard.ViewModels;
using SkiaSharp;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace MFMFMSF.UI.Features.Dashboard.Controls
{
    public partial class IncomeChart : UserControl, INotifyPropertyChanged
    {
        private readonly IGivingService _givingService;

        public ObservableCollection<IncomeVM> IncomeItems { get; } = [];

        public ObservableCollection<ISeries> Series { get; } = [];

        private decimal _totalIncome;

        public decimal TotalIncome
        {
            get => _totalIncome;
            private set
            {
                if (_totalIncome == value)
                    return;

                _totalIncome = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public IncomeChart(IGivingService givingService)
        {
            InitializeComponent();

            _givingService = givingService;

            IncomeItems.Add(new IncomeVM
            {
                Name = "Offerings",
                Color = Brushes.MediumPurple
            });

            IncomeItems.Add(new IncomeVM
            {
                Name = "Tithes",
                Color = Brushes.MediumSeaGreen
            });

            IncomeItems.Add(new IncomeVM
            {
                Name = "Seeds",
                Color = Brushes.Orange
            });

            IncomeItems.Add(new IncomeVM
            {
                Name = "Other Income",
                Color = Brushes.Pink
            });

            DataContext = this;

            Loaded += IncomeChart_Loaded;
        }

        public async Task SetSelectedPeriodAsync(int month, int year)
        {
            var statistics = await _givingService
                .GetMonthlyStatisticsAsync(month, year);

            UpdateIncome(
                statistics.MonthlyOfferings,
                statistics.MonthlyTithes,
                statistics.MonthlySeeds,
                statistics.MonthlyOtherIncome);
        }

        private async void IncomeChart_Loaded(
            object sender,
            RoutedEventArgs e)
        {
            Loaded -= IncomeChart_Loaded;
            await LoadSelectedPeriodAsync();
        }

        private async void PeriodSelector_SelectionChanged(
            object sender,
            SelectionChangedEventArgs e)
        {
            if (!IsLoaded || PeriodSelector.SelectedItem is not ComboBoxItem)
                return;

            await LoadSelectedPeriodAsync();
        }

        private async Task LoadSelectedPeriodAsync()
        {
            try
            {
                var now = DateTime.Now;
                var selectedPeriod =
                    (PeriodSelector.SelectedItem as ComboBoxItem)?.Tag?.ToString();

                MonthlyGivingStatistics? monthly = null;
                AnnualGivingStatistics? annual = null;

                switch (selectedPeriod)
                {
                    case "LastMonth":
                        var previousMonth = now.AddMonths(-1);

                        monthly = await _givingService.GetMonthlyStatisticsAsync(
                            previousMonth.Month,
                            previousMonth.Year);
                        break;

                    case "ThisYear":
                        annual = await _givingService.GetAnnualStatisticsAsync(
                            now.Year);
                        break;

                    default:
                        monthly = await _givingService.GetMonthlyStatisticsAsync(
                            now.Month,
                            now.Year);
                        break;
                }

                if (monthly != null)
                {
                    UpdateIncome(
                        monthly.MonthlyOfferings,
                        monthly.MonthlyTithes,
                        monthly.MonthlySeeds,
                        monthly.MonthlyOtherIncome);
                }
                else if (annual != null)
                {
                    UpdateIncome(
                        annual.AnnualOfferings,
                        annual.AnnualTithes,
                        annual.AnnualSeeds,
                        annual.AnnualOtherIncome);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Unable to load income statistics.\n\n{ex.Message}",
                    "Income Overview",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
        }

        private void UpdateIncome(
            decimal offerings,
            decimal tithes,
            decimal seeds,
            decimal otherIncome)
        {
            IncomeItems[0].Amount = offerings;
            IncomeItems[1].Amount = tithes;
            IncomeItems[2].Amount = seeds;
            IncomeItems[3].Amount = otherIncome;

            TotalIncome = offerings + tithes + seeds + otherIncome;

            foreach (var item in IncomeItems)
            {
                item.Percentage = TotalIncome == 0
                    ? 0
                    : (double)(item.Amount / TotalIncome * 100);
            }

            Series.Clear();

            foreach (var item in IncomeItems)
            {
                Series.Add(new PieSeries<double>
                {
                    Name = item.Name,
                    Values = new[] { (double)item.Amount },
                    Fill = new SolidColorPaint(ToSkColor(item.Color)),
                    Stroke = null
                });
            }
        }

        private static SKColor ToSkColor(Brush brush)
        {
            if (brush is SolidColorBrush solidBrush)
            {
                var color = solidBrush.Color;

                return new SKColor(
                    color.R,
                    color.G,
                    color.B,
                    color.A);
            }

            return SKColors.Gray;
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