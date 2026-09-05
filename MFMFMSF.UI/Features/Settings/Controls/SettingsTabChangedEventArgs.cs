namespace MFMFMSF.UI.Features.Settings.Controls
{
   public class SettingsTabChangedEventArgs
    {
        public int SelectedIndex { get; }

        public SettingsTabChangedEventArgs(int selectedIndex)
        {
            SelectedIndex = selectedIndex;
        }
    }
}
