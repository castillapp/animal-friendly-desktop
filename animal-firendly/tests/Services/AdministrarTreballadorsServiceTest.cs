using Microsoft.VisualStudio.TestTools.UnitTesting;
using Persistencia.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DesktopAppTests.Services
{
    [TestClass]
    public class AdministrarTreballadorsServiceTest
    {
        [TestMethod]
        public void AdministrarTreballadorsService_LlistaTreballadors_HiHaTreballadors()
        {
            using (var scope = Bootstrap.GetLifetimeScope())
            {
                var login = Bootstrap.GetAuthenticator(scope, "111111111A", "admin0");
                var servei = Bootstrap.GetAdministrarTreballadorsService(scope);
                var treballadors = servei.GetAll();
                Assert.IsTrue(treballadors.Count() > 0);
            }
        }

        [TestMethod]
        public void AdministrarTreballadorsService_AgafaTreballador_TeNomCorrecte()
        {
            using (var scope = Bootstrap.GetLifetimeScope())
            {
                var login = Bootstrap.GetAuthenticator(scope, "111111111A", "admin0");
                var servei = Bootstrap.GetAdministrarTreballadorsService(scope);
                var treballador = servei.GetTreballador("55555555E");
                Assert.IsTrue(treballador.Nom == "Josep");
            }
        }

        [TestMethod]
        public void AdministrarTreballadorsService_ModificaTreballador_TeNomCorrecte()
        {
            //No está implementada les crides a la bbdd per actualitzar un treballador
            Assert.Inconclusive();
            using (var scope = Bootstrap.GetLifetimeScope())
            {
                var login = Bootstrap.GetAuthenticator(scope, "111111111A", "admin0");
                var servei = Bootstrap.GetAdministrarTreballadorsService(scope);
                var treballador = servei.GetTreballador("33333333C");
                Assert.IsTrue(treballador.Nom == "Joan");

                treballador.Nom = "Francesc";
                servei.Modifica(treballador);

                treballador = servei.GetTreballador("33333333C");
                Assert.IsTrue(treballador.Nom == "Francesc");

                treballador.Nom = "Joan";
                servei.Modifica(treballador);
            }
        }

        [TestMethod]
        public void AdministrarTreballadorsService_CreaIAssigna_EsCreaTreballadorIAssignaCentre()
        {
            using (var scope = Bootstrap.GetLifetimeScope())
            {
                var login = Bootstrap.GetAuthenticator(scope, "111111111A", "admin0");
                var servei = Bootstrap.GetAdministrarTreballadorsService(scope);

                Random rnd = new Random();
                int id = rnd.Next(10000000, 99999999);
                var dni = id.ToString() + 'A';

                var treballador = new Treballador()
                {
                    Nom = "nou",
                    Cognoms = "nou nou",
                    DNI = dni,
                    Password = "pass",
                    Email = dni+"@test.com",
                    Telefon = "9366666666",
                    Sou = 600,
                    IdTipusTreballador = 0
                };

                servei.Crea(treballador);
                treballador = servei.GetTreballador(dni);
                Assert.IsTrue(treballador.Nom == "nou");

                var centres = servei.LlistaCentresAssignats(treballador);
                Assert.IsTrue(centres.Count() == 0);

                servei.AssignaTreballador(treballador, new Centre() { Id = 0 });
                centres = servei.LlistaCentresAssignats(treballador);
                Assert.IsTrue(centres.First().Id == 0);
            }
        }
    }
}
