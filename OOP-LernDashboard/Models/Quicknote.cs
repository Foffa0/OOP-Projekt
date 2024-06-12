namespace OOP_LernDashboard.Models
{
    internal class QuickNote
    {
        public Guid Id { get; }
        public string Note { get; set; }

        public QuickNote(string note)
        {
            Id = Guid.NewGuid();
            Note = note;
        }
        public QuickNote(Guid id, string note)
        {
            Id = id;
            Note = note;
        }
    }
}
