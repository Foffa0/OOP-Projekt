namespace OOP_LernDashboard.Models
{
    class CalendarEvent
    {
        // used to identify the event in the google calendar
        // when set to null, it means that the event is not yet in the google calendar
        private string? _id;
        public string? Id
        {
            get => _id;
            set
            {
                _id ??= value;
            }
        }

        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime? EndTime { get; set; } // no EndTime means AllDayEvent

        public bool IsAllDayEvent => !EndTime.HasValue;

        public string CalendarId { get; set; }

        public bool CanEdit { get; set; }

        public CalendarEvent(string calendarId, string title, string Desc, bool canEdit, DateTime startTime, DateTime? endTime, string? id = null)
        {
            CalendarId = calendarId;
            _id = id;
            Title = title;
            Description = Desc;
            CanEdit = canEdit;
            StartTime = startTime;
            EndTime = endTime;
        }
    }
}
