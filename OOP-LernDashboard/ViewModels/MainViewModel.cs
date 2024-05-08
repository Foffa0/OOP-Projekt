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
        public ICommand QuickNotesCommand { get; }
        public ICommand SettingsCommand { get; }
        public ICommand TimerCommand { get; }

        public ViewModelBase CurrentViewModel => _navigationStore.CurrentViewModel;


        public MainViewModel(
            NavigationStore navigationStore, 
            NavigationService dashboardNavigationService, 
            NavigationService calendarNavigationServie, 
            NavigationService quickNotesNavigationService, 
            NavigationService settingsNavigationService,
            NavigationService timerNavigationService) 
        {
            _navigationStore = navigationStore;

            _navigationStore.CurrentViewModelChanged += OnCurrentViewModelChanged;

            DashboardCommand = new NavigateCommand(dashboardNavigationService);
            CalendarCommand = new NavigateCommand(calendarNavigationServie);
            SettingsCommand = new NavigateCommand(settingsNavigationService);
            QuickNotesCommand = new NavigateCommand(quickNotesNavigationService);
            TimerCommand = new NavigateCommand(timerNavigationService);

        }

        private void OnCurrentViewModelChanged()
        {
            OnPropertyChanged(nameof(CurrentViewModel));
        }
    }
}
