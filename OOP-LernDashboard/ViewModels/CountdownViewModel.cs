using OOP_LernDashboard.Models;

namespace OOP_LernDashboard.ViewModels
{
    internal class CountdownViewModel : ViewModelBase
    {
        private readonly Countdown _countdown;

        public DateOnly Date => _countdown.Date;

        public string Description => _countdown.Description;

        // public int DaysLeft =>

        public CountdownViewModel(Countdown countdown)
        {
            _countdown = countdown;
        }

        /*private int GetDaysLeft(Countdown c)
        {
            DateOnly day = new DateOnly();
            
        }*/
    }
}
