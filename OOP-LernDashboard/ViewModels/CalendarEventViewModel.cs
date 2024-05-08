using OOP_LernDashboard.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_LernDashboard.ViewModels
{
    class CalendarEventViewModel : ViewModelBase
    {
        private readonly CalendarEvent _calendarEvent;

        public string? Title => _calendarEvent.Title;

        public CalendarEventViewModel(CalendarEvent calendarEvent)
        {
            _calendarEvent = calendarEvent;
        }
    }
}
