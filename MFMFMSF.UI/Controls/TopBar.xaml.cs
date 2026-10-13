using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace MFMFMSF.UI.Controls
{
    public partial class TopBar : UserControl
    {
        public event EventHandler? MinimizeRequested;
        public event EventHandler? MaximizeRequested;
        public event EventHandler? CloseRequested;

        private Window? _window;

        public TopBar()
        {
            InitializeComponent();

            Loaded += TopBar_Loaded;
            Unloaded += TopBar_Unloaded;
        }

        private void TopBar_Loaded(object sender, RoutedEventArgs e)
        {
            var window = Window.GetWindow(this);

            if (window == null || ReferenceEquals(window, _window))
                return;

            if (_window != null)
                _window.StateChanged -= Window_StateChanged;

            _window = window;
            _window.StateChanged += Window_StateChanged;

            UpdateMaximizeButton();
        }

        private void TopBar_Unloaded(object sender, RoutedEventArgs e)
        {
            if (_window != null)
            {
                _window.StateChanged -= Window_StateChanged;
                _window = null;
            }
        }

        private void Window_StateChanged(object? sender, EventArgs e)
        {
            UpdateMaximizeButton();
        }

        private void UpdateMaximizeButton()
        {
            if (_window == null)
                return;

            if (_window.WindowState == WindowState.Maximized)
            {
                MaximizeIcon.Text = "\uE923";
                MaximizeButton.ToolTip = "Restore";
            }
            else
            {
                MaximizeIcon.Text = "\uE922";
                MaximizeButton.ToolTip = "Maximize";
            }
        }

        private void MinimizeButton_Click(
            object sender, RoutedEventArgs e)
        {
            MinimizeRequested?.Invoke(this, EventArgs.Empty);
        }

        private void MaximizeButton_Click(
            object sender, RoutedEventArgs e)
        {
            MaximizeRequested?.Invoke(this, EventArgs.Empty);
        }

        private void CloseButton_Click(
            object sender, RoutedEventArgs e)
        {
            CloseRequested?.Invoke(this, EventArgs.Empty);
        }

        private void TitleBar_MouseLeftButtonDown(
            object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton != MouseButton.Left)
                return;

            var window = Window.GetWindow(this);

            if (window == null)
                return;

            if (e.ClickCount == 2)
            {
                MaximizeRequested?.Invoke(this, EventArgs.Empty);
                return;
            }

            if (e.ButtonState == MouseButtonState.Pressed)
                window.DragMove();
        }
    }
}