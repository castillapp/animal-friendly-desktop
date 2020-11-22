using MockPersistencia.Data;
using Persistencia.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace MockPersistencia.Services
{
    public class LoginService : Persistencia.Services.ILoginService
    {
        public ITreballador FerLogin(string usuari, string password)
        {
            var t = MockDatabase.Treballadors.ListAll().FirstOrDefault(f => f.DNI == usuari);
            if (t == null) return t;
            if (password == "1234")
            {
                return t;
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
