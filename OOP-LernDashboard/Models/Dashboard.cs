using HandyControl.Controls;
using HandyControl.Data;
using OOP_LernDashboard.Stores;

namespace OOP_LernDashboard.Models
{
    class Dashboard
    {
        private readonly DashboardStore _dashboardStore;


        public Dashboard(DashboardStore dashboardStore)
        {
            _dashboardStore = dashboardStore;
        }

        public void CheckForNotifications()
        {
            foreach (Countdown c in _dashboardStore.Countdowns)
            {
                DateOnly today = DateOnly.FromDateTime(DateTime.Now);
                int daysLeft = c.Date.DayNumber - today.DayNumber;
                if (daysLeft <= 0 && c.Notification == false)
                {
                    NotifyIcon.ShowBalloonTip("Countdown abgelaufen", c.Description, NotifyIconInfoType.None, "NotifyIconToken");
                    Countdown newCountdown = new Countdown(c.Id, c.Date, c.Description, true);
                    _ = _dashboardStore.ModifyCountdown(newCountdown);
                }
            }

            foreach (ToDo toDo in _dashboardStore.ToDos)
            {
                DateTime now = DateTime.Now;
                
                if (toDo is RecurringToDo && ((RecurringToDo)toDo).NextDate.Value.Date == now.Date)
                {
                    NotifyIcon.ShowBalloonTip("ToDo heute fällig!", toDo.Description, NotifyIconInfoType.None, "NotifyIconToken");
                }
            }

        }
    }
}
