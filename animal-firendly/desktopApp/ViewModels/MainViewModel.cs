using DesktopApp.Commands;
using DesktopApp.State.Authenticators;
using DesktopApp.State.Navigators;
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

        public MainViewModel(INavigator navigator, IAuthenticator authenticator)
        {
            Navigator = navigator;
            Authenticator = authenticator;

            Navigator.UpdateCurrentViewModelCommand.Execute(ViewType.Login);
        }
    }
}
