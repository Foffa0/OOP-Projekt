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

        public override void Execute(object? parameter)
        {
            DateTime start = _calendarViewModel.IsWholeDay ? _calendarViewModel.NewEventDate : _calendarViewModel.NewEventDate.AddHours(_calendarViewModel.NewEventStartTime.Hour).AddMinutes(_calendarViewModel.NewEventStartTime.Minute);
            DateTime? end = _calendarViewModel.IsWholeDay ? null : _calendarViewModel.NewEventDate.AddHours(_calendarViewModel.NewEventEndTime.Hour).AddMinutes(_calendarViewModel.NewEventEndTime.Minute);
            
            var calendarEvent = new CalendarEvent(
                _calendarViewModel.NewEventTitle,
                _calendarViewModel.NewEventDescription,
                startTime: start,
                endTime: end);

            _dashboardStore.GoogleCalendar?.AddEvent(calendarEvent);
        }
    }
}
