using DesktopApp.State.Authenticators;
using System;
using System.Collections.Generic;
using System.Text;

namespace DesktopApp.ViewModels.Factories
{
    public class UsuariWelcomeViewModelFactory : IViewModelFactory<UsuariWelcomeViewModel>
    {
        private readonly IAuthenticator authenticator;

        public UsuariWelcomeViewModel CreateViewModel()
        {
            return new UsuariWelcomeViewModel(authenticator);
        }

        public UsuariWelcomeViewModelFactory(IAuthenticator authenticator)
        {
            this.authenticator = authenticator;
        }
    }
}
