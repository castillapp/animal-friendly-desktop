using DesktopApp.Commands;
using DesktopApp.State.Authenticators;
using DesktopApp.State.Navigators;
using MockPersistencia.Services;
using Persistencia.Models;
using Persistencia.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace DesktopApp.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        public Usuari Usuari { get; set; }

        public LoginViewModel()
        {
            Authenticator loginService = Bootstrap.Authenticator;
            Navigator navigator = new Navigator();
            LoginCommand = new LoginCommand(this, loginService,navigator);
        }

        public string UserName { get; set; }

        public ICommand LoginCommand { get; }

        private string loginMessage;

        public string LoginMessage
        {
            get { return loginMessage; }
            set { loginMessage = value; OnPropertyChanged(nameof(LoginMessage)); }
        }
    }
}
