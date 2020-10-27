using DesktopApp.Modules;
using Persistencia.Models;
using Persistencia.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;

namespace DesktopApp.State.Authenticators
{
    /// <summary>
    /// Interficia que implementa el sistema per autenticar-se
    /// </summary>
    public interface IAuthenticator
    {
        /// <summary>
        /// Fa el login
        /// </summary>
        /// <param name="usuari">usuari</param>
        /// <param name="pass">contrassenya</param>
        /// <returns>Retorna un missatge amb el resultat de l'operació</returns>
        string Login(string usuari, string pass);

        /// <summary>
        /// Indica si l'usuari està loguejat
        /// </summary>
        bool Logejat { get; }

        /// <summary>
        /// Fa el logout
        /// </summary>
        void Logout();
    }

    public class Authenticator : ObservableObject, IAuthenticator
    {
        private readonly ILoginService loginService;
        public Authenticator()
        {
            loginService = new MockPersistencia.Services.LoginService();
        }

        /// <summary>
        /// Tipus de dades Usuari
        /// </summary>
        private IUsuari currentUsuari;

        /// <summary>
        /// S'implementa un ObservableObject que exposa un event al que els altres elements
        /// s'hi poden subscriure per saber si hi ha hagut un canvi.
        /// Quan es fa un login s'actualitza l'usuari resultant
        /// </summary>
        public IUsuari CurrentUsuari
        {
            get { return currentUsuari; }
            private set
            {
                currentUsuari = value;
                OnPropertyChanged(nameof(Logejat));
                OnPropertyChanged(nameof(CurrentUsuari));
            }
        }

        /// <summary>
        /// Si l'usuari es null vol dir que no està loguejat
        /// </summary>
        public bool Logejat { get { return CurrentUsuari != null; } }

        public string Login(string usuari, string pass)
        {
            if(String.IsNullOrWhiteSpace(usuari) || String.IsNullOrWhiteSpace(pass))
            {
                return "L'usuari o contrassenya estan buits";
            }

            CurrentUsuari = loginService.FerLogin(usuari, pass);

            if (Logejat)
            {
                return "Usuari loguejat!";
            }
            else
            {
                return "L'usuari no existeix o la contrassenya no és correcte";
            }
        }

        public void Logout()
        {
            loginService.Logout();
        }
    }
}
