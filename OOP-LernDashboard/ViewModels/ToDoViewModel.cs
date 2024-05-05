using OOP_LernDashboard.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_LernDashboard.ViewModels
{
    internal class ToDoViewModel
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
