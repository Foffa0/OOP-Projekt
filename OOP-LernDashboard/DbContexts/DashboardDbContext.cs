using Microsoft.EntityFrameworkCore;
using OOP_LernDashboard.DTOs;
using OOP_LernDashboard.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_LernDashboard.DbContexts
{
    internal class DashboardDbContext : DbContext
    {
        public DashboardDbContext(DbContextOptions options) : base(options) { }

        public DbSet<ToDoDTO> ToDos { get; set; }
    }
}
