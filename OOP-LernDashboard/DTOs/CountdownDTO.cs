using System.ComponentModel.DataAnnotations;


namespace OOP_LernDashboard.DTOs
{
    internal class CountdownDTO
    {
        [Key]
        public Guid Id { get; set; }
        public DateOnly Date { get; set; }
        public string Description { get; set; }
        public bool Notification { get; set; }
    }
}
