using OOP_LernDashboard.Stores;
using OOP_LernDashboard.ViewModels;
using System.Configuration;
using System.Data;
using System.Windows;
using System.Windows.Navigation;

namespace OOP_LernDashboard
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly NavigationStore _navigationStore;

        public App()
        {
            _navigationStore = new NavigationStore();
        }
        protected override void OnStartup(StartupEventArgs e)
        {

            _navigationStore.CurrentViewModel = CreateDemoViewModel();

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
            return DemoViewModel.LoadViewModel();
        }
    }

}
