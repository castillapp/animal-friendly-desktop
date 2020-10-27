using Persistencia.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MockPersistencia.Services
{
    public class LoginService : Persistencia.Services.ILoginService
    {
        public IUsuari FerLogin(string usuari, string password)
        {
            if(usuari == "test" && password == "1234")
            {
                return new Usuari("test", 1);
            }
            return null;
        }

        public bool LogOutFet { get; set; } = false;

        public void Logout()
        {
            LogOutFet = true;
        }
    }
}
