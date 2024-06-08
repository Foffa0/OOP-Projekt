using HandyControl.Data;
using OOP_LernDashboard.Models;
using OOP_LernDashboard.Stores;
using System.Windows;

namespace OOP_LernDashboard.Commands
{
    internal class DeleteShortcutCommand : CommandBase
    {
        private readonly Shortcut _shortcut;
        private readonly DashboardStore _dashboardStore;

        public DeleteShortcutCommand(Shortcut shortcut, DashboardStore dashboardStore)
        {
            _shortcut = shortcut;
            _dashboardStore = dashboardStore;
        }

        public override void Execute(object? parameter)
        {
            MessageBoxResult result = HandyControl.Controls.MessageBox.Show(new HandyControl.Data.MessageBoxInfo
            {
                Message = $"Möchtest du diesen Shortcut ({_shortcut.Name}) wirklich löschen?",
                Button = MessageBoxButton.YesNo,
                ConfirmContent = "Ja",
                NoContent = "Nein",
                IconKey = ResourceToken.AskGeometry,
                IconBrushKey = "PrimaryBrush",
            });

            if (result == MessageBoxResult.Yes)
            {
                _ = _dashboardStore.DeleteShortcut(_shortcut);
            }
        }
    }
}
