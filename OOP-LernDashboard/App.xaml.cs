using OOP_LernDashboard.Stores;
using OOP_LernDashboard.ViewModels;
using System.Configuration;
using System.Data;
using System.Windows;
using OOP_LernDashboard.Services;
using OOP_LernDashboard.Models;

namespace OOP_LernDashboard
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly NavigationStore _navigationStore;
        private readonly Dashboard _dashboard;

        public App()
        {
            _navigationStore = new NavigationStore();
            _dashboard = new Dashboard();
        }
        protected override void OnStartup(StartupEventArgs e)
        {

            _navigationStore.CurrentViewModel = CreateDashboardViewModel();

            MainWindow = new MainWindow()
            {
                DataContext = new MainViewModel(_navigationStore)
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
            return DashboardViewModel.LoadViewModel(_dashboard);
        }
    }

}
