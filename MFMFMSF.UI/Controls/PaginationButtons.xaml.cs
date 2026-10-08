using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace MFMFMSF.UI.Controls
{
    public partial class PaginationButtons : UserControl
    {
        public const int DefaultPageSize = 10;

        public ObservableCollection<int> PageNumbers { get; } = new();


        public PaginationButtons()
        {
            InitializeComponent();

            UpdatePagination();
        }


        // ==========================================
        // CURRENT PAGE
        // ==========================================

        public static readonly DependencyProperty CurrentPageProperty =
            DependencyProperty.Register(
                nameof(CurrentPage),
                typeof(int),
                typeof(PaginationButtons),
                new PropertyMetadata(1, OnPaginationPropertyChanged));

        public int CurrentPage
        {
            get => (int)GetValue(CurrentPageProperty);
            set => SetValue(CurrentPageProperty, value);
        }


        // ==========================================
        // TOTAL ITEMS
        // ==========================================

        public static readonly DependencyProperty TotalItemsProperty =
            DependencyProperty.Register(
                nameof(TotalItems),
                typeof(int),
                typeof(PaginationButtons),
                new PropertyMetadata(0, OnPaginationPropertyChanged));

        public int TotalItems
        {
            get => (int)GetValue(TotalItemsProperty);
            set => SetValue(TotalItemsProperty, value);
        }


        // ==========================================
        // PAGE SIZE
        // ==========================================

        public int PageSize => DefaultPageSize;


        // ==========================================
        // TOTAL PAGES
        // ==========================================

        public int TotalPages
        {
            get
            {
                if (TotalItems <= 0)
                    return 1;

                return (int)Math.Ceiling(
                    (double)TotalItems / PageSize);
            }
        }


        // ==========================================
        // PREVIOUS
        // ==========================================

        public bool CanGoPrevious =>
            CurrentPage > 1;


        // ==========================================
        // NEXT
        // ==========================================

        public bool CanGoNext =>
            CurrentPage < TotalPages;


        // ==========================================
        // PAGE CHANGED EVENT
        // ==========================================

        public event EventHandler<PageChangedEventArgs>? PageChanged;


        // ==========================================
        // PROPERTY CHANGED
        // ==========================================

        private static void OnPaginationPropertyChanged(
            DependencyObject d,
            DependencyPropertyChangedEventArgs e)
        {
            var control = (PaginationButtons)d;

            control.UpdatePagination();
        }


        // ==========================================
        // UPDATE PAGINATION
        // ==========================================

        private void UpdatePagination()
        {
            if (!IsInitialized)
                return;

            if (CurrentPage < 1)
                CurrentPage = 1;

            if (CurrentPage > TotalPages)
                CurrentPage = TotalPages;

            PageNumbers.Clear();

            for (int i = 1; i <= TotalPages; i++)
            {
                PageNumbers.Add(i);
            }

            InvalidateVisual();
        }


        // ==========================================
        // PREVIOUS BUTTON
        // ==========================================

        private void PreviousButton_Click(
            object sender,
            RoutedEventArgs e)
        {
            if (!CanGoPrevious)
                return;

            CurrentPage--;

            RaisePageChanged();
        }


        // ==========================================
        // NEXT BUTTON
        // ==========================================

        private void NextButton_Click(
            object sender,
            RoutedEventArgs e)
        {
            if (!CanGoNext)
                return;

            CurrentPage++;

            RaisePageChanged();
        }


        // ==========================================
        // PAGE BUTTON
        // ==========================================

        private void PageButton_Click(
            object sender,
            RoutedEventArgs e)
        {
            if (sender is not Button button)
                return;

            if (button.Content is not int page)
                return;

            if (page == CurrentPage)
                return;

            CurrentPage = page;

            RaisePageChanged();
        }


        // ==========================================
        // RAISE EVENT
        // ==========================================

        private void RaisePageChanged()
        {
            PageChanged?.Invoke(
                this,
                new PageChangedEventArgs(CurrentPage, PageSize));
        }
    }


    public class PageChangedEventArgs : EventArgs
    {
        public int Page { get; }

        public int PageSize { get; }

        public PageChangedEventArgs(
            int page,
            int pageSize)
        {
            Page = page;
            PageSize = pageSize;
        }
    }
}