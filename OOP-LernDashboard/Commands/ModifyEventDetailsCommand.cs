using OOP_LernDashboard.Stores;
using OOP_LernDashboard.ViewModels;

namespace OOP_LernDashboard.Commands
{
    internal class ModifyEventDetailsCommand : AsyncCommandBase
    {
        private readonly EventViewModel _eventViewModel;
        private readonly DashboardStore _dashboardStore;

        public ModifyEventDetailsCommand(EventViewModel eventViewModel, DashboardStore dashboardStore)
        {
            _eventViewModel = eventViewModel;
            _dashboardStore = dashboardStore;
        }

        public async override Task ExecuteAsync(object? parameter)
        {
            _eventViewModel.ApplyTempData();

            _eventViewModel.OnSaved?.Invoke();
            await _dashboardStore.ModifyCalendarEvent(_eventViewModel.Event);
        }
    }
}
