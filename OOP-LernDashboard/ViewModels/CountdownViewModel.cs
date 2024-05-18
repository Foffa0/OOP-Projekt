using OOP_LernDashboard.Models;

namespace OOP_LernDashboard.ViewModels
{
    internal class CountdownViewModel : ViewModelBase
    {
        private readonly Countdown _countdown;

        public DateOnly Date => _countdown.Date;

        public string Description => _countdown.Description;

        public int DaysLeft { get; set; }

        public CountdownViewModel(Countdown countdown)
        {
            _countdown = countdown;

            // get difference between today and the countdown's date
            DateOnly today = DateOnly.FromDateTime(DateTime.Now);
            DaysLeft = Date.DayNumber - today.DayNumber;
        }
    }
}
