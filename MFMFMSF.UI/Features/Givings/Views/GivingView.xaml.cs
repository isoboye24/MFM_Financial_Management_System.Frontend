using MFMFMSF.Core.Models.Givings;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace MFMFMSF.UI.Features.Givings.Views
{
    /// <summary>
    /// Interaction logic for GivingView.xaml
    /// </summary>
    public partial class GivingView : UserControl
    {
        public GivingView()
        {
            InitializeComponent();
        }

        public ObservableCollection<GivingListItem> Givings
        {
            get => (ObservableCollection<GivingListItem>)
                GetValue(GivingsProperty);

            set => SetValue(GivingsProperty, value);
        }


        public static readonly DependencyProperty GivingsProperty =
            DependencyProperty.Register(
                nameof(Givings),
                typeof(ObservableCollection<GivingListItem>),
                typeof(GivingView),
                new PropertyMetadata(null));
    }
}
