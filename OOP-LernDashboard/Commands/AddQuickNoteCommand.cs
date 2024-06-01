using OOP_LernDashboard.Models;
using OOP_LernDashboard.Stores;
using OOP_LernDashboard.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_LernDashboard.Commands
{
    internal class AddQuickNoteCommand : CommandBase
    {
        private DashboardStore _dashboardStore;
        private QuickNotesViewModel _quickNotesViewModel;

        public AddQuickNoteCommand(DashboardStore dashboardStore, QuickNotesViewModel quickNotesViewModel)
        {
            _dashboardStore = dashboardStore;
            _quickNotesViewModel = quickNotesViewModel;
        }

        
        public override async void Execute(object? parameter)
        {
            QuickNote quickNote = new QuickNote(_quickNotesViewModel.Note);

            await _dashboardStore.AddQuickNote(quickNote);
        }
    }
}
