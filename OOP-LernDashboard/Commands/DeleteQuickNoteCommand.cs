using HandyControl.Data;
using OOP_LernDashboard.Models;
using OOP_LernDashboard.Stores;
using System.Windows;

namespace OOP_LernDashboard.Commands
{
    internal class DeleteQuickNoteCommand : CommandBase
    {
        private QuickNote _quickNote;
        private DashboardStore _dashboardStore;

        public DeleteQuickNoteCommand(QuickNote quickNote, DashboardStore dashboardStore)
        {
            _quickNote = quickNote;
            _dashboardStore = dashboardStore;
        }

        public override async void Execute(object? parameter)
        {
            string notePreview = _quickNote.Note.Length > 10 ? _quickNote.Note.Substring(0, 10) + "..." : _quickNote.Note;
            MessageBoxResult result = HandyControl.Controls.MessageBox.Show(new HandyControl.Data.MessageBoxInfo
            {
                Message = $"Möchtest du diese Notiz ({notePreview}) wirklich löschen?",
                Button = MessageBoxButton.YesNo,
                //ConfirmContent = "Ja",
                YesContent = "Ja",
                NoContent = "Nein",
                IconKey = ResourceToken.AskGeometry,
                IconBrushKey = "PrimaryBrush",
            }); ;
            if (result == MessageBoxResult.Yes)
            {
                await _dashboardStore.DeleteQuickNote(_quickNote);
            }
        }
    }
}
