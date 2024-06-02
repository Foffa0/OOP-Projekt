using OOP_LernDashboard.Models;
using OOP_LernDashboard.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace OOP_LernDashboard.ViewModels
{
    internal class QuickNoteViewModel : ViewModelBase
    {
        private readonly DashboardStore _dashboardStore;
        private readonly QuickNote _quickNote;
        public QuickNote QuickNote => _quickNote;

        public string? Note => _quickNote.Note;
        public Guid Id => _quickNote.Id;

        public QuickNoteViewModel(QuickNote quickNote, DashboardStore dashboardStore)
        {
            _quickNote = quickNote;
            _dashboardStore = dashboardStore;
        }
    }
}
