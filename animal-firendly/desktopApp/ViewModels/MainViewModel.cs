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
        public INavigator Navigator { get; set; } = new Navigator();
        public ICommand UpdateViewCommand { get; }
        public Authenticator Authenticator { get; }

        public MainViewModel()
        {
            Authenticator = Bootstrap.Authenticator;
            UpdateViewCommand = new UpdateViewCommand(Navigator, Bootstrap.Authenticator);
            UpdateViewCommand.Execute(ViewType.Login);
        }
    }
}
