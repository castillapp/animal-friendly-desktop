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
        private readonly IAdministrarTreballadorsService treballadorsService;

        public LoginService(IServerConnection connexio, IAdministrarTreballadorsService treballadorsService, IInterpretORM interpretORM) : base(connexio, interpretORM)
        {
            this.treballadorsService = treballadorsService;
        }

        public ITreballador FerLogin(string usuari, string password)
        {
            var res = Connexio.SendRequest("login:" + usuari + ":" + password);
            if (res.Contains("validat"))
            {
                return treballadorsService.GetTreballador(usuari);
            }
            return null;
        }

        public void Logout()
        {
            Connexio.Disconnect();
        }
    }
}
