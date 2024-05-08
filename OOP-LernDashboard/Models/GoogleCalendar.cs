using Google.Apis.Auth.OAuth2;
using Google.Apis.Calendar.v3;
using Google.Apis.Calendar.v3.Data;
using Google.Apis.Services;
using System.Windows;

namespace OOP_LernDashboard.Models
{
    class GoogleCalendar
    {
        private const string CalendarName = "LernDashboard";

        public string AuthToken { get; }

        private CalendarService _calendarService;

        public LinkedList<CalendarEvent> Events { get; private set; }

        private Calendar _calendar;

        public GoogleCalendar(string authToken)
        {
            AuthToken = authToken;

            _calendarService = GetCalendarService(AuthToken);

            CalendarListResource.ListRequest request = _calendarService.CalendarList.List();
            CalendarList calendars = request.Execute();

            // check if there is already a calendar matching the name
            IList<CalendarListEntry> calendarListEntries = calendars.Items;
            foreach (var calendar in calendarListEntries)
            {
                if (calendar.Summary.Equals(CalendarName))
                {
                    _calendar = _calendarService.Calendars.Get(calendar.Id).Execute(); ;
                    MessageBox.Show(calendar.Summary, "Info", MessageBoxButton.OK, MessageBoxImage.Information);
                    break;
                }
            }

            // no matching calendar is found so create one
            if (_calendar == null)
            {
                MessageBox.Show("no calendar found.", "Alert", MessageBoxButton.OK, MessageBoxImage.Information);
                Calendar calendar = new Calendar();
                calendar.Summary = CalendarName;
                _calendar = _calendarService.Calendars.Insert(calendar).Execute();
            }

            Events = new LinkedList<CalendarEvent>();
            Events events = _calendarService.Events.List(_calendar.Id).Execute();
            foreach (var calendarEvent in events.Items)
            {
                Events.Add(new CalendarEvent(calendarEvent.Summary, calendarEvent.Start.DateTime, calendarEvent.End.DateTime));
            }
        }


        static CalendarService GetCalendarService(string token)
        {
            GoogleCredential credential = GoogleCredential.FromAccessToken(token);

            credential = credential.CreateScoped(CalendarService.Scope.Calendar);

            // Create the Calendar service.
            return new CalendarService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = "OOP-Dashboard",
            });
        }

    }
}
