using OOP_LernDashboard.DbContexts;
using OOP_LernDashboard.DTOs;
using OOP_LernDashboard.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_LernDashboard.Services.DataCreators
{
    internal class DatabaseShortcutCreator : IDataCreator<Shortcut>
    {

        private readonly DashboardDbContextFactory _dbContextFactory;

        public DatabaseShortcutCreator(DashboardDbContextFactory dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }
        public async Task CreateModel(Shortcut model)
        {
            using (DashboardDbContext context = _dbContextFactory.CreateDbContext())
            {
                ShortcutDTO shortcutDTO = ToShortcutDTO(model);
                context.Shortcuts.Add(shortcutDTO);
                await context.SaveChangesAsync();
            }
        }

        public async Task DeleteModel(Shortcut model)
        {
            using (DashboardDbContext context = _dbContextFactory.CreateDbContext())
            {
                ShortcutDTO shortcutDTO = ToShortcutDTO(model);
                context.Shortcuts.Remove(shortcutDTO);
                await context.SaveChangesAsync();
            }
        }

        private ShortcutDTO ToShortcutDTO(Shortcut shortcut)
        {
            return new ShortcutDTO()
            {
                Id = shortcut.Id,
                Path = shortcut.Path,
                Name = shortcut.Name,
                IconPath = shortcut.IconPath,
                Type = shortcut.Type
            };
        }
    }
}
