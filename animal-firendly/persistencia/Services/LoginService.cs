using Persistencia.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Persistencia.Services
{
    public interface ILoginService : IBaseService
    {
        /// <summary>
        /// Fa el login de l'usuari
        /// </summary>
        /// <param name="usuari">nom de l'usuari</param>
        /// <param name="password">contrassenya de l'usuari</param>
        /// <returns>Informació de l'usuari, null si no existeix</returns>
        ITreballador FerLogin(string usuari, string password);

        /// <summary>
        /// Fa el LogOut de l'usuari
        /// </summary>
        void Logout();
    }

    public class LoginService : BaseService, ILoginService
    {
        public ITreballador FerLogin(string usuari, string password)
        {
            throw new NotImplementedException();
        }

        public void Logout()
        {
            throw new NotImplementedException();
        }
    }
}
