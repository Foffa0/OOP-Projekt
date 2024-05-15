using OOP_LernDashboard.Models;
using OOP_LernDashboard.Stores;
using OOP_LernDashboard.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace OOP_LernDashboard.Commands
{
    internal class DeleteShortcutCommand : CommandBase
    {
        private readonly Shortcut _shortcut;
        private readonly DashboardStore _dashboardStore;

        public DeleteShortcutCommand(Shortcut shortcut, DashboardStore dashboardStore)
        {
            _shortcut = shortcut;
            _dashboardStore = dashboardStore;
        }

        public override void Execute(object? parameter)
        {
            MessageBoxResult result =  MessageBox.Show($"Möchtest du diesen Shortcut ({_shortcut.Name}) wirklich löschen?", "Title", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if(result == MessageBoxResult.Yes)
            {
                _ = _dashboardStore.DeleteShortcut(_shortcut);
            }
        }
    }
}
