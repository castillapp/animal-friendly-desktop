using DesktopApp.State.Authenticators;
using DesktopApp.State.Navigators;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Text;

namespace DesktopApp.ViewModels.Factories
{
    public class LoginViewModelFactory : IViewModelFactory<LoginViewModel>
    {
        private readonly IAuthenticator authenticator;

        public LoginViewModelFactory(IAuthenticator authenticator)
        {
            this.authenticator = authenticator;
        }

        public LoginViewModel CreateViewModel()
        {
            return new LoginViewModel(authenticator);
        }
    }
}

