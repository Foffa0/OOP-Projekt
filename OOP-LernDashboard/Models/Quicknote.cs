namespace OOP_LernDashboard.Models
{
    internal class QuickNote
    {
        public Guid Id { get; }
        public string Note { get; set; }
        public DateTime CurrentDateTime { get; set; }

        public QuickNote(string note)
        {
            Id = Guid.NewGuid();
            Note = note;
            CurrentDateTime = DateTime.Now;
        }
        public QuickNote(Guid id, string note, DateTime currentDateTime)
        {
            Id = id;
            Note = note;
            CurrentDateTime = currentDateTime;
        }
    }
}
