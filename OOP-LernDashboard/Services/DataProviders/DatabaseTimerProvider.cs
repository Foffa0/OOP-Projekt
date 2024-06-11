using Microsoft.EntityFrameworkCore;
using OOP_LernDashboard.DbContexts;
using OOP_LernDashboard.DTOs;
using OOP_LernDashboard.Models;

namespace OOP_LernDashboard.Services.DataProviders
{
    class DatabaseTimerProvider : IDataProvider<Models.Timer>
    {
        private readonly DashboardDbContextFactory _dbContextFactory;

        public DatabaseTimerProvider(DashboardDbContextFactory dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public async Task<IEnumerable<Models.Timer>> GetAllModels()
        {
            using (DashboardDbContext context = _dbContextFactory.CreateDbContext())
            {
                IEnumerable<TimerDTO> timerDTOs = await context.Timers.ToListAsync();
                return timerDTOs.Select(r => ToTimer(r));
            }
        }

        private static Models.Timer ToTimer(TimerDTO timer)
        {
            return new Models.Timer(timer.Id, timer.TimerName, timer.EndTime, timer.ElapsedTime, timer.TotalTime, timer.TickSize, timer.isPaused);
        }
    }
}
