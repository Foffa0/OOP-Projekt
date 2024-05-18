namespace OOP_LernDashboard.Models
{
    internal class RecurringToDo : ToDo
    {
        public DateTime? NextDate { get; set; }
        public int Interval { get; set; }

        public RecurringToDo(string description, int interval) : base(description)
        {
            this.Interval = interval;
        }
        public DateTime GenerateNextDate()
        {
            return DateTime.Now.AddDays(Interval); //Wrong Time
        }
        public override void check()
        {
            NextDate = GenerateNextDate();
            IsChecked = false;
        }
    }
}
