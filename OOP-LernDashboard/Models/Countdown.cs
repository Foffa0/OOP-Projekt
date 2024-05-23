namespace OOP_LernDashboard.Models
{
    internal class Countdown
    {
        public Guid Id { get; }

        public DateOnly Date { get; }

        public string Description { get; }

        public Countdown(DateOnly date, string description)
        {
            Id = Guid.NewGuid();
            Date = date;
            Description = description;
        }

        public Countdown(Guid id, DateOnly date, string description)
        {
            Id = id;
            Date = date;
            Description = description;
        }
    }
}
