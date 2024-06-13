using System.ComponentModel.DataAnnotations;

namespace OOP_LernDashboard.DTOs
{
    internal class QuickNoteDTO
    {
        [Key]
        public Guid Id { get; set; }
        public string Note { get; set; }
        public DateTime CurrentDateTime { get; set; }
    }
}
