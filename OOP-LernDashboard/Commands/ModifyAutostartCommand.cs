using Microsoft.Win32;
using OOP_LernDashboard.Stores;
using OOP_LernDashboard.ViewModels;

namespace OOP_LernDashboard.Commands
{
    enum ModifyAutostartType
    {
        Configure,
        Minimize
    }

    internal class ModifyAutostartCommand : CommandBase
    {
        private readonly SettingsViewModel _viewModel;
        private readonly DashboardStore _dashboardStore;

        public ModifyAutostartCommand(SettingsViewModel viewModel, DashboardStore dashboardStore)
        {
            _dashboardStore = dashboardStore;
            _viewModel = viewModel;
        }

        public override void Execute(object? parameter)
        {
            RegistryKey? key = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);

            if (key == null)
            {
                throw new Exception("Cannot open registry key while trying to configure autostart");
            }

            if (_viewModel.IsAutostartEnabled)
            {
                if (_viewModel.IsMinimizeEnabled)
                {
                    key.SetValue("LernDashboard", System.Environment.ProcessPath + " -minimize");
                    _dashboardStore.SetAutostart(AutostartConfig.Minimized);
                }
                else
                {
                    key.SetValue("LernDashboard", System.Environment.ProcessPath ?? "");
                    _dashboardStore.SetAutostart(AutostartConfig.Enabled);
                }
            }
            else
            {
                key.DeleteValue("LernDashboard", false);
                _dashboardStore.SetAutostart(AutostartConfig.Disabled);
            }
        }
    }
}
