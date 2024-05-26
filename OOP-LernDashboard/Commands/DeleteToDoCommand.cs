using OOP_LernDashboard.Stores;
using OOP_LernDashboard.ViewModels;
using System.ComponentModel;

namespace OOP_LernDashboard.Commands
{
    internal class DeleteToDoCommand : CommandBase
    {
        private readonly ToDoViewModel _toDoViewModel;
        private readonly DashboardStore _dashboardStore;
        public DeleteToDoCommand(ToDoViewModel toDoViewModel, DashboardStore dashboardStore)
        {
            _toDoViewModel = toDoViewModel;
            _dashboardStore = dashboardStore;

            _toDoViewModel.PropertyChanged += OnViewModelPropertyChanged;
        }
        public override bool CanExecute(object? parameter)
        {
            return _toDoViewModel.IsChecked == false;
        }
        public override async void Execute(object? parameter)
        {
            await _dashboardStore.DeleteToDo(_toDoViewModel.ToDo);
        }
        private void OnViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            OnCanExecutedChanged();
        }
    }
}
