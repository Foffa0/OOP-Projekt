using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_LernDashboard.Models
{
    internal class Quicknote
    {
        public Guid Id { get; }
        public string Note { get; set; }

        public Quicknote(string Note)
        {
            Id = new Guid();
            this.Note = Note;
        }
    }
}
