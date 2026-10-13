using MFMFMSF.UI.Controls;
using System.Windows;
using System.Windows.Controls;

namespace MFMFMSF.UI.Features.Dashboard.Controls
{
    public partial class QuickActionCards : UserControl
    {
        public event RoutedEventHandler? AddMeetingClicked;
        public event RoutedEventHandler? AddExpenditureClicked;
        public event RoutedEventHandler? GenerateReportClicked;

        public QuickActionCards()
        {
            InitializeComponent();

            AddMeetingCard.Click += (sender, e) =>
                AddMeetingClicked?.Invoke(this, e);

            AddExpenditureCard.Click += (sender, e) =>
                AddExpenditureClicked?.Invoke(this, e);

            GenerateReportCard.Click += (sender, e) =>
                GenerateReportClicked?.Invoke(this, e);
        }
    }
}
