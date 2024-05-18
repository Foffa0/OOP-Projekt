using OOP_LernDashboard.Models;
using OOP_LernDashboard.Stores;
using OOP_LernDashboard.ViewModels;

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
            var calendarEvent = new CalendarEvent(_calendarViewModel.NewEventTitle, "test123", new DateTime(2024, 5, 12, 10, 30, 50), new DateTime(2024, 5, 12, 10, 40, 50));
            _dashboardStore.GoogleCalendar.AddEvent(calendarEvent);
        }
    }
}
