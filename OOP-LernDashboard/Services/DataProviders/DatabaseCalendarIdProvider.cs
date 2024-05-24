using Microsoft.EntityFrameworkCore;
using OOP_LernDashboard.DbContexts;
using OOP_LernDashboard.DTOs;

namespace OOP_LernDashboard.Services.DataProviders
{
    class DatabaseCalendarIdProvider : IDataProvider<string>
    {
        private readonly DashboardDbContextFactory _dbContextFactory;

        public DatabaseCalendarIdProvider(DashboardDbContextFactory dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public async Task<IEnumerable<string>> GetAllModels()
        {
            using (DashboardDbContext context = _dbContextFactory.CreateDbContext())
            {
                IEnumerable<CalendarDTO> calendarDTOs = await context.CalendarIds.ToListAsync();
                return calendarDTOs.Select(r => ToCalendarInfo(r));
            }
        }

        private static string ToCalendarInfo(CalendarDTO calendar)
        {
            return calendar.Id;
        }
    }
}
