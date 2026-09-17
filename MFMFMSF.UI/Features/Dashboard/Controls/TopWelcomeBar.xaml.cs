using MFMFMSF.UI.Controls;
using System.Windows.Controls;

namespace MFMFMSF.UI.Features.Dashboard.Controls
{
    public partial class TopWelcomeBar : UserControl
    {
        public event EventHandler<PeriodChangedEventArgs>? PeriodChanged;

        public TopWelcomeBar()
        {
            InitializeComponent();
        }

        private void MonthYearPicker_PeriodChanged(object? sender, PeriodChangedEventArgs e)
        {
            PeriodChanged?.Invoke(this, e);
        }
    }
}