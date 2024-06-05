using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_LernDashboard.DTOs
{
    internal class QuickNoteDTO
    {
        [Key]
        public Guid Id { get; set; }
        public string Note { get; set; }
    }
}
