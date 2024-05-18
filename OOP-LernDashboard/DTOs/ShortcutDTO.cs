using OOP_LernDashboard.Models;
using System.ComponentModel.DataAnnotations;

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
