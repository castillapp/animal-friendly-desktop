﻿using DesktopApp.Commands;
using DesktopApp.Modules;
using DesktopApp.ViewModels;
using DesktopApp.ViewModels.Factories;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;

namespace DesktopApp.State.Navigators
{
    public interface INavigator : INotifyPropertyChanged
    {
        BaseViewModel CurrentViewModel { get; set;  }
        ICommand UpdateCurrentViewModelCommand { get; }
    }

    public class Navigator : ObservableObject, INavigator
    {
        private BaseViewModel currentViewModel;

        public BaseViewModel CurrentViewModel
        {
            get { return currentViewModel; }
            set { currentViewModel = value; OnPropertyChanged(nameof(CurrentViewModel)); }
        }

        public ICommand UpdateCurrentViewModelCommand { get; set; }

        public Navigator(IRootViewModelFactory viewModelFactory)
        {
            UpdateCurrentViewModelCommand = new UpdateViewCommand(this, viewModelFactory);
        }
    }
}