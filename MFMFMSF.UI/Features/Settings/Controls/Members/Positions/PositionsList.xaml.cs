using MFMFMSF.Core.Interfaces;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Windows;
using System.Windows.Controls;

namespace MFMFMSF.UI.Features.Settings.Controls.Members.Positions
{
    /// <summary>
    /// Interaction logic for PositionsList.xaml
    /// </summary>
    public partial class PositionsList : UserControl
    {
        private IPositionService? _positionService;

        public ObservableCollection<PositionItem> Positions { get; } = new();

        public PositionsList()
        {
            InitializeComponent();

            PositionItemsControl.ItemsSource = Positions;
        }

        public void SetService(IPositionService service)
        {
            _positionService = service;
        }


        // ==========================================
        // LOAD POSITIONS
        // ==========================================

        public async Task LoadAsync()
        {
            if (_positionService == null)
                throw new InvalidOperationException(
                    "Position service has not been configured.");

            try
            {
                var positions = await _positionService.GetAllAsync();

                Positions.Clear();

                int number = 1;

                foreach (var position in positions)
                {
                    Positions.Add(
                        new PositionItem
                        {
                            Id = position.Id,
                            Number = number++,
                            Name = position.Name
                        });
                }
            }
            catch (HttpRequestException)
            {
                MessageBox.Show(
                    "Unable to connect to the server.",
                    "Connection Error",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.Message,
                    "Unable to Load Positions",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
        }
    }
}
