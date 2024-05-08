using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_LernDashboard.Models
{
    class CalendarEvent
    {
        public string Title {  get; }
        public DateTime? StartTime { get; }
        public DateTime? EndTime { get; }

        public CalendarEvent(string title, DateTime? startTime, DateTime? endTime)
        {
            Title = title;
            StartTime = startTime;
            EndTime = endTime;
        }
    }
}
