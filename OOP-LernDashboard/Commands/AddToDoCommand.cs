using OOP_LernDashboard.Models;
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
        private readonly Dashboard _dashboard;

        public AddToDoCommand(DashboardViewModel dashboardViewModel, Dashboard dashboard)
        {
            _dashboardViewModel = dashboardViewModel;
            _dashboard = dashboard;

            _dashboardViewModel.PropertyChanged += OnViewModelPropertyChanged;
        }

        public override void Execute(object? parameter)
        {
            if (_dashboardViewModel.ToDoDesc == null)
                throw new NullReferenceException("Cannot create ToDo with Null as description");

            ToDo toDo = new ToDo(_dashboardViewModel.ToDoDesc);

            _dashboard.ToDoList.Add(toDo);

            MessageBox.Show($"Successfully created ToDo: {_dashboardViewModel.ToDoDesc}", "Success",
                    MessageBoxButton.OK, MessageBoxImage.Information);
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
