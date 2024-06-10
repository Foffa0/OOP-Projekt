namespace OOP_LernDashboard.Models
{
    internal class RecurringToDo : ToDo
    {
        public DateTime? NextDate { get; set; }
        public DateTime? StartTime { get; set; }
        public TimeSpan TimeInterval { get; set; }

        public RecurringToDo(string description, bool isChecked, DateTime startTime, TimeSpan timeInterval) : base(description, isChecked)
        {   
            StartTime = startTime;
            TimeInterval = timeInterval;
        }
        public DateTime GenerateNextDate()
        {
            return DateTime.Now; //Wrong Time
        }
        public override void check()
        {
            NextDate = GenerateNextDate();
            IsChecked = false;
        }
    }
}
