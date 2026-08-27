using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using System;
using System.Collections.Generic;
using System.Windows.Controls;

namespace MFMFMSF.UI.Features.OtherIncome.Controls
{
    /// <summary>
    /// Interaction logic for OtherIncomeSideCards.xaml
    /// </summary>
    public partial class OtherIncomeSideCards : UserControl
    {
        public ISeries[] OtherIncomeSeries { get; set; }

        public OtherIncomeSideCards()
        {
            InitializeComponent();

            OtherIncomeSeries = [
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
