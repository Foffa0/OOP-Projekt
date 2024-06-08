using OOP_LernDashboard.Models;
using OOP_LernDashboard.ViewModels;
using System.Diagnostics;

namespace OOP_LernDashboard.Commands
{
    internal class OpenShortcutCommand : CommandBase
    {
        private readonly ShortcutViewModel _shortcutviewmodel;

        public OpenShortcutCommand(ShortcutViewModel shortcutviewmodel)
        {
            _shortcutviewmodel = shortcutviewmodel;
        }

        public override void Execute(object? parameter)
        {
            if (_shortcutviewmodel.Type == ShortcutType.Link)
            {
                System.Diagnostics.Process.Start(new ProcessStartInfo { FileName = _shortcutviewmodel.Path, UseShellExecute = true });
            }
            else
            {
                Process.Start(_shortcutviewmodel.Path);
            }
        }
    }
}
