namespace OOP_LernDashboard.Models
{
    internal class QuickNote
    {
        public Guid Id { get; }
        public string Note { get; set; }

        public QuickNote(string Note)
        {
            Id = new Guid();
            this.Note = Note;
        }
    }
}
