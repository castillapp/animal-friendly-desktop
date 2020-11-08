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
        private readonly INavigator navigator;

        public LoginViewModelFactory(IAuthenticator authenticator,
            INavigator navigator)
        {
            this.authenticator = authenticator;
            this.navigator = navigator;
        }

        public LoginViewModel CreateViewModel()
        {
            return new LoginViewModel(authenticator,navigator);
        }
    }
}

