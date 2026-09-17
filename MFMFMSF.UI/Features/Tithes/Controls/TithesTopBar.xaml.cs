using MFMFMSF.UI.Controls;
using System.Windows.Controls;

namespace MFMFMSF.UI.Features.Tithes.Controls
{
    /// <summary>
    /// Interaction logic for TithesTopBar.xaml
    /// </summary>
    public partial class TithesTopBar : UserControl
    {
        public event EventHandler<PeriodChangedEventArgs>? PeriodChanged;

        public TithesTopBar()
        {
            InitializeComponent();
        }

        private void MonthYearPicker_PeriodChanged(object? sender, PeriodChangedEventArgs e)
        {
            PeriodChanged?.Invoke(this, e);
        }
    }
}
