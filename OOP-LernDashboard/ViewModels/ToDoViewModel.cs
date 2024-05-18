using OOP_LernDashboard.Models;

namespace OOP_LernDashboard.ViewModels
{
    internal class ToDoViewModel : ViewModelBase
    {
        private readonly ToDo _toDo;

        public string? Description => _toDo.Description;
        public Boolean IsChecked => _toDo.IsChecked;

        public ToDoViewModel(ToDo toDo)
        {
            _toDo = toDo;
        }
    }
}
