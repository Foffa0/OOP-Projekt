﻿using OOP_LernDashboard.Models;
using OOP_LernDashboard.Stores;
using OOP_LernDashboard.ViewModels;

namespace OOP_LernDashboard.Commands
{
    /// <summary>
    /// When executed loads the data required by the dashboard viewmodel
    /// </summary>
    internal class LoadDashboardDataCommand : AsyncCommandBase
    {
        private readonly DashboardViewModel _viewModel;
        private readonly DashboardStore _dashboardStore;

        public LoadDashboardDataCommand(DashboardViewModel viewModel, DashboardStore dashboardStore)
        {
            _viewModel = viewModel;
            _dashboardStore = dashboardStore;
        }

        public override async Task ExecuteAsync(object? parameter)
        {
            await _dashboardStore.Load();
            _viewModel.UpdateToDos(_dashboardStore.ToDos.Where(toDo => !toDo.IsChecked||toDo is RecurringToDo));
            _viewModel.UpdateShortcuts(_dashboardStore.Shortcuts);
            _viewModel.UpdateCountdowns(_dashboardStore.Countdowns);
            _viewModel.UpdateWelcomeName(_dashboardStore.WelcomeName);
            _viewModel.UpdateQuickNotes(_dashboardStore.QuickNotes);

            // load calendar Events when possible
            if (_dashboardStore.GoogleCalendar != null)
            {
                await _dashboardStore.GoogleCalendar.LoadEvents(
                    start: DateTime.Now.Date,
                    end: DateTime.Now.Date.AddDays(30)
                    );
                _viewModel.UpdateCalendarEvents(_dashboardStore.GoogleCalendar.Events);
            }
        }
    }
}
