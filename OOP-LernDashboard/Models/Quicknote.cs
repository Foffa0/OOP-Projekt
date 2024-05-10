using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_LernDashboard.Models
{
    internal class QuickNote
    {
        public Guid Id { get; }
        public string Note { get; set; }

        public QuickNote(string Note)
        {
            Id = new Guid();
            this.Note = Note;
        }
    }
}
