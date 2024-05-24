using Google.Apis.Auth.OAuth2;
using Google.Apis.Calendar.v3;
using Google.Apis.Calendar.v3.Data;
using Google.Apis.Services;
using System.Globalization;
using Calendar = Google.Apis.Calendar.v3.Data.Calendar;


namespace OOP_LernDashboard.Models
{
    class GoogleCalendar
    {
        public string AuthToken { get; }

        public DateTime Start { get; set; } // The start of the month to display events for

        private CalendarService _calendarService;

        private string? _selectedCalendarId;

        public IList<CalendarEvent> Events { get; private set; }

        // List of all calendars the user wants to display
        public readonly LinkedList<string> CalendarIds;

        // List of all calendars the user can select
        public readonly LinkedList<Calendar> AllCalendars;

        public GoogleCalendar(string authToken, bool isAfterAppStartup = true)
        {
            AuthToken = authToken;
            _selectedCalendarId = null;

            Events = new List<CalendarEvent>();
            CalendarIds = new LinkedList<string>();
            AllCalendars = new LinkedList<Calendar>();

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
            if (_selectedCalendarId == null)
            {
                throw new Exception("No calendar selected");
            }

            Event e = ToGoogleEvent(calendarEvent);
            Event @event = _calendarService.Events.Insert(e, _selectedCalendarId).Execute();
            calendarEvent.Id = @event.Id;
        }

        /// <summary>
        /// Loads events from the calendar for the current month into the Events property
        /// </summary>
        /// <returns></returns>
        public async Task LoadEvents(bool loadPastEvents = true)
        {
            var eventTasks = CalendarIds.Select(id => GetEventsForCalendarAsync(id)).ToArray();
            var allEvents = await Task.WhenAll(eventTasks);
            var combinedEvents = allEvents.SelectMany(e => e).ToList();
            this.Events = combinedEvents;
        }

        public async Task LoadAllCalendars()
        {
            CalendarListResource.ListRequest request = _calendarService.CalendarList.List();
            CalendarList calendars = await request.ExecuteAsync();

            // get all calendars
            IList<CalendarListEntry> calendarListEntries = calendars.Items;

            AllCalendars.Clear();
            foreach (CalendarListEntry calendar in calendarListEntries)
            {
                // casts CalendarListEntry to Calendar to add it to the list
                AllCalendars.Add(new Calendar()
                {
                    Id = calendar.Id,
                    Summary = calendar.Summary,
                    Description = calendar.Description,
                    Kind = calendar.Kind,
                    Location = calendar.Location,
                    TimeZone = calendar.TimeZone,
                    ETag = calendar.ETag,
                    ConferenceProperties = calendar.ConferenceProperties,
                });
            }
        }


        private async Task<List<CalendarEvent>> GetEventsForCalendarAsync(string calendarId, bool loadPastEvents = true)
        {
            EventsResource.ListRequest request = _calendarService.Events.List(calendarId);
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

            return events.Items.Select(e => ToCalendarEvent(e)).ToList();
        }

        /// <summary>
        /// Updates the event in the Google Calendar
        /// </summary>
        /// <param name="calendarEvent"></param>
        /// <returns></returns>
        public async Task UpdateEvent(CalendarEvent calendarEvent)
        {
            if (_selectedCalendarId == null)
            {
                throw new Exception("No calendar selected");
            }

            Event e = ToGoogleEvent(calendarEvent);
            await _calendarService.Events.Update(e, _selectedCalendarId, calendarEvent.Id).ExecuteAsync();
            this.Events = this.Events.Select(e => e.Id == calendarEvent.Id ? calendarEvent : e).ToList();
        }

        public async Task DeleteEvent(CalendarEvent calendarEvent)
        {
            if (_selectedCalendarId == null)
            {
                throw new Exception("No calendar selected");
            }

            await _calendarService.Events.Delete(_selectedCalendarId, calendarEvent.Id).ExecuteAsync();
            this.Events = this.Events.Where(e => e.Id != calendarEvent.Id).ToList();
        }

        public void SelectCalendar(string calendarId)
        {
            _selectedCalendarId = calendarId;
        }

        public void AddCalendar(string calendarId)
        {
            if (CalendarIds.Contains(calendarId))
            {
                throw new ArgumentException("Calendar already added");
            }

            CalendarIds.Add(calendarId);
        }

        public void RemoveCalendar(string calendarId)
        {
            if (!CalendarIds.Contains(calendarId))
            {
                throw new ArgumentException("Calendar not found");
            }

            CalendarIds.Remove(calendarId);
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
