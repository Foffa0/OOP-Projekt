using OOP_LernDashboard.Stores;
using OOP_LernDashboard.ViewModels;

namespace OOP_LernDashboard.Commands
{
    internal class LoadSettingsDataCommand : AsyncCommandBase
    {
        private readonly SettingsViewModel _viewModel;
        private readonly DashboardStore _dashboardStore;

        public LoadSettingsDataCommand(SettingsViewModel viewModel, DashboardStore dashboardStore)
        {
            _viewModel = viewModel;
            _dashboardStore = dashboardStore;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            await _dashboardStore.Load();

            _viewModel.IsAutostartEnabled = _dashboardStore.AutostartConfig == AutostartConfig.Enabled || _dashboardStore.AutostartConfig == AutostartConfig.Minimized;
            _viewModel.IsMinimizeEnabled = _dashboardStore.AutostartConfig == AutostartConfig.Minimized;

            if (_dashboardStore.GoogleCalendar != null)
            {
                await _dashboardStore.GoogleCalendar.LoadAllCalendars();
                _viewModel.UpdateCalendars(_dashboardStore.GoogleCalendar.AllCalendars, _dashboardStore.CalendarIds);
            }
        }
    }
}
