using Persistencia.Connections;
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
        public LoginService(IServerConnection connexio, IInterpretORM interpretORM) : base(connexio, interpretORM)
        {

        }

        public ITreballador FerLogin(string usuari, string password)
        {
            var res = Connexio.SendRequest("login:" + usuari + ":" + password);
            if (res.Contains("validat"))
            {

            }
            return null;
        }

        public void Logout()
        {
            Connexio.Disconnect();
        }
    }
}
