using OOP_LernDashboard.DbContexts;
using OOP_LernDashboard.DTOs;

namespace OOP_LernDashboard.Services.DataCreators
{
    class DatabaseTimerCreator : IDataCreator<Models.Timer>
    {
        private readonly DashboardDbContextFactory _dbContextFactory;

        public DatabaseTimerCreator(DashboardDbContextFactory dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public async Task CreateModel(Models.Timer model)
        {
            using (DashboardDbContext context = _dbContextFactory.CreateDbContext())
            {
                context.Timers.Add(ToTimerDTO(model));
                await context.SaveChangesAsync();
            }
        }

        public async Task DeleteModel(Models.Timer model)
        {
            using (DashboardDbContext context = _dbContextFactory.CreateDbContext())
            {
                // removes the id from the database
                // context.Timers.Remove(ToTimerDTO(model));

                await context.SaveChangesAsync();
            }
        }

        private TimerDTO ToTimerDTO(Models.Timer timer)
        {
            return new TimerDTO()
            {
                //Id = timer.Id,
                //TimerName = timer.TimerName,
                //HourInput = timer.HourInput,
                //MinuteInput = timer.MinuteInput,
                //SecondInput = timer.SecondInput
            };
        }
    }
}
