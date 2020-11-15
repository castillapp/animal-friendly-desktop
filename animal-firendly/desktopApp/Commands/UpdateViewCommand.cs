using DesktopApp.State.Authenticators;
using DesktopApp.State.Navigators;
using DesktopApp.ViewModels;
using DesktopApp.ViewModels.Factories;
using DesktopApp.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace DesktopApp.Commands
{
    public class UpdateViewCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        private readonly INavigator navigator;
        private readonly IRootViewModelFactory viewModelFactory;

        public UpdateViewCommand(INavigator navigator, IRootViewModelFactory viewModelFactory)
        {
            this.navigator = navigator;
            this.viewModelFactory = viewModelFactory;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            if(parameter is ViewType)
            {
                if (parameter is ViewType)
                {
                    ViewType viewType = (ViewType)parameter;

                    navigator.CurrentViewModel = viewModelFactory.CreateViewModel(viewType);
                }
            }
        }
    }
}
