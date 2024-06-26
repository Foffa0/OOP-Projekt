﻿using OOP_LernDashboard.Stores;
using OOP_LernDashboard.ViewModels;

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
