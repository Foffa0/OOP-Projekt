using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace OOP_LernDashboard.DbContexts
{
    class DashboardDesignTimeDBContextFactory : IDesignTimeDbContextFactory<DashboardDbContext>
    {
        public DashboardDbContext CreateDbContext(string[] args)
        {
            DbContextOptions options = new DbContextOptionsBuilder().UseSqlite("Data Source=dashboard.db").Options;

            return new DashboardDbContext(options);
        }
    }
}
