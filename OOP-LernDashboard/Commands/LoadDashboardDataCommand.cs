﻿using OOP_LernDashboard.Stores;
using OOP_LernDashboard.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_LernDashboard.Commands
{
    /// <summary>
    /// When executed loads the data required by the dashboard viewmodel
    /// </summary>
    internal class LoadDashboardDataCommand : AsyncCommandBase
    {
        private readonly DashboardViewModel _viewModel;
        private readonly DashboardStore _dashboardStore;

        public LoadDashboardDataCommand(DashboardViewModel viewModel, DashboardStore dashboardStore)
        {
            _viewModel = viewModel;
            _dashboardStore = dashboardStore;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            //try
            //{
                await _dashboardStore.Load();
                _viewModel.UpdateToDos(_dashboardStore.ToDos);
            //}
            //catch (Exception ex)
            //{
            //    throw new Exception("Failed to load ToDos");
            //}
        }
    }
}