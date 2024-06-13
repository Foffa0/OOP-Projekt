using OOP_LernDashboard.Stores;
using OOP_LernDashboard.ViewModels;

namespace OOP_LernDashboard.Commands
{
    class DeleteTimerCommand : CommandBase
    {
        DashboardStore _dashboardStore;


        public DeleteTimerCommand(DashboardStore dashboardStore)
        {
            _dashboardStore = dashboardStore;
        }

        public override void Execute(object? parameter)
        {
            if (parameter != null)
            {
                _ = _dashboardStore.DeleteTimer(_dashboardStore.Timers.Where(i => i.Id == ((TimerViewModel)parameter).Id).Single());
            }
        }
    }
}
