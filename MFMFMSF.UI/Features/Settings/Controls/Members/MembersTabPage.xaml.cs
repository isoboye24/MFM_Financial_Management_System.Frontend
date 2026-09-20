using MaterialDesignThemes.Wpf;
using MFMFMSF.Core.Interfaces;
using MFMFMSF.Core.Models.Positions;
using System.Net.Http;
using System.Windows;
using System.Windows.Controls;

namespace MFMFMSF.UI.Features.Settings.Controls.Members
{
    /// <summary>
    /// Interaction logic for MembersTabPage.xaml
    /// </summary>
    public partial class MembersTabPage : UserControl
    {
        private readonly IPositionService _positionService;


        private Guid? _editingPositionId;

        public MembersTabPage(IPositionService positionService)
        {
            InitializeComponent();

            _positionService = positionService;

            PositionsListControl.SetService(_positionService);

            Loaded += PositionsTabPage_Loaded;
        }

        // ==========================================
        // CREATE / UPDATE POSITION
        // ==========================================
        private async void PositionControl_ActionClicked(object? sender, EventArgs e)
        {           
            try
            {
                if (_editingPositionId == null)
                {
                    // CREATE
                    var request = new CreatePositionRequest
                    {
                        Name = PositionControl.PositionName.Trim()
                    };

                    await _positionService.CreateAsync(request);
                    MessageBox.Show(
                        "Position created successfully.",
                        "Position",
                        MessageBoxButton.OK,
                        MessageBoxImage.Information);
                }
                else
                {
                    //// UPDATE
                    //await _positionService.UpdateAsync(
                    //    _editingPositionId.Value,
                    //    positionName);

                    //MessageBox.Show(
                    //    "Position updated successfully.",
                    //    "Position",
                    //    MessageBoxButton.OK,
                    //    MessageBoxImage.Information);
                }


                // Reset form

                PositionControl.PositionName =
                    string.Empty;

                PositionControl.ButtonText =
                    "Create";

                PositionControl.ButtonIcon =
                    PackIconKind.Plus;

                _editingPositionId = null;

                // Refresh list

                //await MeetingCategoriesListControl.LoadAsync();
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
                    "Position",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
        }


        // ==========================================
        // LOAD POSITIONS
        // ==========================================
        private async void PositionsTabPage_Loaded(object sender, RoutedEventArgs e)
        {
            Loaded -= PositionsTabPage_Loaded;

            await PositionsListControl.LoadAsync();
        }
    }
}
