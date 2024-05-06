using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_LernDashboard.DbContexts
{
    internal class DashboardDbContextFactory
    {
        private readonly string _connectionString;

        public DashboardDbContextFactory(string connectionString)
        {
            _connectionString = connectionString;
        }
        public DashboardDbContext CreateDbContext()
        {
            DbContextOptions options = new DbContextOptionsBuilder().UseSqlite(_connectionString).Options;

            return new DashboardDbContext(options);
        }
    }
}
