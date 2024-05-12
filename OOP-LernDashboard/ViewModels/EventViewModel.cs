using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_LernDashboard.ViewModels
{
    internal class EventViewModel
    {
        public string EventDesc { get; set; }
        public string EventStart { get; set; }

        public EventViewModel(string eventDesk, string eventStart)
        {
            EventDesc = eventDesk;
            EventStart = eventStart;
        }
    }
}
