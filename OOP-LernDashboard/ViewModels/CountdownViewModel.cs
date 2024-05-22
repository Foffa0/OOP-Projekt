using OOP_LernDashboard.Models;

namespace OOP_LernDashboard.ViewModels
{
    internal class CountdownViewModel : ViewModelBase
    {
        private readonly Countdown _countdown;

        public Guid Id => _countdown.Id;

        public DateOnly Date => _countdown.Date;

        public string Description => _countdown.Description;

        public int DaysLeft { get; }

        public bool Expired { get; }

        public CountdownViewModel(Countdown countdown)
        {
            _countdown = countdown;
            // get difference between today and the countdown's date
            DateOnly today = DateOnly.FromDateTime(DateTime.Now);
            DaysLeft = Date.DayNumber - today.DayNumber;

            Expired = true ? DaysLeft <= 0 : false;
        }
    }
}
