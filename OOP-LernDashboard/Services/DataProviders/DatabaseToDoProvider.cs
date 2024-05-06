using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using OOP_LernDashboard.DbContexts;
using OOP_LernDashboard.DTOs;
using OOP_LernDashboard.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            return new ToDo(r.Id, r.Description, r.IsChecked);
        }
    }
}
