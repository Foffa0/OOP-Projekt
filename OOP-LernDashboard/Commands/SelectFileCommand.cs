using Microsoft.Win32;
using OOP_LernDashboard.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_LernDashboard.Commands
{
    internal class SelectFileCommand : CommandBase
    {
        private readonly ShortcutsViewModel _shortcutsViewModel;

        public SelectFileCommand(ShortcutsViewModel shortcutsViewModel)
        {
            _shortcutsViewModel = shortcutsViewModel;
        }

        public override void Execute(object? parameter)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            _shortcutsViewModel.NewShortcutPath = openFileDialog.ShowDialog() == true ? openFileDialog.FileName : _shortcutsViewModel.NewShortcutPath;
        }
    }
}
