using System.Windows.Controls;
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;

namespace MFMFMSF.UI.Features.Offering.Controls
{
    public partial class OfferingSideCards : UserControl
    {
        public ISeries[] OfferingSeries { get; set; }

        public OfferingSideCards()
        {
            InitializeComponent();

            OfferingSeries = [
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