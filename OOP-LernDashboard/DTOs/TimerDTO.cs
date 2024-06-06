using System.ComponentModel.DataAnnotations;

namespace OOP_LernDashboard.DTOs
{
    class TimerDTO
    {
        [Key]
        public Guid Id { get; set; }
        public string TimerName { get; set; }
        public int HourInput { get; set; }
        public int MinuteInput { get; set; }
        public int SecondInput { get; set; }
    }
}
