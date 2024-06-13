namespace OOP_LernDashboard.Models
{
    internal class RecurringToDo : ToDo
    {
        public DateTime? NextDate { get; set; }
        public DateTime? StartTime { get; set; }
        public TimeSpan TimeInterval { get; set; }
        public int IsCheckedCounter { get; set; }
        

        public RecurringToDo(string description, bool isChecked, DateTime startTime, TimeSpan timeInterval) : base(description, isChecked)
        {   
            StartTime = startTime;
            TimeInterval = timeInterval;
            NextDate=GenerateNextDate();
            IsCheckedCounter = 0;
        }
        public RecurringToDo(Guid id,string description, bool isChecked, DateTime startTime, TimeSpan timeInterval) : base(id,description, isChecked)
        {
            StartTime = startTime;
            TimeInterval = timeInterval;
            NextDate = GenerateNextDate();
            IsCheckedCounter = 0;
        }
        public DateTime GenerateNextDate()
        {
            if (!StartTime.HasValue)
                throw new InvalidOperationException("Keine Startzeit vorhanden.");
            TimeSpan timeDifference= DateTime.Now - StartTime.Value;
            double intervalSeconds = TimeInterval.TotalSeconds;
            double elapsedSeconds = timeDifference.TotalSeconds;
            double remainderSeconds = elapsedSeconds % intervalSeconds;

            // Calculate the next occurrence
            DateTime nextDate = DateTime.Now.AddSeconds(intervalSeconds - remainderSeconds);
            return nextDate;
        }
        public override void check()
        {
            NextDate = GenerateNextDate(); 
            IsCheckedCounter++;
        }
    }
}
