using OOP_LernDashboard.Commands;
using OOP_LernDashboard.Models;
using OOP_LernDashboard.Stores;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace OOP_LernDashboard.ViewModels
{
    internal class QuickNotesViewModel : ViewModelBase
    {
        private readonly ObservableCollection<QuickNoteViewModel> _quickNotes;
        public IEnumerable<QuickNoteViewModel> QuickNotes => _quickNotes;

        private readonly DashboardStore _dashboardStore;

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

        public QuickNotesViewModel(DashboardStore dashboardStore)
        {
            _quickNotes = new ObservableCollection<QuickNoteViewModel>();
            _quickNotes.Add(new QuickNoteViewModel(new QuickNote("sdafjhlöasdlkökjfhölsdalkjfdlkösa")));
            _quickNotes.Add(new QuickNoteViewModel(new QuickNote("sdafjhlöasdlkökjssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssfhölsdalkjfdlkösa")));
            _quickNotes.Add(new QuickNoteViewModel(new QuickNote("sdafjasdfffffffdddddddddddddddddddddddddddddddddddddddddddddddddddddddddhlöasdlkökjfhölsdalkjfdlkösa")));

            _dashboardStore = dashboardStore;
            this.AddQuickNoteCommand = new AddQuickNoteCommand(_dashboardStore, this);
        }

        public static QuickNotesViewModel LoadViewModel(DashboardStore dashboardStore)
        {
            QuickNotesViewModel viewModel = new QuickNotesViewModel(dashboardStore);
            return viewModel;
        }
    }
}
