using OOP_LernDashboard.Commands;
using OOP_LernDashboard.Models;
using OOP_LernDashboard.Stores;
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
        public string CurrentDateTime
        {
            get
            {
                if (_quickNote.CurrentDateTime.Date == DateTime.Now.Date)
                {
                    return _quickNote.CurrentDateTime.ToString("H:mm");
                }
                else
                {
                    return _quickNote.CurrentDateTime.ToString("dd.MM.yyyy H:mm");
                }
            }
        }

        public ICommand DeleteQuickNoteCommand { get; }
        public QuickNoteViewModel(QuickNote quickNote, DashboardStore dashboardStore)
        {
            _quickNote = quickNote;
            _dashboardStore = dashboardStore;

            DeleteQuickNoteCommand = new DeleteQuickNoteCommand(_quickNote, dashboardStore);
        }
    }
}
