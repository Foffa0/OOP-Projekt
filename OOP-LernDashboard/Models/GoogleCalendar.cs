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

        private Calendar _calendar;

        public IList<CalendarEvent> Events { get; private set; }

        public GoogleCalendar(string authToken)
        {
            AuthToken = authToken;
            Events = new List<CalendarEvent>();

            _calendarService = GetCalendarService(AuthToken);

            CalendarListResource.ListRequest request = _calendarService.CalendarList.List();
            CalendarList calendars = request.Execute();

            // check if there is already a calendar matching the name
            IList<CalendarListEntry> calendarListEntries = calendars.Items;
            foreach (var calendar in calendarListEntries)
            {
                if (calendar.Summary.Equals(CalendarName))
                {
                    _calendar = _calendarService.Calendars.Get(calendar.Id).Execute();
                    MessageBox.Show($"Erfolgreich Kalender {calendar.Summary} zum LernDashboard hinzugefügt.", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
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

        public void AddEvent(CalendarEvent calendarEvent)
        {
            Event e = new Event();
            e.Summary = calendarEvent.Title;
            e.Start = new EventDateTime()
            {
                DateTime = calendarEvent.StartTime
            };
            e.End = new EventDateTime()
            {
                DateTime = calendarEvent.EndTime
            };
            _calendarService.Events.Insert(e, _calendar.Id).Execute();
        }

        public IList<CalendarEvent> GetEvents()
        {
            return _calendarService.Events.List(_calendar.Id).Execute().Items.Select(e => new CalendarEvent(e.Summary, e.Start.DateTime, e.End.DateTime)).ToList();
        }

        public async Task LoadEvents()
        {
            Events events = await _calendarService.Events.List(_calendar.Id).ExecuteAsync();
            this.Events = events.Items.Select(e => new CalendarEvent(e.Summary, e.Start.DateTime, e.End.DateTime)).ToList();
        }
    }
}
