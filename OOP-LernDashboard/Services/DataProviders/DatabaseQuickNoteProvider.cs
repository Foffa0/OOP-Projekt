using Microsoft.EntityFrameworkCore;
using OOP_LernDashboard.DbContexts;
using OOP_LernDashboard.DTOs;
using OOP_LernDashboard.Models;

namespace OOP_LernDashboard.Services.DataProviders
{
    internal class DatabaseQuickNoteProvider : IDataProvider<QuickNote>
    {
        private readonly DashboardDbContextFactory _dbContextFactory;

        public DatabaseQuickNoteProvider(DashboardDbContextFactory dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }
        public async Task<IEnumerable<QuickNote>> GetAllModels()
        {
            using (DashboardDbContext context = _dbContextFactory.CreateDbContext())
            {
                IEnumerable<QuickNoteDTO> quickNotesDTOs = await context.QuickNotes.ToListAsync();

                return quickNotesDTOs.Select(r => ToQuickNote(r));
            }
        }
        private static QuickNote ToQuickNote(QuickNoteDTO r)
        {
            return new QuickNote(r.Id, r.Note);
        }
    }
}
