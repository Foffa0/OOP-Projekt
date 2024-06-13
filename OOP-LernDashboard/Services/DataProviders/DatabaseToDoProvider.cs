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
        private readonly Models.LinkedList<RecurringToDo> _updatedRecurringToDoList;
        public Models.LinkedList<RecurringToDo> UpdatedRecurringToDoList => _updatedRecurringToDoList;

        public DatabaseToDoProvider(DashboardDbContextFactory dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
            _updatedRecurringToDoList = new Models.LinkedList<RecurringToDo>();
        }

        public async Task<IEnumerable<ToDo>> GetAllModels()
        {
            using (DashboardDbContext context = _dbContextFactory.CreateDbContext())
            {
                IEnumerable<ToDoDTO> toDoDTOs = await context.ToDos.ToListAsync();
                _updatedRecurringToDoList.Clear();
                return toDoDTOs.Select(r => ToToDo(r));
            }
        }

        private ToDo ToToDo(ToDoDTO r)
        {
            if (!r.IsRecurringToDo)
                return new ToDo(r.Id, r.Description, r.IsChecked);
            else
            {
                RecurringToDo reToDo = new RecurringToDo(r.Id, r.Description, r.IsChecked, r.StartTime ?? DateTime.Now, r.IntervalTime ?? TimeSpan.Zero);
                if (r.IsChecked != reToDo.IsChecked)
                {
                    _updatedRecurringToDoList.Add(reToDo);
                }
                return reToDo;
            }
        }
    }
}
