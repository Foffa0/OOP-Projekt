using System.ComponentModel.DataAnnotations;

namespace OOP_LernDashboard.DTOs
{
    internal class ToDoDTO
    {
        [Key]
        public Guid Id { get; set; }
        public string Description { get; set; }
        public Boolean IsChecked { get; set; }
        //public DateTime StartTime { get; set; }
        //public DateTime IntervalTime { get; set; }
    }
}
