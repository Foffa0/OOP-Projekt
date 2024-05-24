using Google.Apis.Calendar.v3.Data;
using OOP_LernDashboard.Commands;
using OOP_LernDashboard.Stores;
using System.Windows.Input;

namespace OOP_LernDashboard.ViewModels
{
    internal class CalendarSelectViewModel
    {
        public Calendar Calendar { get; private set; }

        public string Id => Calendar.Id;
        public string Name => Calendar.Summary;

        private bool _isSelected;
        public bool IsSelected => _isSelected;

        public ICommand UpdateCalendarSelected { get; }

        public CalendarSelectViewModel(Calendar calendar, DashboardStore dashboardStore, bool isSelected)
        {
            Calendar = calendar;
            UpdateCalendarSelected = new UpdateCalendarSelectedCommand(this, dashboardStore);
            _isSelected = isSelected;
        }
    }
}
