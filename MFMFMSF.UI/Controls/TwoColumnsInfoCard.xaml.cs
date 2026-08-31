using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace MFMFMSF.UI.Controls
{
    public partial class TwoColumnsInfoCard : UserControl
    {
        public TwoColumnsInfoCard()
        {
            InitializeComponent();
        }


        // =====================================================
        // CARD TITLE
        // =====================================================

        public string CardTitle
        {
            get => (string)GetValue(CardTitleProperty);
            set => SetValue(CardTitleProperty, value);
        }

        public static readonly DependencyProperty CardTitleProperty =
            DependencyProperty.Register(
                nameof(CardTitle),
                typeof(string),
                typeof(TwoColumnsInfoCard),
                new PropertyMetadata(string.Empty));


        // =====================================================
        // LABEL 1
        // =====================================================

        public string Label1
        {
            get => (string)GetValue(Label1Property);
            set => SetValue(Label1Property, value);
        }

        public static readonly DependencyProperty Label1Property =
            DependencyProperty.Register(
                nameof(Label1),
                typeof(string),
                typeof(TwoColumnsInfoCard),
                new PropertyMetadata(string.Empty));


        // =====================================================
        // VALUE 1
        // =====================================================

        public string Value1
        {
            get => (string)GetValue(Value1Property);
            set => SetValue(Value1Property, value);
        }

        public static readonly DependencyProperty Value1Property =
            DependencyProperty.Register(
                nameof(Value1),
                typeof(string),
                typeof(TwoColumnsInfoCard),
                new PropertyMetadata(string.Empty));


        // =====================================================
        // VALUE 1 BACKGROUND
        // =====================================================

        public Brush Value1Background
        {
            get => (Brush)GetValue(Value1BackgroundProperty);
            set => SetValue(Value1BackgroundProperty, value);
        }

        public static readonly DependencyProperty Value1BackgroundProperty =
            DependencyProperty.Register(
                nameof(Value1Background),
                typeof(Brush),
                typeof(TwoColumnsInfoCard),
                new PropertyMetadata(Brushes.Transparent));


        // =====================================================
        // VALUE 1 FOREGROUND
        // =====================================================

        public Brush Value1Foreground
        {
            get => (Brush)GetValue(Value1ForegroundProperty);
            set => SetValue(Value1ForegroundProperty, value);
        }

        public static readonly DependencyProperty Value1ForegroundProperty =
            DependencyProperty.Register(
                nameof(Value1Foreground),
                typeof(Brush),
                typeof(TwoColumnsInfoCard),
                new PropertyMetadata(Brushes.Black));


        // =====================================================
        // LABEL 2
        // =====================================================

        public string Label2
        {
            get => (string)GetValue(Label2Property);
            set => SetValue(Label2Property, value);
        }

        public static readonly DependencyProperty Label2Property =
            DependencyProperty.Register(
                nameof(Label2),
                typeof(string),
                typeof(TwoColumnsInfoCard),
                new PropertyMetadata(string.Empty));


        // =====================================================
        // VALUE 2
        // =====================================================

        public string Value2
        {
            get => (string)GetValue(Value2Property);
            set => SetValue(Value2Property, value);
        }

        public static readonly DependencyProperty Value2Property =
            DependencyProperty.Register(
                nameof(Value2),
                typeof(string),
                typeof(TwoColumnsInfoCard),
                new PropertyMetadata(string.Empty));


        public Brush Value2Background
        {
            get => (Brush)GetValue(Value2BackgroundProperty);
            set => SetValue(Value2BackgroundProperty, value);
        }

        public static readonly DependencyProperty Value2BackgroundProperty =
            DependencyProperty.Register(
                nameof(Value2Background),
                typeof(Brush),
                typeof(TwoColumnsInfoCard),
                new PropertyMetadata(Brushes.Transparent));


        public Brush Value2Foreground
        {
            get => (Brush)GetValue(Value2ForegroundProperty);
            set => SetValue(Value2ForegroundProperty, value);
        }

        public static readonly DependencyProperty Value2ForegroundProperty =
            DependencyProperty.Register(
                nameof(Value2Foreground),
                typeof(Brush),
                typeof(TwoColumnsInfoCard),
                new PropertyMetadata(Brushes.Black));


        // =====================================================
        // LABEL 3
        // =====================================================

        public string Label3
        {
            get => (string)GetValue(Label3Property);
            set => SetValue(Label3Property, value);
        }

        public static readonly DependencyProperty Label3Property =
            DependencyProperty.Register(
                nameof(Label3),
                typeof(string),
                typeof(TwoColumnsInfoCard),
                new PropertyMetadata(string.Empty));


        // =====================================================
        // VALUE 3
        // =====================================================

        public string Value3
        {
            get => (string)GetValue(Value3Property);
            set => SetValue(Value3Property, value);
        }

        public static readonly DependencyProperty Value3Property =
            DependencyProperty.Register(
                nameof(Value3),
                typeof(string),
                typeof(TwoColumnsInfoCard),
                new PropertyMetadata(string.Empty));


        public Brush Value3Background
        {
            get => (Brush)GetValue(Value3BackgroundProperty);
            set => SetValue(Value3BackgroundProperty, value);
        }

        public static readonly DependencyProperty Value3BackgroundProperty =
            DependencyProperty.Register(
                nameof(Value3Background),
                typeof(Brush),
                typeof(TwoColumnsInfoCard),
                new PropertyMetadata(Brushes.Transparent));


        public Brush Value3Foreground
        {
            get => (Brush)GetValue(Value3ForegroundProperty);
            set => SetValue(Value3ForegroundProperty, value);
        }

        public static readonly DependencyProperty Value3ForegroundProperty =
            DependencyProperty.Register(
                nameof(Value3Foreground),
                typeof(Brush),
                typeof(TwoColumnsInfoCard),
                new PropertyMetadata(Brushes.Black));


        // =====================================================
        // LABEL 4
        // =====================================================

        public string Label4
        {
            get => (string)GetValue(Label4Property);
            set => SetValue(Label4Property, value);
        }

        public static readonly DependencyProperty Label4Property =
            DependencyProperty.Register(
                nameof(Label4),
                typeof(string),
                typeof(TwoColumnsInfoCard),
                new PropertyMetadata(string.Empty));


        // =====================================================
        // VALUE 4
        // =====================================================

        public string Value4
        {
            get => (string)GetValue(Value4Property);
            set => SetValue(Value4Property, value);
        }

        public static readonly DependencyProperty Value4Property =
            DependencyProperty.Register(
                nameof(Value4),
                typeof(string),
                typeof(TwoColumnsInfoCard),
                new PropertyMetadata(string.Empty));


        public Brush Value4Background
        {
            get => (Brush)GetValue(Value4BackgroundProperty);
            set => SetValue(Value4BackgroundProperty, value);
        }

        public static readonly DependencyProperty Value4BackgroundProperty =
            DependencyProperty.Register(
                nameof(Value4Background),
                typeof(Brush),
                typeof(TwoColumnsInfoCard),
                new PropertyMetadata(Brushes.Transparent));


        public Brush Value4Foreground
        {
            get => (Brush)GetValue(Value4ForegroundProperty);
            set => SetValue(Value4ForegroundProperty, value);
        }

        public static readonly DependencyProperty Value4ForegroundProperty =
            DependencyProperty.Register(
                nameof(Value4Foreground),
                typeof(Brush),
                typeof(TwoColumnsInfoCard),
                new PropertyMetadata(Brushes.Black));
    }
}