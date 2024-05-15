using OOP_LernDashboard.Models;
using OOP_LernDashboard.Services;
using OOP_LernDashboard.Stores;
using OOP_LernDashboard.ViewModels;
using System.Windows;
using OOP_LernDashboard.Services;
using OOP_LernDashboard.Models;
using OOP_LernDashboard.DbContexts;
using OOP_LernDashboard.Services.DataCreators;
using OOP_LernDashboard.Services.DataProviders;
using Microsoft.EntityFrameworkCore;

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


            _dashboardStore = new DashboardStore(_toDoCreator, _toDoProvider, _shortcutCreator, _shortcutProvider);
            _navigationStore = new NavigationStore();
            _dashboard = new Dashboard(_dashboardStore);
        }
        protected override void OnStartup(StartupEventArgs e)
        {
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
                    new NavigationService(_navigationStore, CreateTimerViewModel),
                    new NavigationService(_navigationStore, CreateShortcutsViewModel)
                    )
            };
            MainWindow.Show();

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
            return DashboardViewModel.LoadViewModel(_dashboard, _dashboardStore, new NavigationService(_navigationStore, CreateShortcutsViewModel));
        }

        private CalendarViewModel CreateCalendarViewModel()
        {
            return CalendarViewModel.LoadViewModel(_dashboard, _dashboardStore);
        }

        private QuickNotesViewModel CreateQuickNotesViewModel()
        {
            return QuickNotesViewModel.LoadViewModel();
        }

        private SettingsViewModel CreateSettingsViewModel()
        {
            return SettingsViewModel.LoadViewModel(_dashboardStore);
        }

        private TimerViewModel CreateTimerViewModel()
        {
            return TimerViewModel.LoadViewModel();
        }

        private ShortcutsViewModel CreateShortcutsViewModel()
        {
            return ShortcutsViewModel.LoadViewModel(_dashboard, _dashboardStore);
        }
    }

}
