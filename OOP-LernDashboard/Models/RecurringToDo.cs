namespace OOP_LernDashboard.Models
{
    internal class RecurringToDo : ToDo
    {
        public DateTime? NextDate { get; set; }
        public int IntervalHours { get; set; }
        public int IntervalDays { get; set; }
        public int IntervalMonths { get; set; }
        public int IntervalYears { get; set; }

        public RecurringToDo(string description, bool isChecked, int intertervalHours, int intertervalDays, int intertervalMonths, int intertervalYears) : base(description, isChecked)
        {
            this.IntervalHours = intertervalHours;
            this.IntervalDays = intertervalDays;
            this.IntervalMonths = intertervalMonths;
            this.IntervalYears = intertervalYears;
        }
        public DateTime GenerateNextDate()
        {
            return DateTime.Now.AddDays(IntervalDays); //Wrong Time
        }
        public override void check()
        {
            NextDate = GenerateNextDate();
            IsChecked = false;
        }
    }
}
