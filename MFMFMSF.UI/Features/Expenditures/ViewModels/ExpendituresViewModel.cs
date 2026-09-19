using MFMFMSF.Core.Interfaces;
using MFMFMSF.UI.Commands;
using MFMFMSF.UI.Features.Expenditures.Views;
using MFMFMSF.UI.Navigation;
using System.Windows.Input;

namespace MFMFMSF.UI.Features.Expenditures.ViewModels
{
    public class ExpendituresViewModel
    {
        private readonly INavigationService _navigationService;
        private readonly IExpenditureService _expenditureService;

        public ICommand AddExpenditureCommand { get; }


        public INavigationService NavigationService => _navigationService;
        public IExpenditureService ExpenditureService => _expenditureService;


        public ExpendituresViewModel(INavigationService navigationService, IExpenditureService expenditureService)
        {
            _navigationService = navigationService;
            _expenditureService = expenditureService;

            AddExpenditureCommand = new RelayCommand(_ => AddExpenditure());

            //SelectedMonth = DateTime.Now.Month;
            //SelectedYear = DateTime.Now.Year;
        }

        private void AddExpenditure()
        {
            _navigationService.Navigate(new CreateExpenditure(_navigationService, _expenditureService));
        }
    }
}
