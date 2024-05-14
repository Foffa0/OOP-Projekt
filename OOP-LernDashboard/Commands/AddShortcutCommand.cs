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
        private readonly DashboardViewModel _dashboardViewModel;
        private readonly DashboardStore _dashboardStore;

        public AddShortcutCommand(DashboardViewModel dashboardViewModel, DashboardStore dashboardStore)
        {
            _dashboardViewModel = dashboardViewModel;
            _dashboardStore = dashboardStore;
        }


        public override async void Execute(object? parameter)
        {

            Shortcut shortcut = new Shortcut(ShortcutType.Application, "test123", "Test?!", "r/programerHumor");
            await _dashboardStore.AddShortcut(shortcut);

        }
    }
}
