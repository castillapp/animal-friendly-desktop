using DesktopApp.State.Authenticators;
using Persistencia.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DesktopApp.ViewModels
{
    public class UsuariWelcomeViewModel : BaseViewModel
    {
        private readonly IAuthenticator authenticator;

        public UsuariWelcomeViewModel(IAuthenticator authenticator)
        {
            this.authenticator = authenticator;
        }

        public string MissatgeBenvinguda { get { return "Benvingut " + authenticator.NomUsuari; } }

        public string MissatgeDefinicioUsuari { get { return CrearMissatgePermisosUsuari(authenticator.TipusUsuari); } }

        private string CrearMissatgePermisosUsuari(TipusTreballador tipusTreballador)
        {
            var res = "\nTens al teu abast les accions que es mostren al menú superior";

            switch (tipusTreballador)
            {
                case TipusTreballador.Administrador:
                    res = "Tens els permisos d'un Administrador" + res;
                    break;
                case TipusTreballador.Veterinari:
                    res = "Tens els permisos d'un Veterinari" + res;
                    break;
                case TipusTreballador.Auxiliar:
                    res = "Tens els permisos d'un Auxiliar" + res;
                    break;
                case TipusTreballador.Voluntari:
                case TipusTreballador.Convidat:
                default:
                    return "Pel tipus d'usuari que ets, no pots fer cap acció amb aquesta aplicació";
            }
            return res;
        }
    }
}
