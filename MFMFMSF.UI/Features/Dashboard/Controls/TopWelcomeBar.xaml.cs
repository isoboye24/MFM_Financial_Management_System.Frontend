using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace MFMFMSF.UI.Features.Dashboard.Controls
{
    public partial class TopWelcomeBar : UserControl
    {
        private int _temporaryMonth;
        private int _temporaryYear;

        public TopWelcomeBar()
        {
            InitializeComponent();

            UpdateSelectedPeriodText();
        }

        // =====================================================
        // MONTH SELECTED ATTACHED PROPERTY
        // =====================================================

        public static readonly DependencyProperty IsMonthSelectedProperty =
            DependencyProperty.RegisterAttached(
                "IsMonthSelected",
                typeof(bool),
                typeof(TopWelcomeBar),
                new PropertyMetadata(false));

        public static void SetIsMonthSelected(
            DependencyObject element,
            bool value)
        {
            element.SetValue(IsMonthSelectedProperty, value);
        }

        public static bool GetIsMonthSelected(
            DependencyObject element)
        {
            return (bool)element.GetValue(IsMonthSelectedProperty);
        }


        // =====================================================
        // SELECTED MONTH
        // =====================================================

        public int SelectedMonth
        {
            get => (int)GetValue(SelectedMonthProperty);
            set => SetValue(SelectedMonthProperty, value);
        }

        public static readonly DependencyProperty SelectedMonthProperty =
            DependencyProperty.Register(
                nameof(SelectedMonth),
                typeof(int),
                typeof(TopWelcomeBar),
                new PropertyMetadata(
                    DateTime.Today.Month,
                    OnSelectedPeriodChanged));


        // =====================================================
        // SELECTED YEAR
        // =====================================================

        public int SelectedYear
        {
            get => (int)GetValue(SelectedYearProperty);
            set => SetValue(SelectedYearProperty, value);
        }

        public static readonly DependencyProperty SelectedYearProperty =
            DependencyProperty.Register(
                nameof(SelectedYear),
                typeof(int),
                typeof(TopWelcomeBar),
                new PropertyMetadata(
                    DateTime.Today.Year,
                    OnSelectedPeriodChanged));


        // =====================================================
        // PERIOD TEXT
        // =====================================================

        public string SelectedPeriodText
        {
            get => (string)GetValue(SelectedPeriodTextProperty);
            private set => SetValue(SelectedPeriodTextProperty, value);
        }

        public static readonly DependencyProperty SelectedPeriodTextProperty =
            DependencyProperty.Register(
                nameof(SelectedPeriodText),
                typeof(string),
                typeof(TopWelcomeBar),
                new PropertyMetadata(string.Empty));


        // =====================================================
        // PERIOD CHANGED
        // =====================================================

        public event EventHandler<PeriodChangedEventArgs>? PeriodChanged;


        private static void OnSelectedPeriodChanged(
            DependencyObject d,
            DependencyPropertyChangedEventArgs e)
        {
            var control = (TopWelcomeBar)d;

            control.UpdateSelectedPeriodText();
        }


        private void UpdateSelectedPeriodText()
        {
            if (SelectedMonth < 1 || SelectedMonth > 12)
                return;

            if (SelectedYear < 1)
                return;

            var firstDay = new DateTime(
                SelectedYear,
                SelectedMonth,
                1);

            var lastDay = DateTime.DaysInMonth(
                SelectedYear,
                SelectedMonth);

            SelectedPeriodText =
                $"{firstDay:MMMM} 1 – " +
                $"{firstDay:MMMM} {lastDay}, " +
                $"{SelectedYear}";
        }


        // =====================================================
        // OPEN POPUP
        // =====================================================

        private void DateButton_Click(
            object sender,
            RoutedEventArgs e)
        {
            _temporaryMonth = SelectedMonth;
            _temporaryYear = SelectedYear;

            YearText.Text = _temporaryYear.ToString();

            HighlightSelectedMonth();

            PeriodPopup.IsOpen = true;
        }


        private void HighlightSelectedMonth()
        {
            foreach (var child in MonthGrid.Children)
            {
                if (child is Button button &&
                    int.TryParse(button.Tag?.ToString(), out int month))
                {
                    SetIsMonthSelected(
                        button,
                        month == _temporaryMonth);
                }
            }
        }


        // =====================================================
        // YEAR
        // =====================================================

        private void PreviousYear_Click(
            object sender,
            RoutedEventArgs e)
        {
            _temporaryYear--;

            YearText.Text = _temporaryYear.ToString();
        }


        private void NextYear_Click(
            object sender,
            RoutedEventArgs e)
        {
            _temporaryYear++;

            YearText.Text = _temporaryYear.ToString();
        }


        // =====================================================
        // MONTH
        // =====================================================

        private void Month_Click(
            object sender,
            RoutedEventArgs e)
        {
            if (sender is Button button &&
                int.TryParse(button.Tag?.ToString(), out int month))
            {
                _temporaryMonth = month;

                HighlightSelectedMonth();
            }

        }


        // =====================================================
        // CANCEL
        // =====================================================

        private void Cancel_Click(
            object sender,
            RoutedEventArgs e)
        {
            PeriodPopup.IsOpen = false;
        }


        // =====================================================
        // APPLY
        // =====================================================

        private void Apply_Click(
            object sender,
            RoutedEventArgs e)
        {
            SelectedMonth = _temporaryMonth;
            SelectedYear = _temporaryYear;

            PeriodPopup.IsOpen = false;

            PeriodChanged?.Invoke(
                this,
                new PeriodChangedEventArgs(
                    SelectedMonth,
                    SelectedYear));
        }
    }


    // =========================================================
    // EVENT ARGUMENTS
    // =========================================================

    public class PeriodChangedEventArgs : EventArgs
    {
        public int Month { get; }
        public int Year { get; }

        public PeriodChangedEventArgs(
            int month,
            int year)
        {
            Month = month;
            Year = year;
        }
    }
}