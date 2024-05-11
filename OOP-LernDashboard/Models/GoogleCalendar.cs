using Google.Apis.Auth.OAuth2;
using Google.Apis.Calendar.v3;
using Google.Apis.Calendar.v3.Data;
using Google.Apis.Services;
using HandyControl.Controls;
using System.Globalization;


namespace OOP_LernDashboard.Models
{
    class GoogleCalendar
    {
        private const string CalendarName = "LernDashboard";

        public string AuthToken { get; }

        public DateTime Start { get; set; }

        private CalendarService _calendarService;

        private Google.Apis.Calendar.v3.Data.Calendar _calendar;

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
                    MessageBox.Success($"Erfolgreich Kalender {calendar.Summary} zum LernDashboard hinzugefügt.", "Information");
                    break;
                }
            }

            // no matching calendar is found so create one
            if (_calendar == null)
            {
                Google.Apis.Calendar.v3.Data.Calendar calendar = new Google.Apis.Calendar.v3.Data.Calendar();
                calendar.Summary = CalendarName;
                _calendar = _calendarService.Calendars.Insert(calendar).Execute();
            }

            DateTime now = DateTime.Now;
            Start = new DateTime(now.Year, now.Month, 1);
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
                DateTimeDateTimeOffset = calendarEvent.StartTime
            };
            e.End = new EventDateTime()
            {
                DateTimeDateTimeOffset = calendarEvent.EndTime
            };
            _calendarService.Events.Insert(e, _calendar.Id).Execute();
        }

        public IList<CalendarEvent> GetEvents()
        {
            return _calendarService.Events.List(_calendar.Id).Execute().Items.Select(e => new CalendarEvent(e.Summary, e.Start.DateTime ?? new DateTime(), e.End.DateTime)).ToList();
        } 

        public async Task LoadEvents()
        {
            EventsResource.ListRequest request = _calendarService.Events.List(_calendar.Id);
            request.TimeMinDateTimeOffset = Start;
            request.TimeMaxDateTimeOffset = Start.AddMonths(1).AddDays(-1);
            Events events = await request.ExecuteAsync();
            this.Events = events.Items.Select(e => ToCalendarEvent(e)).ToList();
        }

        private static CalendarEvent ToCalendarEvent(Event e)
        {
            if(e.Start.DateTime == null || e.End.DateTime == null)
            {
                return new CalendarEvent(e.Summary, DateTime.ParseExact(e.Start.Date, "yyyy-mm-dd", CultureInfo.InvariantCulture), null);
            }
            else
            {
                return new CalendarEvent(e.Summary, e.Start.DateTimeDateTimeOffset.Value.DateTime, e.End.DateTimeDateTimeOffset.Value.DateTime);
            }
        }
    }
}
