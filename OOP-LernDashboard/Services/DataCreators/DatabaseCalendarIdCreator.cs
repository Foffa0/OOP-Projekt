using OOP_LernDashboard.DbContexts;
using OOP_LernDashboard.DTOs;

namespace OOP_LernDashboard.Services.DataCreators
{
    class DatabaseCalendarIdCreator : IDataCreator<string>
    {
        private readonly DashboardDbContextFactory _dbContextFactory;

        public DatabaseCalendarIdCreator(DashboardDbContextFactory dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public async Task CreateModel(string id)
        {
            using (DashboardDbContext context = _dbContextFactory.CreateDbContext())
            {
                context.CalendarIds.Add(ToCalendarDTO(id));
                await context.SaveChangesAsync();
            }
        }

        public async Task DeleteModel(string id)
        {
            using (DashboardDbContext context = _dbContextFactory.CreateDbContext())
            {
                // removes the id from the database
                context.CalendarIds.Remove(ToCalendarDTO(id));

                await context.SaveChangesAsync();
            }
        }

        private CalendarDTO ToCalendarDTO(string id)
        {
            return new CalendarDTO()
            {
                Id = id
            };
        }
    }
}
