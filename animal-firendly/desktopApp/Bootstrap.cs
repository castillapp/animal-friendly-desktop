using DesktopApp.State.Authenticators;
using System;
using System.Collections.Generic;
using System.Text;

namespace DesktopApp
{
    /// <summary>
    /// Classe encarregada de gestionar la injecció de dependencies
    /// </summary>
    public static class Bootstrap
    {
        public static Authenticator Authenticator { get { return authenticator; } }

        private static Authenticator authenticator = new Authenticator();
    }
}
