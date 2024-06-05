using OOP_LernDashboard.DbContexts;
using OOP_LernDashboard.DTOs;
using OOP_LernDashboard.Models;

namespace OOP_LernDashboard.Services.DataCreators
{
    internal class DatabaseCountdownCreator : IDataCreator<Countdown>
    {
        private readonly DashboardDbContextFactory _dbContextFactory;

        public DatabaseCountdownCreator(DashboardDbContextFactory dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public async Task CreateModel(Countdown model)
        {
            using (DashboardDbContext context = _dbContextFactory.CreateDbContext())
            {
                CountdownDTO countdownDTO = ToCountdownDTO(model);

                context.Countdowns.Add(countdownDTO);
                await context.SaveChangesAsync();
            }
        }

        public async Task ModifyModel(Countdown model)
        {
            using (DashboardDbContext context = _dbContextFactory.CreateDbContext())
            {
                CountdownDTO countdownDTO = ToCountdownDTO(model);
                // sets the shortcutDTO with the same id as the model to the new values
                CountdownDTO? existingCountdown = await context.Countdowns.FindAsync(model.Id);

                if (existingCountdown == null)
                {
                    throw new Exception("Shortcut not found");
                }

                // Update the properties of the existing countdownDTO with the new values
                existingCountdown.Description = model.Description;
                existingCountdown.Date = model.Date;
                existingCountdown.Notification = model.Notification;

                // Save the changes to the database
                await context.SaveChangesAsync();
            }
        }

        public async Task DeleteModel(Countdown model)
        {
            using (DashboardDbContext context = _dbContextFactory.CreateDbContext())
            {
                CountdownDTO countdownDTO = ToCountdownDTO(model);

                context.Countdowns.Remove(countdownDTO);
                await context.SaveChangesAsync();
            }
        }

        private CountdownDTO ToCountdownDTO(Countdown countdown)
        {
            return new CountdownDTO()
            {
                Id = countdown.Id,
                Date = countdown.Date,
                Description = countdown.Description,
                Notification = countdown.Notification,
            };
        }
    }
}
