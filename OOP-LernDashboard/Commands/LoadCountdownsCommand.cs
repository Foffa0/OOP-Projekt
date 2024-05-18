using OOP_LernDashboard.Stores;
using OOP_LernDashboard.ViewModels;

namespace OOP_LernDashboard.Commands
{
    class LoadCountdownsCommand : AsyncCommandBase
    {
        private readonly DashboardStore _dashboardStore;
        private readonly CountdownsViewModel _countdownsViewModel;

        public LoadCountdownsCommand(CountdownsViewModel countdownsViewModel, DashboardStore dashboardStore)
        {
            _dashboardStore = dashboardStore;
            _countdownsViewModel = countdownsViewModel;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            await _dashboardStore.Load();
            _countdownsViewModel.UpdateCountdowns(_dashboardStore.Countdowns);
        }
    }
}
