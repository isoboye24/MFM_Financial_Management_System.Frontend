using MFMFMSF.UI.Features.Settings.Controls;
using MFMFMSF.UI.Features.Settings.Controls.Categories;
using MFMFMSF.UI.Features.Settings.Controls.DeletedData;
using MFMFMSF.UI.Features.Settings.Controls.General;
using MFMFMSF.UI.Features.Settings.Controls.Members;
using System.Windows.Controls;

namespace MFMFMSF.UI.Features.Settings.Views
{
    public partial class SettingsView : UserControl
    {
        public SettingsView()
        {
            InitializeComponent();

            SettingsContent.Content = new GeneralSettingsTabPage();
        }


        private void SettingsPageTabs_TabChanged(object? sender, SettingsTabChangedEventArgs e)
        {
            switch (e.SelectedIndex)
            {
                case 0:
                    SettingsContent.Content = new GeneralSettingsTabPage();
                    break;

                case 1:
                    SettingsContent.Content = new CategoriesTabPage(App.MeetingCategoryService, App.GivingCategoryService);
                    break;
                
                case 2:
                    SettingsContent.Content = new MembersTabPage(App.PositionService);
                    break;
               
                case 3:
                    SettingsContent.Content = new DeletedDataTabPage();
                    break;
            }
        }
    }
}