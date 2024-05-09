using OOP_LernDashboard.Models;
using OOP_LernDashboard.Stores;
using OOP_LernDashboard.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_LernDashboard.Commands
{
    class CreateCalendarEventCommand : CommandBase
    {
        private readonly CalendarViewModel _calendarViewModel;
        private readonly DashboardStore _dashboardStore;

        public CreateCalendarEventCommand(CalendarViewModel calendarViewModel, DashboardStore dashboardStore)
        {
            _calendarViewModel = calendarViewModel;
            _dashboardStore = dashboardStore;
        }

        public override async void Execute(object? parameter)
        {
            var calendarEvent = new CalendarEvent(_calendarViewModel.NewEventTitle, new DateTime(2024, 5, 12, 10, 30, 50), new DateTime(2024, 5, 12, 10, 40, 50));
            _dashboardStore.GoogleCalendar.AddEvent(calendarEvent);
        }
    }
}
