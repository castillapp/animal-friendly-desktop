﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace DesktopApp.Commands
{
    //public class ExitAppCommand : ICommand
    //{
    //    private IAuthenticator autenticador;
    //    private LoginViewModel loginViewModel;
    //    private INavigator navigator;

    //    public LoginCommand(LoginViewModel loginViewModel, IAuthenticator autenticador, INavigator navigator)
    //    {
    //        this.autenticador = autenticador;
    //        this.loginViewModel = loginViewModel;
    //        this.navigator = navigator;
    //    }

    //    public event EventHandler CanExecuteChanged;

    //    public bool CanExecute(object parameter)
    //    {
    //        return true;
    //    }

    //    public void Execute(object parameter)
    //    {
    //        if (parameter is null) return;

    //        var result = autenticador.Login(loginViewModel.UserName, parameter?.ToString());
    //        loginViewModel.LoginMessage = result;

    //        if (autenticador.Logejat)
    //        {
    //            navigator.CurrentViewModel = new UsuariWelcomeViewModel();
    //        }
    //    }
    //}
}
