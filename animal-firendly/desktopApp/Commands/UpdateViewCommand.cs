using DesktopApp.State.Authenticators;
using DesktopApp.State.Navigators;
using DesktopApp.ViewModels;
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

        //private MainViewModel viewModel;
        private INavigator navigator;
        private IAuthenticator authenticator;

        public UpdateViewCommand(INavigator navigator, IAuthenticator authenticator)
        {
            //this.viewModel = viewModel;
            this.navigator = navigator;
            this.authenticator = authenticator;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            if(parameter is ViewType)
            {
                ViewType view = (ViewType)parameter;
                switch (view)
                {
                    case ViewType.Login:
                        navigator.CurrentViewModel = new LoginViewModel();
                        break;
                    case ViewType.Welcome:
                        navigator.CurrentViewModel = new UsuariWelcomeViewModel();
                        break;
                    case ViewType.Exit:
                        authenticator.Logout();
                        System.Windows.Application.Current.Shutdown();
                        break;
                    default:
                        break;
                }
            }

            //if(parameter.ToString() == "Login")
            //{
            //    viewModel.SelectedViewModel = new LoginViewModel();
            //}else if(parameter.ToString() == "Usuari")
            //{
            //    viewModel.SelectedViewModel = new LoginViewModel();
            //}
        }
    }
}
