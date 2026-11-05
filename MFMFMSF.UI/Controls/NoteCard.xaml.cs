using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace MFMFMSF.UI.Controls
{
    public partial class NoteCard : UserControl
    {
        public NoteCard()
        {
            InitializeComponent();
        }

        // NOTE TITLE
        public string NoteTitle
        {
            get => (string)GetValue(NoteTitleProperty);
            set => SetValue(NoteTitleProperty, value);
        }

        public static readonly DependencyProperty NoteTitleProperty =
            DependencyProperty.Register(
                nameof(NoteTitle),
                typeof(string),
                typeof(NoteCard),
                new PropertyMetadata(string.Empty));


        // NOTE CONTENT
        public string Note
        {
            get => (string)GetValue(NoteProperty);
            set => SetValue(NoteProperty, value);
        }

        public static readonly DependencyProperty NoteProperty =
            DependencyProperty.Register(
                nameof(Note),
                typeof(string),
                typeof(NoteCard),
                new PropertyMetadata(string.Empty));


        // CREATION TIME
        public DateTime? CreationTime
        {
            get => (DateTime?)GetValue(CreationTimeProperty);
            set => SetValue(CreationTimeProperty, value);
        }

        public static readonly DependencyProperty CreationTimeProperty =
            DependencyProperty.Register(
                nameof(CreationTime),
                typeof(DateTime?),
                typeof(NoteCard),
                new PropertyMetadata(null));

        // ===================================================== -->
        // ACTION BUTTONS -->
        // ===================================================== -->

        // View command
        public ICommand? ViewCommand
        {
            get => (ICommand?)GetValue(ViewCommandProperty);
            set => SetValue(ViewCommandProperty, value);
        }

        public static readonly DependencyProperty ViewCommandProperty =
            DependencyProperty.Register(
                nameof(ViewCommand),
                typeof(ICommand),
                typeof(NoteCard),
                new PropertyMetadata(null));

        // Edit command
        public ICommand? EditCommand
        {
            get => (ICommand?)GetValue(EditCommandProperty);
            set => SetValue(EditCommandProperty, value);
        }

        public static readonly DependencyProperty EditCommandProperty =
            DependencyProperty.Register(
                nameof(EditCommand),
                typeof(ICommand),
                typeof(NoteCard),
                new PropertyMetadata(null));

        // Delete command
        public ICommand? DeleteCommand
        {
            get => (ICommand?)GetValue(DeleteCommandProperty);
            set => SetValue(DeleteCommandProperty, value);
        }

        public static readonly DependencyProperty DeleteCommandProperty =
            DependencyProperty.Register(
                nameof(DeleteCommand),
                typeof(ICommand),
                typeof(NoteCard),
                new PropertyMetadata(null));
    }
}