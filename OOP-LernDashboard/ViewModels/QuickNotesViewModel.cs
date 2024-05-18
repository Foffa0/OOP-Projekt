using OOP_LernDashboard.Models;

namespace OOP_LernDashboard.ViewModels
{
    internal class QuickNotesViewModel : ViewModelBase
    {
        private QuickNote _quickNote { get; }

        public string? Note => _quickNote.Note;

        public static QuickNotesViewModel LoadViewModel()
        {
            QuickNotesViewModel viewModel = new QuickNotesViewModel();
            return viewModel;
        }
    }
}
