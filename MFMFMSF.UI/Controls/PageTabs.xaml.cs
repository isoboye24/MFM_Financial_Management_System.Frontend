using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace MFMFMSF.UI.Controls
{
    public partial class PageTabs : UserControl
    {
        public PageTabs()
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
                typeof(PageTabs),
                new PropertyMetadata(0, OnSelectedIndexChanged));


        private static void OnSelectedIndexChanged(
            DependencyObject d,
            DependencyPropertyChangedEventArgs e)
        {
            var control = (PageTabs)d;

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

                TabChanged?.Invoke(
                    this,
                    new TabChangedEventArgs(index));
            }
        }


        // ==========================================
        // UPDATE VISUAL STATE
        // ==========================================

        private void UpdateSelectedTab(int index)
        {
            if (ActiveIndicator == null)
                return;

            if (index < 0 || index > 5)
                index = 0;

            Grid.SetColumn(ActiveIndicator, index);

            if (ActiveIndicator.Parent is not Grid parentGrid)
                return;

            foreach (var child in parentGrid.Children)
            {
                if (child is Button button &&
                    int.TryParse(button.Tag?.ToString(), out int buttonIndex))
                {
                    button.Foreground =
                        buttonIndex == index
                            ? new SolidColorBrush(
                                Color.FromRgb(91, 76, 243))
                            : new SolidColorBrush(
                                Color.FromRgb(48, 54, 83));
                }
            }
        }


        // ==========================================
        // TAB CHANGED EVENT
        // ==========================================

        public event EventHandler<TabChangedEventArgs>? TabChanged;
    }


    // ==============================================
    // EVENT ARGUMENTS
    // ==============================================

    public class TabChangedEventArgs : EventArgs
    {
        public int SelectedIndex { get; }

        public TabChangedEventArgs(int selectedIndex)
        {
            SelectedIndex = selectedIndex;
        }
    }
}