using Persistencia.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Persistencia.Services
{
    public interface ILoginService
    {
        /// <summary>
        /// Fa el login de l'usuari
        /// </summary>
        /// <param name="usuari">nom de l'usuari</param>
        /// <param name="password">contrassenya de l'usuari</param>
        /// <returns>Informació de l'usuari, null si no existeix</returns>
        IUsuari FerLogin(string usuari, string password);
    }

    public class LoginService : ILoginService
    {
        public IUsuari FerLogin(string usuari, string password)
        {
            throw new NotImplementedException();
        }
    }
}
