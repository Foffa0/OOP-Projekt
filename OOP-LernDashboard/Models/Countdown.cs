namespace OOP_LernDashboard.Models
{
    internal class Countdown
    {
        public Guid Id { get; }

        public DateOnly Date { get; }

        public string Description { get; }

        public bool Notification { get; }

        public Countdown(DateOnly date, string description, bool notification)
        {
            Id = Guid.NewGuid();
            Date = date;
            Description = description;
            Notification = notification;
        }

        public Countdown(Guid id, DateOnly date, string description, bool notification)
        {
            Id = id;
            Date = date;
            Description = description;
            Notification = notification;
        }
    }
}
