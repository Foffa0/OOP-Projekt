using HandyControl.Themes;
using OOP_LernDashboard.Stores;
using OOP_LernDashboard.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_LernDashboard.Commands
{
    internal class UpdateAccentColorCommand : CommandBase
    {
        private readonly SettingsViewModel _settingsViewModel;
        private readonly DashboardStore _dashboardStore;

        public UpdateAccentColorCommand(SettingsViewModel settingsViewModel, DashboardStore dashboardStore)
        {
            _settingsViewModel = settingsViewModel;
            _dashboardStore = dashboardStore;
        }

        public override void Execute(object? parameter)
        {
            _dashboardStore.SetAccentColor(_settingsViewModel.AccentColorBrush);
        }
    }
}
