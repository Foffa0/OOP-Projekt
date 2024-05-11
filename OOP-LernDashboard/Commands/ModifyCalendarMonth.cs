using OOP_LernDashboard.Stores;
using OOP_LernDashboard.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_LernDashboard.Commands
{
    internal class ModifyCalendarMonth : CommandBase
    {
        private readonly CalendarViewModel _viewModel;
        private readonly DashboardStore _dashboardStore;

        private int _delta;

        public ModifyCalendarMonth(CalendarViewModel viewModel, DashboardStore dashboardStore, int delta)
        {
            _viewModel = viewModel;
            _dashboardStore = dashboardStore;
            _delta = delta;
        }

        public override void Execute(object? parameter)
        {
            if(_dashboardStore.GoogleCalendar == null)
            {
                throw new InvalidOperationException("Cannot modify month on non existing calendar.");
            }
            _dashboardStore.GoogleCalendar.Start = _dashboardStore.GoogleCalendar.Start.AddMonths(_delta);
            _viewModel.LoadCalendarCommand.Execute(null);
        }
    }
}
