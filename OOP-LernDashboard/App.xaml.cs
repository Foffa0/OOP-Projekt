using Microsoft.EntityFrameworkCore;
using OOP_LernDashboard.DbContexts;
using OOP_LernDashboard.Models;
using OOP_LernDashboard.Services;
using OOP_LernDashboard.Services.DataCreators;
using OOP_LernDashboard.Services.DataProviders;
using OOP_LernDashboard.Stores;
using OOP_LernDashboard.ViewModels;
using System.Windows;

namespace OOP_LernDashboard
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private const string CONNECTION_STRING = "Data Source=dashboard.db";

        private readonly NavigationStore _navigationStore;
        private readonly DashboardStore _dashboardStore;
        private readonly Dashboard _dashboard;
        private readonly DashboardDbContextFactory _dashboardDbContextFactory;

        public App()
        {
            _dashboardDbContextFactory = new DashboardDbContextFactory(CONNECTION_STRING);
            IDataCreator<ToDo> _toDoCreator = new DatabaseToDoCreator(_dashboardDbContextFactory);
            IDataProvider<ToDo> _toDoProvider = new DatabaseToDoProvider(_dashboardDbContextFactory);
            IDataCreator<Shortcut> _shortcutCreator = new DatabaseShortcutCreator(_dashboardDbContextFactory);
            IDataProvider<Shortcut> _shortcutProvider = new DatabaseShortcutProvider(_dashboardDbContextFactory);
            IDataCreator<Countdown> _countdownCreator = new DatabaseCountdownCreator(_dashboardDbContextFactory);
            IDataProvider<Countdown> _countdownPriovider = new DatabaseCountdownProvider(_dashboardDbContextFactory);
            IDataCreator<string> _calendarIdCreator = new DatabaseCalendarIdCreator(_dashboardDbContextFactory);
            IDataProvider<string> _calendarIdPriovider = new DatabaseCalendarIdProvider(_dashboardDbContextFactory);
            IDataCreator<QuickNote> _quickNoteCreator = new DatabaseQuickNoteCreator(_dashboardDbContextFactory);
            IDataProvider<QuickNote> _quickNoteProvider = new DatabaseQuickNoteProvider(_dashboardDbContextFactory);
            IDataCreator<Models.Timer> _timerCreator = new DatabaseTimerCreator(_dashboardDbContextFactory);
            IDataProvider<Models.Timer> _timerProvider = new DatabaseTimerProvider(_dashboardDbContextFactory);


            _dashboardStore = new DashboardStore(
                _toDoCreator, _toDoProvider,
                _shortcutCreator, _shortcutProvider,
                _countdownCreator, _countdownPriovider,
                _calendarIdCreator, _calendarIdPriovider,
                _quickNoteCreator, _quickNoteProvider,
                _timerCreator, _timerProvider
                );
            _navigationStore = new NavigationStore();
            _dashboard = new Dashboard(_dashboardStore);
        }
        protected override void OnStartup(StartupEventArgs e)
        {
            // Set the current directory to the directory of the executable
            // Necessary when running the application on Windows startup
            Environment.CurrentDirectory = System.IO.Path.GetDirectoryName(Environment.ProcessPath) ?? AppDomain.CurrentDomain.BaseDirectory;

            using (DashboardDbContext dbContext = _dashboardDbContextFactory.CreateDbContext())
            {
                dbContext.Database.Migrate();
            }

            _navigationStore.CurrentViewModel = CreateDashboardViewModel();

            MainWindow = new MainWindow()
            {
                DataContext = new MainViewModel(
                    _navigationStore,
                    new NavigationService(_navigationStore, CreateDashboardViewModel),
                    new NavigationService(_navigationStore, CreateCalendarViewModel),
                    new NavigationService(_navigationStore, CreateQuickNotesViewModel),
                    new NavigationService(_navigationStore, CreateSettingsViewModel),
                    new NavigationService(_navigationStore, CreateTimerCollectionViewModel),
                    new NavigationService(_navigationStore, CreateShortcutsViewModel),
                    new NavigationService(_navigationStore, CreateCountdownViewModel),
                    new NavigationService(_navigationStore, CreateToDosViewModel)
                    )
            };
            _dashboardStore.LoadAccentColor();

            // Check if the application was started with the -minimized argument
            if (e.Args.Contains("-minimized"))
            {
                MainWindow.WindowState = WindowState.Minimized;
            }

            MainWindow.Show();

            _dashboard.CheckForNotifications();

            base.OnStartup(e);
        }

        protected override void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);
        }

        private DemoViewModel CreateDemoViewModel()
        {
            return DemoViewModel.LoadViewModel(new NavigationService(_navigationStore, CreateDemo2ViewModel));
        }

        private Demo2ViewModel CreateDemo2ViewModel()
        {
            return Demo2ViewModel.LoadViewModel();
        }

        private DashboardViewModel CreateDashboardViewModel()
        {
            return DashboardViewModel.LoadViewModel(_dashboard, _dashboardStore, new NavigationService(_navigationStore, CreateShortcutsViewModel), new NavigationService(_navigationStore, CreateCalendarViewModel), new NavigationService(_navigationStore, CreateCountdownViewModel), new NavigationService(_navigationStore, CreateToDosViewModel), new NavigationService(_navigationStore, CreateQuickNotesViewModel));
        }

        private CalendarViewModel CreateCalendarViewModel()
        {
            return CalendarViewModel.LoadViewModel(_dashboard, _dashboardStore);
        }

        private QuickNotesViewModel CreateQuickNotesViewModel()
        {
            return QuickNotesViewModel.LoadViewModel(_dashboardStore);
        }

        private SettingsViewModel CreateSettingsViewModel()
        {
            return SettingsViewModel.LoadViewModel(_dashboardStore);
        }

        private TimerCollectionViewModel CreateTimerCollectionViewModel()
        {
            return TimerCollectionViewModel.LoadViewModel(_dashboardStore);
        }

        private ShortcutsViewModel CreateShortcutsViewModel()
        {
            return ShortcutsViewModel.LoadViewModel(_dashboard, _dashboardStore);
        }

        private CountdownsViewModel CreateCountdownViewModel()
        {
            return CountdownsViewModel.LoadViewModel(_dashboardStore);
        }
        private ToDosViewModel CreateToDosViewModel()
        {
            return ToDosViewModel.LoadViewModel(_dashboard, _dashboardStore);
        }
    }

}
