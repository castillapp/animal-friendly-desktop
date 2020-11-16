using DesktopApp.Commands;
using DesktopApp.State.Authenticators;
using DesktopApp.State.Navigators;
using DesktopApp.ViewModels.Factories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Windows.Input;

namespace DesktopApp.ViewModels
{
    public class MainViewModel: BaseViewModel
    {
        public INavigator Navigator { get; set; }
        public IAuthenticator Authenticator { get; }
        public ICommand UpdateCurrentViewModelCommand { get; }

        public MainViewModel(INavigator navigator, IAuthenticator authenticator, IRootViewModelFactory viewModelFactory)
        {
            Navigator = navigator;
            Authenticator = authenticator;

            UpdateCurrentViewModelCommand = new UpdateViewCommand(navigator, viewModelFactory);
            UpdateCurrentViewModelCommand.Execute(ViewType.Login);
        }
    }
}
