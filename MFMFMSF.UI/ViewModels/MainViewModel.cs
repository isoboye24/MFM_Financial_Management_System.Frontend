using MFMFMSF.Core.Interfaces;
using MFMFMSF.UI.Features.Dashboard.Controls;
using MFMFMSF.UI.Features.Dashboard.Views;
using MFMFMSF.UI.Navigation;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Controls;

namespace MFMFMSF.UI.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private readonly INavigationService _navigation;
        private readonly IMeetingCategoryService _meetingCategoryService;
        private readonly IMeetingService _meetingService;
        private readonly IGivingCategoryService _givingCategoryService;
        private readonly IGivingService _givingService;
        private readonly IExpenditureService _expenditureService;
        private readonly IReportService _reportService;

        private UserControl _currentView;

        public UserControl CurrentView
        {
            get => _currentView;
            private set
            {
                _currentView = value;
                OnPropertyChanged();
            }
        }


        public MainViewModel(INavigationService navigationService, IGivingService givingService, IExpenditureService expenditureService, IncomeChart incomeChart, 
            IncomeVsExpenditure incomeVsExpenditure, IMeetingCategoryService meetingCategoryService, IMeetingService meetingService, 
            IGivingCategoryService givingCategoryService, IReportService reportService)
        {
            _navigation = navigationService;
            _meetingCategoryService = meetingCategoryService;
            _meetingService = meetingService;
            _givingCategoryService = givingCategoryService;
            _givingService = givingService;
            _expenditureService = expenditureService;
            _reportService = reportService;

            _navigation.PageChanged += OnPageChanged;

            // Initial page
            CurrentView = new DashboardView(_navigation, _meetingCategoryService, _meetingService, _givingCategoryService, _givingService, _expenditureService, incomeChart, 
                incomeVsExpenditure, _reportService);
        }


        private void OnPageChanged(UserControl page)
        {
            CurrentView = page;
        }


        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged(
            [CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(
                this,
                new PropertyChangedEventArgs(propertyName));
        }
    }
}