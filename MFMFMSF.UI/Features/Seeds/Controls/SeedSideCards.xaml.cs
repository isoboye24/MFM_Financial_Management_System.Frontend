using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using System.Windows.Controls;

namespace MFMFMSF.UI.Features.Seeds.Controls
{
    /// <summary>
    /// Interaction logic for SeedSideCards.xaml
    /// </summary>
    public partial class SeedSideCards : UserControl
    {
        public ISeries[] SeedSeries { get; set; }

        public SeedSideCards()
        {
            InitializeComponent();

            SeedSeries = [
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
