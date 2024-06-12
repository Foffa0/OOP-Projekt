using OOP_LernDashboard.Stores;
using OOP_LernDashboard.ViewModels;

namespace OOP_LernDashboard.Commands
{
    internal class LoadTimersCommand : AsyncCommandBase
    {
        private readonly TimerCollectionViewModel _timerCollectionViewModel;
        private readonly DashboardStore _dashboardStore;

        public LoadTimersCommand(TimerCollectionViewModel timerCollectionViewModel, DashboardStore dashboardStore)
        {
            _dashboardStore = dashboardStore;
            _timerCollectionViewModel = timerCollectionViewModel;
        }

        public override async Task ExecuteAsync(object? parameter)
        {
            await _dashboardStore.Load();
            _timerCollectionViewModel.UpdateTimers(_dashboardStore.Timers);
        }
    }
}
