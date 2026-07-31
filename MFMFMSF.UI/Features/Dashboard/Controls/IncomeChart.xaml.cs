using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using MFMFMSF.UI.Features.Dashboard.ViewModels;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Windows.Media;

namespace MFMFMSF.UI.Features.Dashboard.Controls
{
    /// <summary>
    /// Interaction logic for IncomeChart.xaml
    /// </summary>
    public partial class IncomeChart : UserControl
    {
        public ObservableCollection<IncomeVM> IncomeItems { get; set; }
        public IEnumerable<ISeries> Series { get; set; }
        public decimal TotalIncome => IncomeItems.Sum(i => i.Amount);

        public IncomeChart()
        {
            InitializeComponent();

            IncomeItems = [
                            new() { Name="Offerings", Amount=28450, Percentage=50, Color=Brushes.MediumPurple },
                            new() { Name="Tithes", Amount=18620, Percentage=32.7, Color=Brushes.MediumSeaGreen },
                            new() { Name="Seeds", Amount=6870, Percentage=12.1, Color=Brushes.Orange },
                            new() { Name="Other Income", Amount=2960, Percentage=5.2, Color=Brushes.Pink }
                        ];

            Series = IncomeItems.Select(i => new PieSeries<double>
            {
                Values = new[] { (double)i.Amount }
            }).ToArray();

            DataContext = this;
        }
    }
}
