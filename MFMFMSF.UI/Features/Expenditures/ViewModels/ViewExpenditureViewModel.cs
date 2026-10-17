using MFMFMSF.Core.Interfaces;
using MFMFMSF.Core.Models.Expenditures;
using MFMFMSF.UI.Navigation;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MFMFMSF.UI.Features.Expenditures.ViewModels
{
    public class ViewExpenditureViewModel : INotifyPropertyChanged
    {
        private readonly INavigationService _navigationService;
        private readonly IExpenditureService _expenditureService;

        public Guid ExpenditureId { get; }

        private ExpenditureDetail? _expenditure;
        public ExpenditureDetail? Expenditure
        {
            get => _expenditure;
            private set
            {
                _expenditure = value;
                OnPropertyChanged();
            }
        }

        public ViewExpenditureViewModel(Guid expenditureId, INavigationService navigationService, IExpenditureService expenditureService)
        {
            _navigationService = navigationService;
            _expenditureService = expenditureService;
            ExpenditureId = expenditureId;

        }

        public async Task LoadAsync()
        {
            Expenditure = await _expenditureService.GetByIdAsync(ExpenditureId);
        }

        private void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
