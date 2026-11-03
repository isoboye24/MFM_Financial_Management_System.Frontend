using System.Windows;
using System.Windows.Controls;

namespace MFMFMSF.UI.Features.WorkerNotes.Controls.Form
{
    public partial class WorkerNoteForm : UserControl
    {
        public WorkerNoteForm()
        {
            InitializeComponent();
        }

        public string NoteTitle
        {
            get => (string)GetValue(NoteTitleProperty);
            set => SetValue(NoteTitleProperty, value);
        }

        public static readonly DependencyProperty NoteTitleProperty =
            DependencyProperty.Register(
                nameof(NoteTitle),
                typeof(string),
                typeof(WorkerNoteForm),
                new FrameworkPropertyMetadata(
                    string.Empty,
                    FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public string Note
        {
            get => (string)GetValue(NoteProperty);
            set => SetValue(NoteProperty, value);
        }

        public static readonly DependencyProperty NoteProperty =
            DependencyProperty.Register(
                nameof(Note),
                typeof(string),
                typeof(WorkerNoteForm),
                new FrameworkPropertyMetadata(
                    string.Empty,
                    FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));
    }
}