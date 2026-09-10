using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace MFMFMSF.UI.Features.Settings.Controls
{
    /// <summary>
    /// Interaction logic for SettingsPageTabs.xaml
    /// </summary>
    public partial class SettingsPageTabs : UserControl
    {
        public SettingsPageTabs()
        {
            InitializeComponent();

            // Set the initial selected tab
            UpdateSelectedTab(SelectedIndex);
        }


        // ==========================================
        // SELECTED TAB
        // ==========================================

        public int SelectedIndex
        {
            get => (int)GetValue(SelectedIndexProperty);
            set => SetValue(SelectedIndexProperty, value);
        }

        public static readonly DependencyProperty SelectedIndexProperty =
            DependencyProperty.Register(
                nameof(SelectedIndex),
                typeof(int),
                typeof(SettingsPageTabs),
                new PropertyMetadata(0, OnSelectedIndexChanged));


        private static void OnSelectedIndexChanged(
            DependencyObject d,
            DependencyPropertyChangedEventArgs e)
        {
            var control = (SettingsPageTabs)d;

            control.UpdateSelectedTab((int)e.NewValue);
        }


        // ==========================================
        // TAB CLICK
        // ==========================================

        private void Tab_Click(
            object sender,
            RoutedEventArgs e)
        {
            if (sender is Button button &&
                int.TryParse(button.Tag?.ToString(), out int index))
            {
                SelectedIndex = index;

                SettingsTabChanged?.Invoke(
                    this,
                    new SettingsTabChangedEventArgs(index));
            }
        }


        // ==========================================
        // UPDATE VISUAL STATE
        // ==========================================

        private void UpdateSelectedTab(int index)
        {
            if (SettingsActiveIndicator == null)
                return;

            if (index < 0 || index > 2)
                index = 0;

            Grid.SetColumn(SettingsActiveIndicator, index);

            if (SettingsActiveIndicator.Parent is not Grid parentGrid)
                return;

            foreach (var child in parentGrid.Children)
            {
                if (child is Button button &&
                    int.TryParse(button.Tag?.ToString(), out int buttonIndex))
                {
                    button.Foreground =
                        buttonIndex == index
                            ? new SolidColorBrush(Color.FromRgb(91, 76, 243))
                            : new SolidColorBrush(Color.FromRgb(48, 54, 83));
                }
            }
        }


        // ==========================================
        // TAB CHANGED EVENT
        // ==========================================

        public event EventHandler<SettingsTabChangedEventArgs>? SettingsTabChanged;
    }
}

