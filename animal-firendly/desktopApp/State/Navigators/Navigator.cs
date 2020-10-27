using DesktopApp.Commands;
using DesktopApp.Modules;
using DesktopApp.ViewModels;
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
    }

    public enum ViewType
    {
        Login,
        Welcome,
        Exit
    }

    public class Navigator : ObservableObject, INavigator
    {
        private BaseViewModel currentViewModel;

        public BaseViewModel CurrentViewModel
        {
            get { return currentViewModel; }
            set { currentViewModel = value; OnPropertyChanged(nameof(CurrentViewModel)); }
        }
    }
}
