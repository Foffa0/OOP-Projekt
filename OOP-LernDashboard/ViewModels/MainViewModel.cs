using OOP_LernDashboard.Commands;
using OOP_LernDashboard.Services;
using OOP_LernDashboard.Stores;
using System.Diagnostics;
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
        public ICommand ShortcutsCommand { get; }

        public bool DashboardViewActive { get; set; }
        public bool CalendarViewActive { get; set; }
        public bool QuickNotesViewActive { get; set; }
        public bool TimerViewActive {  get; set; }
        public bool ShortcutsViewActive { get; set; }

        public ViewModelBase CurrentViewModel => _navigationStore.CurrentViewModel;


        public MainViewModel(
            NavigationStore navigationStore, 
            NavigationService dashboardNavigationService, 
            NavigationService calendarNavigationServie, 
            NavigationService quickNotesNavigationService, 
            NavigationService settingsNavigationService,
            NavigationService timerNavigationService,
            NavigationService shortcutsNavigationService) 
        {
            _navigationStore = navigationStore;

            _navigationStore.CurrentViewModelChanged += OnCurrentViewModelChanged;

            DashboardCommand = new NavigateCommand(dashboardNavigationService);
            CalendarCommand = new NavigateCommand(calendarNavigationServie);
            SettingsCommand = new NavigateCommand(settingsNavigationService);
            QuickNotesCommand = new NavigateCommand(quickNotesNavigationService);
            TimerCommand = new NavigateCommand(timerNavigationService);
            ShortcutsCommand = new NavigateCommand(shortcutsNavigationService);

            DashboardViewActive = true;
            ShortcutsViewActive = QuickNotesViewActive = CalendarViewActive = TimerViewActive = false;
        }

        private void OnCurrentViewModelChanged()
        {
            DashboardViewActive = ShortcutsViewActive = QuickNotesViewActive = CalendarViewActive = TimerViewActive = false;
            switch (CurrentViewModel.ToString())
            {
                case "OOP_LernDashboard.ViewModels.QuickNotesViewModel":
                    QuickNotesViewActive = true;
                    OnPropertyChanged(nameof(QuickNotesViewActive));
                    break;
                case "OOP_LernDashboard.ViewModels.ShortcutsViewModel":
                    ShortcutsViewActive = true;
                    OnPropertyChanged(nameof(ShortcutsViewActive));
                    break;
                case "OOP_LernDashboard.ViewModels.CalendarViewModel":
                    CalendarViewActive = true;
                    OnPropertyChanged(nameof(CalendarViewActive));
                    break;
                case "OOP_LernDashboard.ViewModels.TimerViewModel":
                    TimerViewActive = true;
                    OnPropertyChanged(nameof(TimerViewActive));
                    break;
            }

            OnPropertyChanged(nameof(CurrentViewModel));
        }
    }
}
