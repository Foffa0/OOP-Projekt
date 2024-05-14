using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_LernDashboard.Models
{
    class NormalToDo : ToDo
    {
        public override void check()
        {
            if (IsChecked)
                throw new InvalidOperationException("Cannot check already checked ToDo");

            this.IsChecked = true;
        }
        public NormalToDo(string description) : base(description)
            {

            }
    }
}
