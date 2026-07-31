using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace MFMFMSF.UI.Features.Dashboard.ViewModels
{
    public class IncomeVM
    {
        public string Name { get; set; }

        public decimal Amount { get; set; }

        public double Percentage { get; set; }

        public Brush Color { get; set; }
    }
}
