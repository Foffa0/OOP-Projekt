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

        public DateTime Start { get; set; } // The start of the month to display events for

        private CalendarService _calendarService;

        private Google.Apis.Calendar.v3.Data.Calendar _calendar;

        public IList<CalendarEvent> Events { get; private set; }

        public GoogleCalendar(string authToken, bool isAfterAppStartup = true)
        {
            AuthToken = authToken;
            Events = new List<CalendarEvent>();

            _calendarService = GetCalendarService(AuthToken);

            CalendarList calendars;
            try
            {
                CalendarListResource.ListRequest request = _calendarService.CalendarList.List();
                calendars = request.Execute();
            }
            catch (Exception e)
            {
                // TODO: Logout user
                throw new Exception("Error while loading calendar", e);
            }


            // check if there is already a calendar matching the name
            IList<CalendarListEntry> calendarListEntries = calendars.Items;
            foreach (var calendar in calendarListEntries)
            {
                if (calendar.Summary.Equals(CalendarName))
                {
                    _calendar = _calendarService.Calendars.Get(calendar.Id).Execute();
                    if (isAfterAppStartup)
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

            // Sets the month to display events for to the current month
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
            Event e = ToGoogleEvent(calendarEvent);
            Event @event = _calendarService.Events.Insert(e, _calendar.Id).Execute();
            calendarEvent.Id = @event.Id;
        }

        /// <summary>
        /// Loads events from the calendar for the current month into the Events property
        /// </summary>
        /// <returns></returns>
        public async Task LoadEvents(bool loadPastEvents = true)
        {
            EventsResource.ListRequest request = _calendarService.Events.List(_calendar.Id);
            request.TimeMinDateTimeOffset = Start;
            request.TimeMaxDateTimeOffset = Start.AddMonths(1).AddDays(-1);
            request.OrderBy = EventsResource.ListRequest.OrderByEnum.StartTime;
            request.SingleEvents = true;
            Events events = await request.ExecuteAsync();
            // if loadPastEvents is false, only keep events that are in the future or today
            if (!loadPastEvents)
            {
                events.Items = events.Items.Where(
                    e => (
                        e.Start.Date != null && DateTime.ParseExact(e.Start.Date, "yyyy-MM-dd", CultureInfo.InvariantCulture).Date >= DateTime.Now.Date)
                        || (e.Start.DateTimeDateTimeOffset != null && e.Start.DateTimeDateTimeOffset.Value.LocalDateTime.Date >= DateTime.Now.Date)
                    ).ToList();
            }
            this.Events = events.Items.Select(e => ToCalendarEvent(e)).ToList();
        }

        /// <summary>
        /// Updates the event in the Google Calendar
        /// </summary>
        /// <param name="calendarEvent"></param>
        /// <returns></returns>
        public async Task UpdateEvent(CalendarEvent calendarEvent)
        {
            Event e = ToGoogleEvent(calendarEvent);
            await _calendarService.Events.Update(e, _calendar.Id, calendarEvent.Id).ExecuteAsync();
            this.Events = this.Events.Select(e => e.Id == calendarEvent.Id ? calendarEvent : e).ToList();
        }

        public async Task DeleteEvent(CalendarEvent calendarEvent)
        {
            await _calendarService.Events.Delete(_calendar.Id, calendarEvent.Id).ExecuteAsync();
            this.Events = this.Events.Where(e => e.Id != calendarEvent.Id).ToList();
        }

        private static CalendarEvent ToCalendarEvent(Event e)
        {
            if (e.Start.Date != null)
            {
                // whole day event
                return new CalendarEvent(
                    e.Summary,
                    e.Description,
                    DateTime.Parse(e.Start.Date),
                    null,
                    e.Id);
            }
            else
            {
                // event with start and end time
                return new CalendarEvent(
                    e.Summary,
                    e.Description,
                    e.Start.DateTimeDateTimeOffset.Value.LocalDateTime,
                    e.End.DateTimeDateTimeOffset.Value.LocalDateTime,
                    e.Id);
            }
        }

        private static Event ToGoogleEvent(CalendarEvent e)
        {
            Event googleEvent = new Event();

            if (e.Id != null) googleEvent.Id = e.Id;
            googleEvent.Summary = e.Title;
            googleEvent.Description = e.Description;

            googleEvent.Start = new EventDateTime();
            googleEvent.End = new EventDateTime();

            if (e.IsAllDayEvent)
            {
                googleEvent.Start.Date = e.StartTime.ToString("yyyy-MM-dd");
                googleEvent.End.Date = e.StartTime.AddDays(1).ToString("yyyy-MM-dd");
            }
            else
            {
                googleEvent.Start.DateTimeDateTimeOffset = e.StartTime;
                googleEvent.End.DateTimeDateTimeOffset = e.EndTime;
            }

            return googleEvent;
        }
    }
}
