using OOP_LernDashboard.Stores;
using OOP_LernDashboard.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_LernDashboard.Commands
{
    internal class CheckToDoCommand:CommandBase
    {
        private readonly ToDoViewModel _toDoViewModel;
        private readonly DashboardStore _dashboardStore;
        public CheckToDoCommand(ToDoViewModel toDoViewModel, DashboardStore dashboardStore)
        {
            _toDoViewModel = toDoViewModel;
            _dashboardStore = dashboardStore;

            _toDoViewModel.PropertyChanged += OnViewModelPropertyChanged;
        }
        //public override bool CanExecute(object? parameter)
        //{
        //    return _toDoViewModel.ToDo.IsChecked = false;
        //}
        public override void Execute(object? parameter)
        {
            _toDoViewModel.ToDo.check(); //check Methodenaufruf?
        }
        private void OnViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(DashboardViewModel.ToDoDesc)) //IsCecked?
            {
                OnCanExecutedChanged();
            }
        }
    }
}
