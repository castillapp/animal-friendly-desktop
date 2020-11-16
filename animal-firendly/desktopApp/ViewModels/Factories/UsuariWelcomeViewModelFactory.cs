using System;
using System.Collections.Generic;
using System.Text;

namespace DesktopApp.ViewModels.Factories
{
    public class UsuariWelcomeViewModelFactory : IViewModelFactory<UsuariWelcomeViewModel>
    {
        public UsuariWelcomeViewModel CreateViewModel()
        {
            return new UsuariWelcomeViewModel();
        }
    }
}
