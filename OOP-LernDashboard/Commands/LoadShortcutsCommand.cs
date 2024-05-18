using OOP_LernDashboard.Stores;
using OOP_LernDashboard.ViewModels;

namespace OOP_LernDashboard.Commands
{
    internal class LoadShortcutsCommand : AsyncCommandBase
    {
        private readonly ShortcutsViewModel _viewModel;
        private readonly DashboardStore _dashboardStore;

        public LoadShortcutsCommand(ShortcutsViewModel viewModel, DashboardStore dashboardStore)
        {
            _viewModel = viewModel;
            _dashboardStore = dashboardStore;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            await _dashboardStore.Load();
            _viewModel.UpdateShortcuts(_dashboardStore.Shortcuts);
        }
    }
}
