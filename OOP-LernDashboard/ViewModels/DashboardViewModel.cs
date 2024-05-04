using OOP_LernDashboard.Commands;
using OOP_LernDashboard.Models;
using System.Windows.Input;

namespace OOP_LernDashboard.ViewModels
{
    internal class DashboardViewModel : ViewModelBase
    {
        private string? _firstName = "Studierende Person";
        private string? _welcomeMessage;

        public string? WelcomeMessage
        {
            get { return _welcomeMessage; }
            set
            {
                _welcomeMessage = value;
                OnPropertyChanged(nameof(WelcomeMessage));
            }
        }

        private string? _toDoDesc;
        public string? ToDoDesc
        {
            get { return _toDoDesc; }
            set
            {
                _toDoDesc = value;
                OnPropertyChanged(nameof(ToDoDesc));
            }
        }

        public ICommand AddCommand { get; }

        public DashboardViewModel(Dashboard dashboard)
        {
            this.WelcomeMessage = $"Hallo {_firstName}!";
            AddCommand = new AddToDoCommand(this, dashboard);
        }

        public static DashboardViewModel LoadViewModel(Dashboard dashboard)
        {
            DashboardViewModel viewModel = new DashboardViewModel(dashboard);
            return viewModel;
        }
    }
}
