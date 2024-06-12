using HandyControl.Controls;
using HandyControl.Tools.Extension;
using OOP_LernDashboard.Models;
using OOP_LernDashboard.Stores;
using OOP_LernDashboard.ViewModels;

namespace OOP_LernDashboard.Commands
{
    interface INoteViewModel
    {
        string Note { get; set; }
    }
    internal class AddQuickNoteCommand : CommandBase
    {
        private DashboardStore _dashboardStore;
        private INoteViewModel _viewModel;

        public AddQuickNoteCommand(INoteViewModel viewModel, DashboardStore dashboardStore)
        {
            _dashboardStore = dashboardStore;
            _viewModel = viewModel;
        }

        public override async void Execute(object? parameter)
        {
            
            if (!_viewModel.Note.IsNullOrEmpty())
            {
                QuickNote quickNote = new QuickNote(_viewModel.Note);
                _viewModel.Note = "";
                await _dashboardStore.AddQuickNote(quickNote);
            }
            
        }
    }
}
