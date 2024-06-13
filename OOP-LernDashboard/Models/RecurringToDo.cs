namespace OOP_LernDashboard.Models
{
    internal class RecurringToDo : ToDo
    {        
        public DateTime? StartTime { get; set; }
        public TimeSpan TimeInterval { get; set; }       
        

        public RecurringToDo(string description, bool isChecked, DateTime startTime, TimeSpan timeInterval) : base(description, isChecked)
        {   
            StartTime = startTime;
            TimeInterval = timeInterval;
            GenerateNextDate();
          
        }
        public RecurringToDo(Guid id,string description, bool isChecked, DateTime startTime, TimeSpan timeInterval) : base(id,description, isChecked)
        {
            StartTime = startTime;
            TimeInterval = timeInterval;
            GenerateNextDate();
            
        }
        public void GenerateNextDate()
        {
            if (!StartTime.HasValue)
                throw new InvalidOperationException("Keine Startzeit vorhanden.");
            if(DateTime.Now>StartTime+TimeInterval)
            {
                StartTime += Math.Floor((double)((DateTime.Now - StartTime) / TimeInterval)) *TimeInterval;
                IsChecked = false;
            }            
        }
        public override void check()
        {
             
            
        }
    }
}
