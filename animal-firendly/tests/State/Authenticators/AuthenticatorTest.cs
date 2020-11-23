using Microsoft.VisualStudio.TestTools.UnitTesting;
using DesktopApp.State.Authenticators;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace DesktopAppTests.State.Authenticators
{
    [TestClass]
    public class AuthenticatorTest
    {
        [TestMethod]
        public void Login_CredencialsIncorrectes_ReturnsCredencialsIncorrectes()
        {
            var authenticator = Bootstrap.GetAuthenticator();

            //Usuari que no existeix
            var missatge = authenticator.Login("1F", "Incorrecte");
            Assert.AreEqual(missatge, "L'usuari no existeix o la contrassenya no és correcte");
            Assert.IsTrue(!authenticator.Logejat);

            //Password incorrecte
            missatge = authenticator.Login("1F", "12345");
            Assert.AreEqual(missatge, "L'usuari no existeix o la contrassenya no és correcte");
            Assert.IsTrue(!authenticator.Logejat);
        }

        [TestMethod]
        public void Login_CredencialsCorrectes_ReturnsTrue()
        {
            var authenticator = Bootstrap.GetAuthenticator();

            var missatge = authenticator.Login("1F", "1234");
            Assert.AreEqual(missatge, "Usuari loguejat!");
            Assert.IsTrue(authenticator.Logejat);
        }

        [TestMethod]
        public void Login_CredencialsBuides_ReturnsCredencialsBuides()
        {
            var authenticator = Bootstrap.GetAuthenticator();

            //Usuari buit
            var missatge = authenticator.Login(" ", "1234");
            Assert.AreEqual(missatge, "L'usuari o contrassenya estan buits");
            Assert.IsTrue(!authenticator.Logejat);

            //Usuari nul
            missatge = authenticator.Login(null, "1234");
            Assert.AreEqual(missatge, "L'usuari o contrassenya estan buits");
            Assert.IsTrue(!authenticator.Logejat);

            //Pass nul
            missatge = authenticator.Login("1F", null);
            Assert.AreEqual(missatge, "L'usuari o contrassenya estan buits");
            Assert.IsTrue(!authenticator.Logejat);

            //Pass buit
            missatge = authenticator.Login(null, "");
            Assert.AreEqual(missatge, "L'usuari o contrassenya estan buits");
            Assert.IsTrue(!authenticator.Logejat);
        }

        [TestMethod]
        public void Login_Logout_TancaConnexio()
        {
            var authenticator = Bootstrap.GetAuthenticator();
            var loginService = Bootstrap.GetLoginService();
            authenticator.Logout();
            Assert.IsTrue(loginService.LogOutFet);
        }
    }
}
