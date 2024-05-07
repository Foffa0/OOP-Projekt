using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_LernDashboard.Models
{
    internal class RecurringToDo : ToDo
    {
        public DateTime? NextDate { get; set; }
        public int Interval { get; set; }

        public RecurringToDo(string description, int interval) : base(description)
        {
            this.Interval = interval;
        }
        //public void NextDate()
        //{
           
        //}
        //public override void check()
        //{

        //    this.IsChecked = true;
        //}
    }
}
