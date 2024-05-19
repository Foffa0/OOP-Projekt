using Microsoft.EntityFrameworkCore;
using OOP_LernDashboard.DbContexts;
using OOP_LernDashboard.DTOs;
using OOP_LernDashboard.Models;

namespace OOP_LernDashboard.Services.DataProviders
{
    internal class DatabaseCountdownProvider : IDataProvider<Countdown>
    {
        private readonly DashboardDbContextFactory _dbContextFactory;

        public DatabaseCountdownProvider(DashboardDbContextFactory dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public async Task<IEnumerable<Countdown>> GetAllModels()
        {
            using (DashboardDbContext context = _dbContextFactory.CreateDbContext())
            {
                IEnumerable<CountdownDTO> countdownDTOs = await context.Countdowns.ToListAsync();

                return countdownDTOs.Select(r => ToCountdown(r));
            }
        }

        private static Countdown ToCountdown(CountdownDTO countdown)
        {
            return new Countdown(countdown.Id, countdown.Date, countdown.Description);
        }
    }
}
