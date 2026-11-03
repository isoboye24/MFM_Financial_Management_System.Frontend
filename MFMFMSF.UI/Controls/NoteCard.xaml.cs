using System.Windows;
using System.Windows.Controls;

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
    }
}