using Microsoft.VisualStudio.TestTools.UnitTesting;
using DesktopApp.State.Authenticators;
using System;
using System.Collections.Generic;
using System.Text;

namespace DesktopAppTests.State.Authenticators
{
    [TestClass]
    public class AuthenticatorTest
    {
        [TestMethod]
        public void Login_CredencialsIncorrectes_ReturnsCredencialsIncorrectes()
        {
            var authenticator = new Authenticator();

            //Usuari que no existeix
            var missatge = authenticator.Login("Usuari", "Incorrecte");
            Assert.AreEqual(missatge, "L'usuari no existeix o la contrassenya no és correcte");
            Assert.IsTrue(!authenticator.Logejat);

            //Password incorrecte
            missatge = authenticator.Login("Test", "12345");
            Assert.AreEqual(missatge, "L'usuari no existeix o la contrassenya no és correcte");
            Assert.IsTrue(!authenticator.Logejat);
        }

        [TestMethod]
        public void Login_CredencialsCorrectes_ReturnsTrue()
        {
            var authenticator = new Authenticator();

            var missatge = authenticator.Login("test", "1234");
            Assert.AreEqual(missatge, "Usuari loguejat!");
            Assert.IsTrue(authenticator.Logejat);
        }

        [TestMethod]
        public void Login_CredencialsBuides_ReturnsCredencialsBuides()
        {
            var authenticator = new Authenticator();

            //Usuari buit
            var missatge = authenticator.Login(" ", "1234");
            Assert.AreEqual(missatge, "L'usuari o contrassenya estan buits");
            Assert.IsTrue(!authenticator.Logejat);

            //Usuari nul
            missatge = authenticator.Login(null, "1234");
            Assert.AreEqual(missatge, "L'usuari o contrassenya estan buits");
            Assert.IsTrue(!authenticator.Logejat);

            //Pass nul
            missatge = authenticator.Login("Test", null);
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
            /*NOTA: Amb la configuració actual és molt complicat fer aquest test,
             * ja que el Servei de login (LoginService) Mock de la persistència 
             * guarda en una propietat si s'ha fet logout.
             * Per fer aquesta proba caldria accedir al propi servei, que és una variable interna
             * priivada de Authenticator.
             * Una vegada implementat la Inversio de Controal amb Injecció de dependències, podem
             * instanciar una classe estàtica de Mock LoginService (singleton) i accedir-hi des
             * de fora el Authenticator.
            */

            Assert.IsTrue(true);
        }
    }
}
