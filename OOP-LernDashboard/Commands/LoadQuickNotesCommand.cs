using OOP_LernDashboard.Stores;
using OOP_LernDashboard.ViewModels;

namespace OOP_LernDashboard.Commands
{
    internal class LoadQuickNotesCommand : AsyncCommandBase
    {
        private readonly QuickNotesViewModel _quickNotesViewModel;
        private readonly DashboardStore _dashboardStore;

        public LoadQuickNotesCommand(QuickNotesViewModel quickNotesViewModel, DashboardStore dashboardStore)
        {
            _dashboardStore = dashboardStore;
            _quickNotesViewModel = quickNotesViewModel;
        }

        public override async Task ExecuteAsync(object? parameter)
        {
            await _dashboardStore.Load();
            _quickNotesViewModel.UpdateQuickNotes(_dashboardStore.QuickNotes);
        }
    }
}
