using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MFMFMSF.UI.Features.Workers.Controls
{
    /// <summary>
    /// Interaction logic for NewWorkersTrend.xaml
    /// </summary>
    public partial class NewWorkersTrend : UserControl
    {
        public ISeries[] Series { get; set; }

        public Axis[] XAxes { get; set; }

        public Axis[] YAxes { get; set; }


        public NewWorkersTrend()
        {
            InitializeComponent();

            CreateChart();

            DataContext = this;
        }


        private void CreateChart()
        {
            // =================================================
            // INCOME DATA
            // =================================================

            var values = new double[]
            {
                650,
                700,
                680,
                800,
                820,
                400,
                650,
                1250,
                550,
                750,
                1000,
                750
            };


            // =================================================
            // LINE SERIES
            // =================================================

            Series =
            [
                new LineSeries<double>
                {
                    Values = values,

                    // Purple line
                    Stroke = new SolidColorPaint(
                        new SKColor(0x66, 0x54, 0xD9))
                    {
                        StrokeThickness = 2
                    },

                    // Light purple area
                    Fill = new SolidColorPaint(
                        new SKColor(0x66, 0x54, 0xD9, 35)),

                    // Data point
                    GeometrySize = 6,

                    GeometryFill = new SolidColorPaint(
                        new SKColor(0x66, 0x54, 0xD9)),

                    GeometryStroke = new SolidColorPaint(
                        SKColors.White)
                    {
                        StrokeThickness = 1.5f
                    },

                    LineSmoothness = 0.7,

                    // Don't display values above points
                    DataLabelsSize = 0
                }
            ];


            // =================================================
            // X AXIS
            // =================================================

            XAxes =
            [
                new Axis
                {
                    Labels =
                    [
                        "May 1",
                        "",
                        "",
                        "",
                        "May 6",
                        "",
                        "May 11",
                        "",
                        "May 16",
                        "",
                        "May 21",
                        "",
                        "May 26",
                        "",
                        "May 31"
                    ],

                    TextSize = 8,

                    LabelsPaint = new SolidColorPaint(
                        new SKColor(0x68, 0x70, 0x8A)),

                    SeparatorsPaint = null,

                    TicksPaint = null,

                    Padding = new LiveChartsCore.Drawing.Padding(
                        0, 0, 0, 0)
                }
            ];


            // =================================================
            // Y AXIS
            // =================================================

            YAxes =
            [
                new Axis
                {
                    MinLimit = 0,
                    MaxLimit = 1500,

                    MinStep = 500,

                    TextSize = 8,

                    LabelsPaint = new SolidColorPaint(
                        new SKColor(0x68, 0x70, 0x8A)),

                    SeparatorsPaint = new SolidColorPaint(
                        new SKColor(0xE8, 0xEA, 0xF2))
                    {
                        StrokeThickness = 1
                    },

                    TicksPaint = null,

                    Labeler = value =>
                    {
                        if (value == 0)
                            return "$0";

                        return $"${value / 1000:0.#}K";
                    },

                    Padding = new LiveChartsCore.Drawing.Padding(
                        0, 0, 0, 0)
                }
            ];
        }
    }
}
