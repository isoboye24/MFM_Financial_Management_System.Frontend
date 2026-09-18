using MFMFMSF.UI.Controls;
using System.Windows.Controls;

namespace MFMFMSF.UI.Features.Seeds.Controls
{
    /// <summary>
    /// Interaction logic for SeedsTopBar.xaml
    /// </summary>
    public partial class SeedsTopBar : UserControl
    {
        public event EventHandler<PeriodChangedEventArgs>? PeriodChanged;

        public SeedsTopBar()
        {
            InitializeComponent();
        }

        private void MonthYearPicker_PeriodChanged(object? sender, PeriodChangedEventArgs e)
        {
            PeriodChanged?.Invoke(this, e);
        }
    }
}
