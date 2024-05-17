using Microsoft.EntityFrameworkCore;
using OOP_LernDashboard.DTOs;

namespace OOP_LernDashboard.DbContexts
{
    internal class DashboardDbContext : DbContext
    {
        public DashboardDbContext(DbContextOptions options) : base(options) { }

        public DbSet<ToDoDTO> ToDos { get; set; }

        public DbSet<ShortcutDTO> Shortcuts { get; set; }

        public DbSet<CountdownDTO> Countdowns { get; set; }
    }
}
