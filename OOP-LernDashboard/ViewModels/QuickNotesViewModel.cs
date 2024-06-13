using OOP_LernDashboard.Commands;
using OOP_LernDashboard.Models;
using OOP_LernDashboard.Stores;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace OOP_LernDashboard.ViewModels
{
    internal class QuickNotesViewModel : ViewModelBase, INoteViewModel
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

        public ICommand AddQuickNoteCommand { get; }
        public ICommand LoadDataAsyncCommand { get; }

        public QuickNotesViewModel(DashboardStore dashboardStore)
        {
            _quickNotes = new ObservableCollection<QuickNoteViewModel>();

            _dashboardStore = dashboardStore;
            this.AddQuickNoteCommand = new AddQuickNoteCommand(this, _dashboardStore);
            LoadDataAsyncCommand = new LoadQuickNotesCommand(this, _dashboardStore);

            _dashboardStore.QuickNoteCreated += OnQuickNoteCreated;
            _dashboardStore.QuickNoteDeleted += OnQuickNoteDeleted;
        }

        public static QuickNotesViewModel LoadViewModel(DashboardStore dashboardStore)
        {
            QuickNotesViewModel quickNotesViewModel = new QuickNotesViewModel(dashboardStore);
            quickNotesViewModel.LoadDataAsyncCommand.Execute(null);
            return quickNotesViewModel;
        }

        public void UpdateQuickNotes(IEnumerable<QuickNote> quickNotes)
        {
            _quickNotes.Clear();
            foreach (var quickNote in quickNotes)
            {
                _quickNotes.Add(new QuickNoteViewModel(quickNote, _dashboardStore));
            }
        }

        public void OnQuickNoteCreated(QuickNote quickNote)
        {
            QuickNoteViewModel quickNoteViewModel = new QuickNoteViewModel(quickNote, _dashboardStore);
            _quickNotes.Add(quickNoteViewModel);
        }

        public void OnQuickNoteDeleted(QuickNote quickNote)
        {
            var q = _quickNotes.First(q => q.Id == quickNote.Id);
            if (q != null)
            {
                _quickNotes.Remove(q);
            }
        }
    }
}
