using OOP_LernDashboard.Stores;
using OOP_LernDashboard.ViewModels;

namespace OOP_LernDashboard.Commands
{
    internal class LoadToDosCommand : AsyncCommandBase
    {
        private readonly ToDosViewModel _viewModel;
        private readonly DashboardStore _dashboardStore;
        public LoadToDosCommand(ToDosViewModel viewModel, DashboardStore dashboardStore)
        {
            _viewModel = viewModel;
            _dashboardStore = dashboardStore;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            await _dashboardStore.Load();
            _viewModel.UpdateToDos(_dashboardStore.ToDos);
        }
    }
}
