using MFMFMSF.UI.Controls;
using System.Windows.Controls;

namespace MFMFMSF.UI.Features.OtherIncome.Controls
{
    /// <summary>
    /// Interaction logic for OtherIncomeTopBar.xaml
    /// </summary>
    public partial class OtherIncomeTopBar : UserControl
    {
        public event EventHandler<PeriodChangedEventArgs>? PeriodChanged;
        public OtherIncomeTopBar()
        {
            InitializeComponent();
        }

        private void MonthYearPicker_PeriodChanged(object? sender, PeriodChangedEventArgs e)
        {
            PeriodChanged?.Invoke(this, e);
        }
    }
}
