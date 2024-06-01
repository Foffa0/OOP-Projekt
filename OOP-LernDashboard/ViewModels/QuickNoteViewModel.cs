using OOP_LernDashboard.Models;
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
        
        private QuickNote _quickNote { get; }

        public string? Note => _quickNote.Note;

        public QuickNoteViewModel(QuickNote quickNote)
        {
            _quickNote = quickNote;
        }
    }
}
