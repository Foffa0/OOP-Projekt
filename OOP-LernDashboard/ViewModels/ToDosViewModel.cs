﻿using OOP_LernDashboard.Commands;
using OOP_LernDashboard.Models;
using OOP_LernDashboard.Stores;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace OOP_LernDashboard.ViewModels
{
    internal class ToDosViewModel : ViewModelBase
    {
        private readonly DashboardStore _dashboardStore;

        private readonly ObservableCollection<ToDoViewModel> _toDos;
        private readonly ObservableCollection<ToDoViewModel> _checkedtoDos;
        public IEnumerable<ToDoViewModel> ToDos => _toDos;
        public IEnumerable<ToDoViewModel> CheckedToDos => _checkedtoDos;
        private string _toDoDesc = "";
        public string ToDoDesc
        {
            get { return _toDoDesc; }
            set
            {
                _toDoDesc = value;
                OnPropertyChanged(nameof(ToDoDesc));
            }
        }
        private bool _isRecurringToDo = false;
        public bool IsRecurringToDo
        {
            get { return _isRecurringToDo; }
            set
            {
                _isRecurringToDo = value;
                OnPropertyChanged(nameof(IsRecurringToDo));
            }
        }
        private bool _startTimeIsNow = false;
        public bool StartTimeIsNow
        {
            get { return _startTimeIsNow; }
            set
            {
                _startTimeIsNow = value;
                OnPropertyChanged(nameof(StartTimeIsNow));
            }
        }
        private bool _isChecked = false;
        public bool IsChecked
        {
            get { return _isChecked; }
            set
            {
                _isChecked = value;
                OnPropertyChanged(nameof(IsChecked));
            }
        }
        private DateTime _newToDoStartTime = DateTime.Now;
        public DateTime NewToDoStartTime
        {
            get { return _newToDoStartTime; }
            set
            {
                _newToDoStartTime = value;
                OnPropertyChanged(nameof(NewToDoStartTime));
            }
        }
        private DateTime _newToDoDate = DateTime.Today;
        public DateTime NewToDoDate
        {
            get { return _newToDoDate; }
            set
            {
                _newToDoDate = value;
                OnPropertyChanged(nameof(NewToDoDate));
            }
        }
        private int _intervalHours;
        public int IntervalHours
        {
            get => _intervalHours;
            set
            {
                _intervalHours = value;
                OnPropertyChanged(nameof(IntervalHours));

            }
        }
        private int _intervalDays;
        public int IntervalDays
        {
            get => _intervalDays;
            set
            {
                _intervalDays = value;
                OnPropertyChanged(nameof(IntervalDays));

            }
        }
        private int _intervalMonths;
        public int IntervalMonths
        {
            get => _intervalMonths;
            set
            {
                _intervalMonths = value;
                OnPropertyChanged(nameof(IntervalMonths));

            }
        }
        private int _intervalYears;
        public int IntervalYears
        {
            get => _intervalYears;
            set
            {
                _intervalYears = value;
                OnPropertyChanged(nameof(IntervalYears));

            }
        }

        public ICommand LoadDataAsyncCommand { get; }
        public ICommand AddToDoCommand { get; }
        public ToDosViewModel(Dashboard dashboard, DashboardStore dashboardStore)
        {
            _dashboardStore = dashboardStore;
            LoadDataAsyncCommand = new LoadToDosCommand(this, dashboardStore);
            this.AddToDoCommand = new AddToDoCommand(this, dashboardStore);


            _toDos = new ObservableCollection<ToDoViewModel>();
            _checkedtoDos = new ObservableCollection<ToDoViewModel>();


            _dashboardStore.ToDoDeleted += OnToDoDeleted;
            _dashboardStore.ToDoCreated += OnToDoCreated;
            _dashboardStore.ToDoChecked += OnToDoChecked;
        }
        public void UpdateToDos(IEnumerable<ToDo> todos)
        {
            _toDos.Clear();

            foreach (var toDo in todos)
            {
                if (toDo is RecurringToDo)
                {
                    _checkedtoDos.Add(new ToDoViewModel(toDo, _dashboardStore));
                    _toDos.Add(new ToDoViewModel(toDo, _dashboardStore));
                    continue;
                }
                if (toDo.IsChecked)
                {
                    _checkedtoDos.Add(new ToDoViewModel(toDo, _dashboardStore));
                }
                else
                {
                    _toDos.Add(new ToDoViewModel(toDo, _dashboardStore));
                }

            }
        }
        public static ToDosViewModel LoadViewModel(Dashboard dashboard, DashboardStore dashboardStore)
        {
            ToDosViewModel viewModel = new ToDosViewModel(dashboard, dashboardStore);
            viewModel.LoadDataAsyncCommand.Execute(null);
            return viewModel;
        }
        private void OnToDoCreated(ToDo toDo)
        {
            ToDoViewModel toDoViewModel = new ToDoViewModel(toDo, _dashboardStore);
            if (toDo is RecurringToDo)
            {
                _checkedtoDos.Add(toDoViewModel);
                _toDos.Add(toDoViewModel);
                return;
            }
            if (!toDo.IsChecked)
            {
                _toDos.Add(toDoViewModel);
            }
            else
                _checkedtoDos.Add(toDoViewModel);
        }
        private void OnToDoChecked(ToDo toDo)
        {
            ToDoViewModel toDoViewModel = new ToDoViewModel(toDo, _dashboardStore);
            _checkedtoDos.Add(toDoViewModel);
            var s = _toDos.FirstOrDefault(s => s.ToDo.Id == toDo.Id);
            if (s != default)
            {
                _toDos.Remove(s);
            }
        }
        private void OnToDoDeleted(ToDo toDo)
        {
            var s = _toDos.FirstOrDefault(s => s.ToDo.Id == toDo.Id);
            if (s != default)
            {
                _toDos.Remove(s);
            }

            s = _checkedtoDos.FirstOrDefault(s => s.ToDo.Id == toDo.Id);
            if (s != default)
            {
                _checkedtoDos.Remove(s);
            }
        }
    }
}
