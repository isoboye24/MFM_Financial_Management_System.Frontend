using MFMFMSF.UI.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MFMFMSF.UI.Features.Offering.Controls
{
    /// <summary>
    /// Interaction logic for OfferingTopBar.xaml
    /// </summary>
    public partial class OfferingTopBar : UserControl
    {
        public event EventHandler<PeriodChangedEventArgs>? PeriodChanged;

        public OfferingTopBar()
        {
            InitializeComponent();
        }

        private void MonthYearPicker_PeriodChanged(object? sender, PeriodChangedEventArgs e)
        {
            PeriodChanged?.Invoke(this, e);
        }
    }
}
