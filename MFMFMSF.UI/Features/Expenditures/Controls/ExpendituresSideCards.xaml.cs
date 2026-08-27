using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using System.Windows.Controls;

namespace MFMFMSF.UI.Features.Expenditures.Controls
{
    /// <summary>
    /// Interaction logic for ExpendituresSideCards.xaml
    /// </summary>
    public partial class ExpendituresSideCards : UserControl
    {
        public ISeries[] ExpendituresSeries { get; set; }

        public ExpendituresSideCards()
        {
            InitializeComponent();

            ExpendituresSeries = [
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
