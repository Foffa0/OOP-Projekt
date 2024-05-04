using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_LernDashboard.DbContexts
{
    internal class DashboardDesignTimeDbContextFactory : IDesignTimeDbContextFactory<DashboardDbContext>
    {
        public DashboardDbContext CreateDbContext(string[] args)
        {
            DbContextOptions options = new DbContextOptionsBuilder().UseSqlite("Data Source=dashboard.db").Options;

            return new DashboardDbContext(options);
        }
    }
}
