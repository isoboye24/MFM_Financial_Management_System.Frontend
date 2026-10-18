using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using MFMFMSF.Core.Interfaces;
using SkiaSharp;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace MFMFMSF.UI.Features.Offering.Controls
{
    /// <summary>
    /// Interaction logic for OfferingTrend.xaml
    /// </summary>
    public partial class OfferingTrend : UserControl
    {

        private IGivingService? _givingService;
        private bool _isLoading;

        public ISeries[] Series { get; private set; } = [];
        public Axis[] XAxes { get; private set; } = [];
        public Axis[] YAxes { get; private set; } = [];

        public OfferingTrend()
        {
            InitializeComponent();

            DataContext = this;

            CreateChart(new double[12]);
            Loaded += OfferingTrend_Loaded;
        }

        private async void OfferingTrend_Loaded(object sender, RoutedEventArgs e)
        {
            Loaded -= OfferingTrend_Loaded;
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

                var monthlyOfferings = new double[12];

                for (int month = 1; month <= 12; month++)
                {
                    var offerings = await _givingService.GetByMonthAndYearAsync(month, year, "Offering", page: 1, recordsPerPage: 1000);

                    monthlyOfferings[month - 1] = (double)offerings.Sum(
                        offering => offering.Amount);
                }

                CreateChart(monthlyOfferings);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Unable to load offering data for {year}.\n\n{ex.Message}",
                    "Offering Trend",
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
                        Name = "Offerings",
                        Values = values,

                        // Purple line 6941E8
                        Stroke = new SolidColorPaint(
                            new SKColor(0x69, 0x41, 0xE8))
                        {
                            StrokeThickness = 2
                        },

                        // Light purple area
                        Fill = new SolidColorPaint(
                            new SKColor(0x69, 0x41, 0xE8, 35)),


                        GeometrySize = 6,

                        // Purple data points
                        GeometryFill = new SolidColorPaint(
                            new SKColor(0x69, 0x41, 0xE8)),

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

                    LabelsPaint = new SolidColorPaint(
                        new SKColor(0x68, 0x70, 0x8A)),

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
