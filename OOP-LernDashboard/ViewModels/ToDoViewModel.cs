using OOP_LernDashboard.Commands;
using OOP_LernDashboard.Models;
using OOP_LernDashboard.Stores;
using System.Windows.Input;

namespace OOP_LernDashboard.ViewModels
{
    internal class ToDoViewModel : ViewModelBase
    {
        private readonly ToDo _toDo;
        public ToDo ToDo => _toDo;

        public string? Description => _toDo.Description;
        public Boolean IsChecked
        {
            get => _toDo.IsChecked;
            set
            {
                if (value)
                {
                    ToDo.check();
                }
            }
        }
        public string NextDate => (_toDo as RecurringToDo)?.NextDate.ToString() ?? "";
        public ICommand DeleteToDoCommand { get; set; }
        public ICommand CheckToDoCommand { get; set; }

        public ToDoViewModel(ToDo toDo, DashboardStore dashboardStore)
        {
            _toDo = toDo;
            DeleteToDoCommand = new DeleteToDoCommand(this, dashboardStore);
            CheckToDoCommand =new CheckToDoCommand(this,dashboardStore);
        }
    }
}

