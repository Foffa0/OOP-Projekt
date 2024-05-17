using OOP_LernDashboard.Stores;
using OOP_LernDashboard.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_LernDashboard.Commands
{
    class ModifyWelcomeName : CommandBase
    {
        private readonly SettingsViewModel _settingsViewModel;
        private readonly DashboardStore _dashboardStore;

        public ModifyWelcomeName(SettingsViewModel settingsViewModel, DashboardStore dashboardStore)
        {
            _settingsViewModel = settingsViewModel;
            _dashboardStore = dashboardStore;
        }

        public override void Execute(object? parameter)
        {
            if(_settingsViewModel.WelcomeName == null || _settingsViewModel.WelcomeName == "")
            {
                return;
            }

            _dashboardStore.SetWelcomeName(_settingsViewModel.WelcomeName);
        }
    }
}
