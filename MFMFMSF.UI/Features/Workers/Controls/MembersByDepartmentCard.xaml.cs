using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using System.Windows.Controls;

namespace MFMFMSF.UI.Features.Workers.Controls
{
    /// <summary>
    /// Interaction logic for MembersByDepartmentCard.xaml
    /// </summary>
    public partial class MembersByDepartmentCard : UserControl
    {
        public ISeries[] MembersDepartmentSeries { get; set; }

        public MembersByDepartmentCard()
        {
            InitializeComponent();

            MembersDepartmentSeries = [
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
