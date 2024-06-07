using OOP_LernDashboard.Models;
using OOP_LernDashboard.Stores;

namespace OOP_LernDashboard.Commands
{
    internal class DeleteQuickNoteCommand : CommandBase
    {
        private QuickNote _quickNote;
        private DashboardStore _dashboardStore;

        public DeleteQuickNoteCommand(QuickNote quickNote, DashboardStore dashboardStore)
        {
            _quickNote = quickNote;
            _dashboardStore = dashboardStore;
        }

        public override async void Execute(object? parameter)
        {
            await _dashboardStore.DeleteQuickNote(_quickNote);
        }
    }
}
