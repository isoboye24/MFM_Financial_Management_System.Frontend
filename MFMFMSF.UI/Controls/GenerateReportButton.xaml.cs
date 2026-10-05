using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace MFMFMSF.UI.Controls
{
    public partial class GenerateReportButton : UserControl
    {
        public GenerateReportButton()
        {
            InitializeComponent();
        }


        // =====================================================
        // COMMAND
        // =====================================================

        public ICommand? Command
        {
            get => (ICommand?)GetValue(CommandProperty);
            set => SetValue(CommandProperty, value);
        }

        public static readonly DependencyProperty CommandProperty =
            DependencyProperty.Register(
                nameof(Command),
                typeof(ICommand),
                typeof(GenerateReportButton),
                new PropertyMetadata(null));


        // =====================================================
        // IS LOADING
        // =====================================================

        public bool IsLoading
        {
            get => (bool)GetValue(IsLoadingProperty);
            set => SetValue(IsLoadingProperty, value);
        }

        public static readonly DependencyProperty IsLoadingProperty =
            DependencyProperty.Register(
                nameof(IsLoading),
                typeof(bool),
                typeof(GenerateReportButton),
                new PropertyMetadata(false, OnIsLoadingChanged));


        private static void OnIsLoadingChanged(
            DependencyObject d,
            DependencyPropertyChangedEventArgs e)
        {
            if (d is GenerateReportButton button)
            {
                button.UpdateLoadingState();
            }
        }


        // =====================================================
        // UPDATE STATE
        // =====================================================

        private void UpdateLoadingState()
        {
            if (IsLoading)
            {
                NormalContent.Visibility = Visibility.Collapsed;
                LoadingContent.Visibility = Visibility.Visible;

                InnerButton.IsEnabled = false;

                StartGearAnimation();
            }
            else
            {
                StopGearAnimation();

                LoadingContent.Visibility = Visibility.Collapsed;
                NormalContent.Visibility = Visibility.Visible;

                InnerButton.IsEnabled = true;
            }
        }


        // =====================================================
        // START ANIMATION
        // =====================================================

        private void StartGearAnimation()
        {
            var animation = new DoubleAnimation
            {
                From = 0,
                To = 360,
                Duration = TimeSpan.FromSeconds(0.6),
                RepeatBehavior = RepeatBehavior.Forever
            };

            GearRotation.BeginAnimation(
                RotateTransform.AngleProperty,
                animation);
        }


        // =====================================================
        // STOP ANIMATION
        // =====================================================

        private void StopGearAnimation()
        {
            GearRotation.BeginAnimation(
                RotateTransform.AngleProperty,
                null);

            GearRotation.Angle = 0;
        }
    }
}