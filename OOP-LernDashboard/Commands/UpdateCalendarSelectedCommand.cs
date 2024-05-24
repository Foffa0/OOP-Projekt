using OOP_LernDashboard.Stores;
using OOP_LernDashboard.ViewModels;

namespace OOP_LernDashboard.Commands
{
    internal class UpdateCalendarSelectedCommand : CommandBase
    {
        private readonly CalendarSelectViewModel _viewModel;
        private readonly DashboardStore _dashboardStore;

        public UpdateCalendarSelectedCommand(CalendarSelectViewModel viewModel, DashboardStore dashboardStore)
        {
            _viewModel = viewModel;
            _dashboardStore = dashboardStore;
        }

        public override void Execute(object? parameter)
        {
            if (parameter is not bool)
                throw new ArgumentException("Parameter must be a boolean value");

            bool isSelected = (bool)parameter;

            _dashboardStore.SetCalendarSelected(_viewModel.Id, isSelected);
        }
    }
}
