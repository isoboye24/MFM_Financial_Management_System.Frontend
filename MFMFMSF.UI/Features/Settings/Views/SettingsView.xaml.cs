using MFMFMSF.UI.Features.Settings.Controls;
using MFMFMSF.UI.Features.Settings.Controls.General;
using MFMFMSF.UI.Features.Settings.Controls.MeetingCategories;
using System.Windows.Controls;

namespace MFMFMSF.UI.Features.Settings.Views
{
    /// <summary>
    /// Interaction logic for SettingsView.xaml
    /// </summary>
    public partial class SettingsView : UserControl
    {
        public SettingsView()
        {
            InitializeComponent();

            // Show the first report when the page opens
            SettingsContent.Content = new GeneralSettingsTabPage();
        }

        private void SettingsPageTabs_TabChanged(
            object? sender,
            SettingsTabChangedEventArgs e)
        {
            switch (e.SelectedIndex)
            {
                case 0:
                    SettingsContent.Content = new GeneralSettingsTabPage();
                    break;

                case 1:
                    SettingsContent.Content = new MeetingCategoriesTabPage();
                    break;

            }
        }
    }
}
