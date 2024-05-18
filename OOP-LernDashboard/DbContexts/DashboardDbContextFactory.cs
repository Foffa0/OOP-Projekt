using Microsoft.EntityFrameworkCore;

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
