using OOP_LernDashboard.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_LernDashboard.DTOs
{
    internal class ShortcutDTO
    {
        [Key]
        public Guid Id { get; set; }
        public ShortcutType Type { get; set; }
        public string Path { get; set; }
        public string Name { get; set; }
        public string IconPath { get; set; }
    }
}
