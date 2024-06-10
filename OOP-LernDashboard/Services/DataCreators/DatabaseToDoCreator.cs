using OOP_LernDashboard.DbContexts;
using OOP_LernDashboard.DTOs;
using OOP_LernDashboard.Models;

namespace OOP_LernDashboard.Services.DataCreators
{
    /// <summary>
    /// Creates, deletes, updates todos in the database
    /// </summary>
    internal class DatabaseToDoCreator : IDataCreator<ToDo>
    {

        private readonly DashboardDbContextFactory _dbContextFactory;

        public DatabaseToDoCreator(DashboardDbContextFactory dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }
        public async Task CreateModel(ToDo model)
        {
            using (DashboardDbContext context = _dbContextFactory.CreateDbContext())
            {
                ToDoDTO toDoDTO = ToToDoDTO(model);

                context.ToDos.Add(toDoDTO);
                await context.SaveChangesAsync();
            }
        }

        public async Task DeleteModel(ToDo model)
        {
            using (DashboardDbContext context = _dbContextFactory.CreateDbContext())
            {
                ToDoDTO toDoDTO = ToToDoDTO(model);

                context.ToDos.Remove(toDoDTO);
                await context.SaveChangesAsync();
            }
        }

        private ToDoDTO ToToDoDTO(ToDo toDo)
        {
            return new ToDoDTO()
            {
                Id = toDo.Id,
                Description = toDo.Description,
                IsChecked = toDo.IsChecked,
                //StartTime=toDo.

            };
        }
    }
}
