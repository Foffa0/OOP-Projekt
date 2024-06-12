using Microsoft.EntityFrameworkCore;
using OOP_LernDashboard.DbContexts;
using OOP_LernDashboard.DTOs;
using OOP_LernDashboard.Models;

namespace OOP_LernDashboard.Services.DataProviders
{
    /// <summary>
    /// Fetches data from the database
    /// </summary>
    internal class DatabaseToDoProvider : IDataProvider<ToDo>
    {
        private readonly DashboardDbContextFactory _dbContextFactory;

        public DatabaseToDoProvider(DashboardDbContextFactory dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public async Task<IEnumerable<ToDo>> GetAllModels()
        {
            using (DashboardDbContext context = _dbContextFactory.CreateDbContext())
            {
                IEnumerable<ToDoDTO> videoDTOs = await context.ToDos.ToListAsync();

                return videoDTOs.Select(r => ToToDo(r));
            }
        }

        private static ToDo ToToDo(ToDoDTO r)
        {
            if(!r.IsRecurringToDo)
                return new ToDo(r.Id, r.Description, r.IsChecked);
            else
                return new RecurringToDo(r.Id,r.Description, r.IsChecked,r.StartTime??DateTime.Now,r.IntervalTime??TimeSpan.Zero);
        }
    }
}
