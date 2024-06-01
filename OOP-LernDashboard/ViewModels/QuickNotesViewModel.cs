using OOP_LernDashboard.Models;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace OOP_LernDashboard.ViewModels
{
    internal class QuickNotesViewModel : ViewModelBase
    {
        private readonly ObservableCollection<QuickNoteViewModel> _quickNotes;
        public IEnumerable<QuickNoteViewModel> QuickNotes => _quickNotes;

        private string _note = "";
        public string Note
        {
            get { return _note; }
            set
            {
                _note = value;
                OnPropertyChanged(nameof(Note));
            }
        }

        public ICommand AddQuickNoteCommand;
        public ICommand DeleteQuickNoteCommand;

        public QuickNotesViewModel()
        {
            _quickNotes = new ObservableCollection<QuickNoteViewModel>();
            _quickNotes.Add(new QuickNoteViewModel(new QuickNote("sdafjhlöasdlkökjfhölsdalkjfdlkösa")));
            _quickNotes.Add(new QuickNoteViewModel(new QuickNote("sdafjhlöasdlkökjssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssfhölsdalkjfdlkösa")));
            _quickNotes.Add(new QuickNoteViewModel(new QuickNote("sdafjasdfffffffdddddddddddddddddddddddddddddddddddddddddddddddddddddddddhlöasdlkökjfhölsdalkjfdlkösa")));
        }

        public static QuickNotesViewModel LoadViewModel()
        {
            QuickNotesViewModel viewModel = new QuickNotesViewModel();
            return viewModel;
        }
    }
}
