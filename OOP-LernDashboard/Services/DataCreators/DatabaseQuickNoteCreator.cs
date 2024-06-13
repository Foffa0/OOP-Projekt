using OOP_LernDashboard.DbContexts;
using OOP_LernDashboard.DTOs;
using OOP_LernDashboard.Models;

namespace OOP_LernDashboard.Services.DataCreators
{
    internal class DatabaseQuickNoteCreator : IDataCreator<QuickNote>
    {
        private readonly DashboardDbContextFactory _dbContextFactory;

        public DatabaseQuickNoteCreator(DashboardDbContextFactory dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public async Task CreateModel(QuickNote model)
        {
            using (DashboardDbContext context = _dbContextFactory.CreateDbContext())
            {
                QuickNoteDTO quickNoteDTO = ToQuickNoteDTO(model);

                context.QuickNotes.Add(quickNoteDTO);
                await context.SaveChangesAsync();
            }
        }

        public async Task DeleteModel(QuickNote model)
        {
            using (DashboardDbContext context = _dbContextFactory.CreateDbContext())
            {
                QuickNoteDTO quickNoteDTO = ToQuickNoteDTO(model);

                context.QuickNotes.Remove(quickNoteDTO);
                await context.SaveChangesAsync();
            }
        }

        private QuickNoteDTO ToQuickNoteDTO(QuickNote quickNote)
        {
            return new QuickNoteDTO
            {
                Id = quickNote.Id,
                Note = quickNote.Note,
                CurrentDateTime = quickNote.CurrentDateTime
            };
        }
    }
}
