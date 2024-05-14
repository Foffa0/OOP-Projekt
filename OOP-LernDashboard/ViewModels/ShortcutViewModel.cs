using OOP_LernDashboard.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_LernDashboard.ViewModels
{
    internal class ShortcutViewModel
    {
        private readonly Shortcut _shortcut;

        public ShortcutType Type => _shortcut.Type;
        public string Path => _shortcut.Path;
        public string? Name => _shortcut.Name;
        public string? IconPath => _shortcut.IconPath;

        public ShortcutViewModel(Shortcut shortcut)
        {
            _shortcut = shortcut;
        }
    }
}
