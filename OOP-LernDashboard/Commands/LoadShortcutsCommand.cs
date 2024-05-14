using OOP_LernDashboard.Stores;
using OOP_LernDashboard.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
