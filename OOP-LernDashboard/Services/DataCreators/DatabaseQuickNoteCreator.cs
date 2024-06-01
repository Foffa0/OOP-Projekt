using OOP_LernDashboard.DbContexts;
using OOP_LernDashboard.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_LernDashboard.Services.DataCreators
{
    internal class DatabaseQuickNoteCreator : IDataCreator<QuickNote>
    {
        private readonly DashboardDbContextFactory _dbContextFactory;

        public DatabaseQuickNoteCreator(DashboardDbContextFactory dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public Task CreateModel(QuickNote model)
        {
            throw new NotImplementedException();
        }

        public Task DeleteModel(QuickNote model)
        {
            throw new NotImplementedException();
        }
    }
}
