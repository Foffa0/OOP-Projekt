﻿using OOP_LernDashboard.Stores;
using OOP_LernDashboard.ViewModels;

namespace OOP_LernDashboard.Commands
{
    internal class DeleteCalendarEventCommand : AsyncCommandBase
    {
        private readonly EventViewModel _viewModel;
        private readonly DashboardStore _dashboardStore;

        public DeleteCalendarEventCommand(EventViewModel viewModel, DashboardStore dashboardStore)
        {
            _viewModel = viewModel;
            _dashboardStore = dashboardStore;
        }

        public override async Task ExecuteAsync(object? parameter)
        {
            if (_dashboardStore.GoogleCalendar == null)
            {
                throw new InvalidOperationException("Cannot delete event on non existing calendar.");
            }

            await _dashboardStore.GoogleCalendar.DeleteEvent(_viewModel.Event);
            _viewModel.OnDeleted?.Invoke();
        }
    }
}
