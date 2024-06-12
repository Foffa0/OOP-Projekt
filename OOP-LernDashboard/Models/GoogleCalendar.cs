using Google.Apis.Auth.OAuth2;
using Google.Apis.Calendar.v3;
using Google.Apis.Calendar.v3.Data;
using Google.Apis.Services;
using Calendar = Google.Apis.Calendar.v3.Data.Calendar;


namespace OOP_LernDashboard.Models
{
    class GoogleCalendar
    {
        public string AuthToken { get; }

        public DateTime Start { get; set; } // The start of the month to display events for

        private CalendarService _calendarService;

        public IList<CalendarEvent> Events { get; private set; }

        // List of all calendars the user wants to display
        public readonly LinkedList<string> CalendarIds;

        // List of all calendars the user can select
        public readonly LinkedList<Calendar> AllCalendars;

        public GoogleCalendar(string authToken, bool isAfterAppStartup = true)
        {
            AuthToken = authToken;

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

        public void AddEvent(CalendarEvent calendarEvent, string calendarId)
        {
            Event e = ToGoogleEvent(calendarEvent);
            Event @event = _calendarService.Events.Insert(e, calendarId).Execute();
            calendarEvent.Id = @event.Id;
        }

        public void ClearEvents()
        {
            this.Events.Clear();
        }

        /// <summary>
        /// Loads events from the calendar into the Events property
        /// </summary>
        /// <returns></returns>
        public async Task LoadEvents(DateTime? start = null, DateTime? end = null)
        {
            var eventTasks = CalendarIds.Select(id => GetEventsForCalendarAsync(id, start: start, end: end)).ToArray();
            var allEvents = await Task.WhenAll(eventTasks);
            var combinedEvents = allEvents.SelectMany(e => e).ToList();

            // sort events by start time
            combinedEvents.Sort((e1, e2) => e1.StartTime.CompareTo(e2.StartTime));

            // remove all events that are falsely in the list where its datetime is before the start date
            // only for leading elements in list
            while (combinedEvents.Any() && combinedEvents[0] != null && combinedEvents[0].StartTime < (start ?? Start))
            {
                combinedEvents.RemoveAt(0);
            }

            this.Events = combinedEvents;
        }

        /// <summary>
        /// Loads all calendars the user has access
        /// When onlyEditable is set, only those where the user has write access are loaded
        /// </summary>
        /// <param name="onlyEditable"></param>
        /// <returns></returns>
        public async Task LoadAllCalendars(bool onlyEditable = false)
        {
            CalendarListResource.ListRequest request = _calendarService.CalendarList.List();

            if (onlyEditable)
            {
                request.MinAccessRole = CalendarListResource.ListRequest.MinAccessRoleEnum.Owner;
            }

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


        private async Task<List<CalendarEvent>> GetEventsForCalendarAsync(
            string calendarId,
            DateTime? start = null,
            DateTime? end = null,
            int max = 250
            )
        {
            EventsResource.ListRequest request = _calendarService.Events.List(calendarId);
            request.TimeMinDateTimeOffset = start ?? Start;
            request.TimeMaxDateTimeOffset = end ?? Start.AddMonths(1).AddDays(-1);
            request.OrderBy = EventsResource.ListRequest.OrderByEnum.StartTime;
            request.SingleEvents = true;
            request.MaxResults = max;
            Events events = await request.ExecuteAsync();

            bool canEdit = events.AccessRole == "owner" || events.AccessRole == "writer";
            return events.Items.Select(e => ToCalendarEvent(e, calendarId, canEdit)).ToList();
        }

        /// <summary>
        /// Updates the event in the Google Calendar
        /// </summary>
        /// <param name="calendarEvent"></param>
        /// <returns></returns>
        public async Task UpdateEvent(CalendarEvent calendarEvent)
        {
            Event e = ToGoogleEvent(calendarEvent);
            await _calendarService.Events.Update(e, calendarEvent.CalendarId, calendarEvent.Id).ExecuteAsync();
            this.Events = this.Events.Select(e => e.Id == calendarEvent.Id ? calendarEvent : e).ToList();
        }

        public async Task DeleteEvent(CalendarEvent calendarEvent)
        {
            await _calendarService.Events.Delete(calendarEvent.CalendarId, calendarEvent.Id).ExecuteAsync();
            this.Events = this.Events.Where(e => e.Id != calendarEvent.Id).ToList();
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

        private static CalendarEvent ToCalendarEvent(Event e, string calendarId, bool canEdit)
        {
            if (e.Start.Date != null)
            {
                // whole day event
                return new CalendarEvent(
                    calendarId,
                    e.Summary,
                    e.Description,
                    canEdit,
                    DateTime.Parse(e.Start.Date),
                    null,
                    e.Id);
            }
            else
            {
                if (e.Start.DateTimeDateTimeOffset == null || e.End.DateTimeDateTimeOffset == null)
                    throw new NullReferenceException("Event does not have sufficiant data");

                // event with start and end time
                return new CalendarEvent(
                    calendarId,
                    e.Summary,
                    e.Description,
                    canEdit,
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
