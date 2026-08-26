using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
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

namespace MFMFMSF.UI.Features.Tithes.Controls
{
    /// <summary>
    /// Interaction logic for TitheSideCards.xaml
    /// </summary>
    public partial class TitheSideCards : UserControl
    {
        public ISeries[] TitheSeries { get; set; }

        public TitheSideCards()
        {
            InitializeComponent();

            TitheSeries = [
                                new PieSeries<double>
                                {
                                    Values = [18620],
                                    InnerRadius = 28
                                },

                                new PieSeries<double>
                                {
                                    Values = [28450],
                                    InnerRadius = 28
                                },

                                new PieSeries<double>
                                {
                                    Values = [6870],
                                    InnerRadius = 28
                                },

                                new PieSeries<double>
                                {
                                    Values = [2960],
                                    InnerRadius = 28
                                }
                            ];

            DataContext = this;
        }
    }
}
