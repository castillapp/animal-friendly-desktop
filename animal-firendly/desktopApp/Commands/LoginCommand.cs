using DesktopApp.State.Authenticators;
using DesktopApp.State.Navigators;
using DesktopApp.ViewModels;
using Persistencia.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace DesktopApp.Commands
{
    public class LoginCommand : ICommand
    {
        private IAuthenticator autenticador;
        private LoginViewModel loginViewModel;
        private IRenavigator renavigator;

        public LoginCommand(LoginViewModel loginViewModel, IAuthenticator autenticador, IRenavigator renavigator)
        {
            this.autenticador = autenticador;
            this.loginViewModel = loginViewModel;
            this.renavigator = renavigator;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            if (parameter is null) return;

            var result = autenticador.Login(loginViewModel.UserName, parameter?.ToString());
            loginViewModel.LoginMessage = result;

            if (autenticador.Logejat)
            {
                renavigator.Renavigate();
            }
        }
    }
}
