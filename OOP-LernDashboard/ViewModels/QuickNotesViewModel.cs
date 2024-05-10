using OOP_LernDashboard.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
