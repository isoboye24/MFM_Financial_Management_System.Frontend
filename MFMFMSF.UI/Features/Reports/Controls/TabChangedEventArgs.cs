namespace MFMFMSF.UI.Features.Reports.Controls
{
    public class TabChangedEventArgs : EventArgs
    {
        public int SelectedIndex { get; }

        public TabChangedEventArgs(int selectedIndex)
        {
            SelectedIndex = selectedIndex;
        }
    }
}
