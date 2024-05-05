using OOP_LernDashboard.Commands;
using OOP_LernDashboard.Models;
using System.Collections.ObjectModel;
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

        private readonly ObservableCollection<ToDoViewModel> _toDos;
        public IEnumerable<ToDoViewModel> ToDos => _toDos;

        public ICommand AddCommand { get; }

        public DashboardViewModel(Dashboard dashboard)
        {
            this.WelcomeMessage = $"Hallo {_firstName}!";
            AddCommand = new AddToDoCommand(this, dashboard);

            _toDos = new ObservableCollection<ToDoViewModel>
            {
                new(new ToDo("1")),
                new(new ToDo("2")),
                new(new ToDo("3")),
            };

            UpdateToDos(dashboard.ToDoList);
        }

        public static DashboardViewModel LoadViewModel(Dashboard dashboard)
        {
            DashboardViewModel viewModel = new DashboardViewModel(dashboard);
            return viewModel;
        }

        public void UpdateToDos(IEnumerable<ToDo> todos)
        {
            _toDos.Clear();

            foreach (var toDo in todos) 
            {
                _toDos.Add(new ToDoViewModel(toDo));
            }
        }
    }
}
