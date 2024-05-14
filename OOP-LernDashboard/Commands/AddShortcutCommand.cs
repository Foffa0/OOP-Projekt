using OOP_LernDashboard.Models;
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
    internal class AddShortcutCommand : CommandBase
    {
        private readonly ShortcutsViewModel _shortcutsViewModel;
        private readonly DashboardStore _dashboardStore;

        public AddShortcutCommand(ShortcutsViewModel shortcutsViewModel, DashboardStore dashboardStore)
        {
            _shortcutsViewModel = shortcutsViewModel;
            _dashboardStore = dashboardStore;
        }


        public override async void Execute(object? parameter)
        {

            Shortcut shortcut = new Shortcut(ShortcutType.Application, _shortcutsViewModel.NewShortcutPath, _shortcutsViewModel.NewShortcutName, "r/programerHumor");
            _shortcutsViewModel.NewShortcutPath = "";
            _shortcutsViewModel.NewShortcutName = "";
            await _dashboardStore.AddShortcut(shortcut);

        }
    }
}
