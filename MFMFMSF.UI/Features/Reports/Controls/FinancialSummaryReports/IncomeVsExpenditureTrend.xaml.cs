using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using SkiaSharp;
using System.Windows.Controls;

namespace MFMFMSF.UI.Features.Reports.Controls.FinancialSummaryReports
{
    public partial class IncomeVsExpenditureTrend : UserControl
    {
        public ISeries[] Series { get; set; }

        public Axis[] XAxes { get; set; }

        public Axis[] YAxes { get; set; }


        public IncomeVsExpenditureTrend()
        {
            InitializeComponent();

            CreateChart();
        }


        private void CreateChart()
        {
            // ==========================================
            // INCOME
            // ==========================================

            var income = new LineSeries<double>
            {
                Name = "Income",

                Values = new double[]
                {
                    98450,
                    110230,
                    123560,
                    129280,
                    148320
                },

                Stroke = new SolidColorPaint(
                    SKColors.SeaGreen)
                {
                    StrokeThickness = 2
                },

                Fill = null,

                GeometrySize = 6,

                GeometryFill = new SolidColorPaint(
                    SKColors.SeaGreen),

                GeometryStroke = null,

                DataLabelsSize = 7,

                DataLabelsPaint = new SolidColorPaint(
                    SKColors.SeaGreen),

                DataLabelsPosition =
                    LiveChartsCore.Measure.DataLabelsPosition.Top,

                DataLabelsFormatter =
                    point => $"${point.Coordinate.PrimaryValue:N0}"
            };


            // ==========================================
            // EXPENDITURE
            // ==========================================

            var expenditure = new LineSeries<double>
            {
                Name = "Expenditure",

                Values = new double[]
                {
                    62120,
                    71450,
                    75860,
                    95320,
                    87450
                },

                Stroke = new SolidColorPaint(
                    SKColors.Crimson)
                {
                    StrokeThickness = 2
                },

                Fill = null,

                GeometrySize = 6,

                GeometryFill = new SolidColorPaint(
                    SKColors.Crimson),

                GeometryStroke = null,

                DataLabelsSize = 7,

                DataLabelsPaint = new SolidColorPaint(
                    SKColors.Crimson),

                DataLabelsPosition =
                    LiveChartsCore.Measure.DataLabelsPosition.Top,

                DataLabelsFormatter =
                    point => $"${point.Coordinate.PrimaryValue:N0}"
            };


            // ==========================================
            // NET INCOME
            // ==========================================

            var netIncome = new ColumnSeries<double>
            {
                Name = "Net Income",

                Values = new double[]
                {
                    36330,
                    38780,
                    47700,
                    33960,
                    60870
                },

                Fill = new SolidColorPaint(
                    new SKColor(96, 70, 216)),

                Stroke = null,

                MaxBarWidth = 30,

                DataLabelsSize = 7,

                DataLabelsPaint = new SolidColorPaint(
                    new SKColor(96, 70, 216)),

                DataLabelsPosition =
                    LiveChartsCore.Measure.DataLabelsPosition.Top,

                DataLabelsFormatter =
                    point => $"${point.Coordinate.PrimaryValue:N0}"
            };


            Series = new ISeries[]
            {
                netIncome,
                income,
                expenditure
            };


            // ==========================================
            // X AXIS
            // ==========================================

            XAxes = new Axis[]
            {
                new Axis
                {
                    Labels = new[]
                    {
                        "Jan 2025",
                        "Feb 2025",
                        "Mar 2025",
                        "Apr 2025",
                        "May 2025"
                    },

                    TextSize = 8,

                    LabelsPaint = new SolidColorPaint(
                        new SKColor(48, 54, 83)),

                    SeparatorsPaint = null
                }
            };


            // ==========================================
            // Y AXIS
            // ==========================================

            YAxes = new Axis[]
            {
                new Axis
                {
                    MinLimit = 0,
                    MaxLimit = 160000,

                    TextSize = 8,

                    LabelsPaint = new SolidColorPaint(
                        new SKColor(48, 54, 83)),

                    Labeler = value =>
                        value == 0
                            ? "$0"
                            : $"${value / 1000:N0}K",

                    SeparatorsPaint =
                        new SolidColorPaint(
                            new SKColor(235, 237, 243))
                    {
                        StrokeThickness = 1
                    }
                }
            };


            DataContext = this;
        }
    }
}