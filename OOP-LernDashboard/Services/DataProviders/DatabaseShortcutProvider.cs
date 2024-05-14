using Microsoft.EntityFrameworkCore;
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
    internal class DatabaseShortcutProvider : IDataProvider<Shortcut>
    {
        private readonly DashboardDbContextFactory _dbContextFactory;

        public DatabaseShortcutProvider(DashboardDbContextFactory dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public async Task<IEnumerable<Shortcut>> GetAllModels()
        {
            using (DashboardDbContext context = _dbContextFactory.CreateDbContext())
            {
                IEnumerable<ShortcutDTO> shortcutDTOs = await context.Shortcuts.ToListAsync();

                return shortcutDTOs.Select(r => ToShortcut(r));
            }
        }

        private static Shortcut ToShortcut(ShortcutDTO r)
        {
            return new Shortcut(r.Id, r.Type, r.Path, r.Name, r.IconPath);
        }
    }
}
