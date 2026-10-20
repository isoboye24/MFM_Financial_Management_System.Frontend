using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using MFMFMSF.Core.Interfaces;
using SkiaSharp;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace MFMFMSF.UI.Features.Meetings.Controls.ViewMeeting
{
    public partial class MaleAttendanceTrend : UserControl
    {
        private IMeetingService? _meetingService;
        private bool _isLoading;

        public ISeries[] Series { get; private set; } = [];
        public Axis[] XAxes { get; private set; } = [];
        public Axis[] YAxes { get; private set; } = [];

        public MaleAttendanceTrend()
        {
            InitializeComponent();

            DataContext = this;

            CreateChart(new double[12]);
            Loaded += MaleAttendanceTrend_Loaded;
        }

        private async void MaleAttendanceTrend_Loaded(object sender, RoutedEventArgs e)
        {
            Loaded -= MaleAttendanceTrend_Loaded;
            await LoadYearAsync(DateTime.Now.Year);
        }

        public async Task SetSelectedPeriodAsync(int month, int year)
        {
            await LoadYearAsync(year);
        }

        public async void SetMeetingService(IMeetingService meetingService)
        {
            _meetingService = meetingService ?? throw new ArgumentNullException(nameof(meetingService));

            if (IsLoaded)
            {
                await LoadYearAsync(DateTime.Now.Year);
            }
        }


        private async Task LoadYearAsync(int year)
        {
            if (_isLoading || _meetingService == null)
                return;

            _isLoading = true;

            try
            {
                SelectedYear = year;

                var monthlyAttendance = new double[12];

                for (int month = 1; month <= 12; month++)
                {
                    var meetings = await _meetingService.GetByMonthAndYearAsync(
                        month,
                        year,
                        page: 1,
                        recordsPerPage: 1000);

                    monthlyAttendance[month - 1] = meetings.Sum(
                        meeting => meeting.NoOfMaleAttendance);
                }

                CreateChart(monthlyAttendance);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Unable to load male attendance for {year}.\n\n{ex.Message}",
                    "Male Attendance Trend",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
            finally
            {
                _isLoading = false;
            }
        }

        private SKColor GetChartColor(string resourceKey)
        {
            if (Application.Current.TryFindResource(resourceKey)
                is SolidColorBrush brush)
            {
                var color = brush.Color;

                return new SKColor(
                    color.R,
                    color.G,
                    color.B,
                    color.A);
            }

            throw new InvalidOperationException(
                $"Theme resource '{resourceKey}' was not found.");
        }

        private void CreateChart(IReadOnlyList<double> values)
        {
            // Get semantic brushes from the active theme.
            var seriesColor = GetChartColor("ChartSeriesBrushMale");
            var areaColor = GetChartColor("ChartAreaBrushMale");
            var labelColor = GetChartColor("ChartLabelBrush");
            var separatorColor = GetChartColor("ChartSeparatorBrush");
            var pointBorderColor = GetChartColor("ChartPointBorderBrush");

            Series =
            [
                new LineSeries<double>
        {
            Name = "T. A.",
            Values = values,

            Stroke = new SolidColorPaint(seriesColor)
            {
                StrokeThickness = 2
            },

            Fill = new SolidColorPaint(areaColor),

            GeometrySize = 6,

            GeometryFill = new SolidColorPaint(seriesColor),

            GeometryStroke = new SolidColorPaint(pointBorderColor)
            {
                StrokeThickness = 1.5f
            },

            LineSmoothness = 0.7
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

            LabelsPaint = new SolidColorPaint(labelColor),

            SeparatorsPaint = null,
            TicksPaint = null
        }
            ];

            double maximum = values.Count > 0
                ? values.Max()
                : 0;

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

            LabelsPaint = new SolidColorPaint(labelColor),

            SeparatorsPaint = new SolidColorPaint(separatorColor)
            {
                StrokeThickness = 1
            },

            TicksPaint = null,
            Labeler = value => value.ToString("N0")
        }
            ];

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
