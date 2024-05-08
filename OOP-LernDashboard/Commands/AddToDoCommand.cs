using OOP_LernDashboard.Models;
using OOP_LernDashboard.Stores;
using OOP_LernDashboard.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace OOP_LernDashboard.Commands
{
    internal class AddToDoCommand : CommandBase
    {
        private readonly DashboardViewModel _dashboardViewModel;
        private readonly DashboardStore _dashboardStore;

        public AddToDoCommand(DashboardViewModel dashboardViewModel, DashboardStore dashboardStore)
        {
            _dashboardViewModel = dashboardViewModel;
            _dashboardStore = dashboardStore;

            _dashboardViewModel.PropertyChanged += OnViewModelPropertyChanged;
        }

        public override bool CanExecute(object? parameter)
        {
            return _dashboardViewModel.ToDoDesc != null;
        }

        public override async void Execute(object? parameter)
        {
            //if (_dashboardViewModel.ToDoDesc == null)
            //    throw new NullReferenceException("Cannot create ToDo with Null as description");

            ToDo toDo = new ToDo(_dashboardViewModel.ToDoDesc);

            await _dashboardStore.AddToDo(toDo);
            
        }

        private void OnViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(DashboardViewModel.ToDoDesc))
            {
                OnCanExecutedChanged();
            }
        }
    }
}
