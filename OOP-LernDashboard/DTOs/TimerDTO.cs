using System.ComponentModel.DataAnnotations;

namespace OOP_LernDashboard.DTOs
{
    class TimerDTO
    {
        [Key]

        public Guid Id { get; set; }
        public string TimerName { get; set; }
        public TimeSpan EndTime { get; set; }
        public double ElapsedTime { get; set; }
        public double TotalTime { get; set; }
        public bool isPaused { get; set; }
        public int TickSize { get; set; }

    }
}
