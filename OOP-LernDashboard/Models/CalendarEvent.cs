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
                if (_id == null)
                {
                    _id = value;
                }
            }
        }

        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime? EndTime { get; set; }

        public bool IsAllDayEvent => !EndTime.HasValue;

        public CalendarEvent(string title, string Desc, DateTime startTime, DateTime? endTime, string? id = null)
        {
            _id = id;
            Title = title;
            Description = Desc;
            StartTime = startTime;
            EndTime = endTime;
        }
    }
}
