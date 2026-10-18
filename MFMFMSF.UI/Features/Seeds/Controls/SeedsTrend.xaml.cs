using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using MFMFMSF.Core.Interfaces;
using SkiaSharp;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace MFMFMSF.UI.Features.Seeds.Controls
{
    /// <summary>
    /// Interaction logic for SeedsTrend.xaml
    /// </summary>
    public partial class SeedsTrend : UserControl
    {
        private IGivingService? _givingService;
        private bool _isLoading;

        public ISeries[] Series { get; private set; } = [];
        public Axis[] XAxes { get; private set; } = [];
        public Axis[] YAxes { get; private set; } = [];

        public SeedsTrend()
        {
            InitializeComponent();

            DataContext = this;

            CreateChart(new double[12]);
            Loaded += SeedsTrend_Loaded;
        }

        private async void SeedsTrend_Loaded(object sender, RoutedEventArgs e)
        {
            Loaded -= SeedsTrend_Loaded;
            await LoadYearAsync(DateTime.Now.Year);
        }

        public async Task SetSelectedPeriodAsync(int month, int year)
        {
            await LoadYearAsync(year);
        }

        public async void SetGivingService(IGivingService givingService)
        {
            _givingService = givingService ?? throw new ArgumentNullException(nameof(givingService));

            if (IsLoaded)
            {
                await LoadYearAsync(DateTime.Now.Year);
            }
        }


        private async Task LoadYearAsync(int year)
        {
            if (_isLoading || _givingService == null)
                return;

            _isLoading = true;

            try
            {
                SelectedYear = year;

                var monthlySeeds = new double[12];

                for (int month = 1; month <= 12; month++)
                {
                    var seeds = await _givingService.GetByMonthAndYearAsync(month, year, "Seed", page: 1, recordsPerPage: 1000);
                    monthlySeeds[month - 1] = (double)seeds.Sum(
                        seed => seed.Amount);
                }

                CreateChart(monthlySeeds);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Unable to load seed data for {year}.\n\n{ex.Message}",
                    "Seed Trend",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
            finally
            {
                _isLoading = false;
            }
        }


        private void CreateChart(IReadOnlyList<double> values)
        {
            Series =
                [
                    new LineSeries<double>
                    {
                        Name = "Seeds",
                        Values = values,

                        // Green line 42A979
                        Stroke = new SolidColorPaint(new SKColor(0x42, 0xA9, 0x79))
                        {
                            StrokeThickness = 2
                        },

                        // Light green area
                        Fill = new SolidColorPaint(new SKColor(0x42, 0xA9, 0x79, 35)),


                        GeometrySize = 6,

                        // Green data points
                        GeometryFill = new SolidColorPaint(new SKColor(0x42, 0xA9, 0x79)),

                        GeometryStroke = new SolidColorPaint(SKColors.White)
                        {
                            StrokeThickness = 1.5f
                        },

                        LineSmoothness = 0.7,
                    }
                ];

            XAxes =
            [
                new Axis
                {
                    Labels =
                    [
                        "Jan", "Feb", "Mar", "Apr", "May", "Jun",
                        "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"
                    ],

                    TextSize = 9,

                    LabelsPaint = new SolidColorPaint(new SKColor(0x68, 0x70, 0x8A)),

                    SeparatorsPaint = null,
                    TicksPaint = null
                }
            ];

            double maximum = values.Count > 0
                ? values.Max()
                : 0;

            // Round the upper limit up to a useful interval.
            double step = maximum <= 10
                ? 2
                : Math.Pow(10, Math.Floor(Math.Log10(maximum)) - 1);

            if (step <= 0)
                step = 1;

            double axisMaximum = maximum <= 0
                ? 10
                : Math.Ceiling(maximum / step) * step;

            YAxes =
            [
                new Axis
                {
                    MinLimit = 0,
                    MaxLimit = axisMaximum,
                    MinStep = step,
                    TextSize = 9,

                    LabelsPaint = new SolidColorPaint(
                        new SKColor(0x68, 0x70, 0x8A)),

                    SeparatorsPaint = new SolidColorPaint(
                        new SKColor(0xE8, 0xEA, 0xF2))
                    {
                        StrokeThickness = 1
                    },

                    TicksPaint = null,
                    Labeler = value => value.ToString("N0")
                }
            ];

            // Refresh the chart bindings.
            DataContext = null;
            DataContext = this;
        }


        private int _selectedYear = DateTime.Now.Year;

        public int SelectedYear
        {
            get => _selectedYear;
            private set
            {
                if (_selectedYear == value)
                    return;

                _selectedYear = value;
                OnPropertyChanged(nameof(SelectedYear));
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(
                this,
                new PropertyChangedEventArgs(propertyName));
        }
    }
}
