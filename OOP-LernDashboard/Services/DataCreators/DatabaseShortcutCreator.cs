using OOP_LernDashboard.DbContexts;
using OOP_LernDashboard.DTOs;
using OOP_LernDashboard.Models;

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

                // check if name is unique
                if (context.Shortcuts.Any(s => s.Name == model.Name))
                {
                    throw new Exception("Shortcut with this name already exists");
                }

                context.Shortcuts.Add(shortcutDTO);
                await context.SaveChangesAsync();
            }
        }

        public async Task ModifyModel(Shortcut model)
        {
            using (DashboardDbContext context = _dbContextFactory.CreateDbContext())
            {
                ShortcutDTO shortcutDTO = ToShortcutDTO(model);
                // sets the shortcutDTO with the same id as the model to the new values
                ShortcutDTO? existingShortcut = await context.Shortcuts.FindAsync(model.Id);

                if (existingShortcut == null)
                {
                    throw new Exception("Shortcut not found");
                }

                // check if name is unique
                if (context.Shortcuts.Any(s => s.Name == model.Name && s.Id != model.Id))
                {
                    throw new Exception("Shortcut with this name already exists");
                }

                // Update the properties of the existing ShortcutDTO with the new values
                existingShortcut.Name = model.Name;
                existingShortcut.Path = model.Path;

                // Save the changes to the database
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
