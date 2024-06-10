using OOP_LernDashboard.Models;
using OOP_LernDashboard.Stores;
using OOP_LernDashboard.ViewModels;
using System.ComponentModel;

namespace OOP_LernDashboard.Commands
{
    internal class AddToDoCommand : CommandBase
    {
        private readonly ToDosViewModel _toDosViewModel;
        private readonly DashboardStore _dashboardStore;

        public AddToDoCommand(ToDosViewModel toDosViewModel, DashboardStore dashboardStore)
        {
            _toDosViewModel = toDosViewModel;
            _dashboardStore = dashboardStore;

            _toDosViewModel.PropertyChanged += OnViewModelPropertyChanged;
        }

        public override bool CanExecute(object? parameter)
        {
            return _toDosViewModel.ToDoDesc != null;
        }

        public override async void Execute(object? parameter)
        {
            //if (_dashboardViewModel.ToDoDesc == null)
            //    throw new NullReferenceException("Cannot create ToDo with Null as description");
            DateTime? starttime= _toDosViewModel.IsRecurringToDo ? _toDosViewModel.NewToDoStartTime : null;
            TimeSpan? timeInterval = TimeSpan.FromHours(_toDosViewModel.IntervalHours);
            if(_toDosViewModel.IsRecurringToDo)
            {
                if (!starttime.HasValue || !timeInterval.HasValue) { throw new ArgumentNullException(); }
                
                RecurringToDo toDo = new RecurringToDo(_toDosViewModel.ToDoDesc, _toDosViewModel.IsChecked, (DateTime)starttime, (TimeSpan)timeInterval);
                await _dashboardStore.AddToDo(toDo);
                
            }
            else
            {
                ToDo toDo = new ToDo(_toDosViewModel.ToDoDesc);
                await _dashboardStore.AddToDo(toDo);
            }
                  
        }

        private void OnViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(DashboardViewModel.ToDoDesc))
            {
                OnCanExecutedChanged();
            }
        }
    }
}
