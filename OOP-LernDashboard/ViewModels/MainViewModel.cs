using OOP_LernDashboard.Commands;
using OOP_LernDashboard.Services;
using OOP_LernDashboard.Stores;
using System.Windows.Input;

namespace OOP_LernDashboard.ViewModels
{
    internal class MainViewModel : ViewModelBase
    {
        private readonly NavigationStore _navigationStore;

        public ICommand DashboardCommand { get; }
        public ICommand CalendarCommand { get; }
        public ICommand SettingsCommand { get; }

        public ViewModelBase CurrentViewModel => _navigationStore.CurrentViewModel;


        public MainViewModel(NavigationStore navigationStore, NavigationService dashboardNavigationService, NavigationService calendarNavigationServie, NavigationService settingsNavigationService) 
        {
            _navigationStore = navigationStore;

            _navigationStore.CurrentViewModelChanged += OnCurrentViewModelChanged;

            DashboardCommand = new NavigateCommand(dashboardNavigationService);
            CalendarCommand = new NavigateCommand(calendarNavigationServie);
            SettingsCommand = new NavigateCommand(settingsNavigationService);
        }

        private void OnCurrentViewModelChanged()
        {
            OnPropertyChanged(nameof(CurrentViewModel));
        }
    }
}
