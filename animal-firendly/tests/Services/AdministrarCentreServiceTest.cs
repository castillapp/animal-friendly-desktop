using Microsoft.VisualStudio.TestTools.UnitTesting;
using Persistencia.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DesktopAppTests.Services
{
    [TestClass]
    public class AdministrarCentreServiceTest
    {
        [TestMethod]
        public void AdministrarCentreService_LlistaCentres_HiHaZones()
        {
            using (var scope = Bootstrap.GetLifetimeScope())
            {
                var login = Bootstrap.GetAuthenticator(scope, "111111111A", "admin0");
                var servei = Bootstrap.GetAdministrarCentreService(scope);
                var centres = servei.GetAll();
                Assert.IsTrue(centres.Count() > 0);
            }
        }

        [TestMethod]
        public void AdministrarCentreService_LlistaZones_HiHaZones()
        {
            using (var scope = Bootstrap.GetLifetimeScope())
            {
                var login = Bootstrap.GetAuthenticator(scope, "111111111A", "admin0");
                var servei = Bootstrap.GetAdministrarCentreService(scope);
                var zones = servei.GetAllZones();
                Assert.IsTrue(zones.Count() > 0);
            }
        }

        [TestMethod]
        public void AdministrarCentreService_LlistaZonesCentre_HiHaZones()
        {
            using (var scope = Bootstrap.GetLifetimeScope())
            {
                var login = Bootstrap.GetAuthenticator(scope, "111111111A", "admin0");
                var servei = Bootstrap.GetAdministrarCentreService(scope);
                var zones = servei.GetZonesCentre(Persistencia.Models.TipusCentre.Barcelona);
                Assert.IsTrue(zones.Count() > 0);
            }
        }

        [TestMethod]
        public void AdministrarCentreService_CreaIActualitzaIMouZona_TotOk()
        {
            using (var scope = Bootstrap.GetLifetimeScope())
            {
                var login = Bootstrap.GetAuthenticator(scope, "111111111A", "admin0");
                var servei = Bootstrap.GetAdministrarCentreService(scope);

                Random rnd = new Random();
                int id = rnd.Next(10000000, 99999999);
                var nomZona = id.ToString() + 'A';

                var zona = new Zona()
                {
                    Nom = nomZona,
                    Aforament = 10,
                    Superficie = 11,
                    TipusZona = TipusZona.Exterior,
                    TipusCentre = TipusCentre.Barcelona
                };

                servei.CreaZona(zona);
                var zones = servei.GetAllZones();
                Assert.IsTrue(zones.Any(f => f.Nom == nomZona));

                zona.TipusCentre = TipusCentre.Palma;
                zona.Nom = nomZona + "Canvi";
                servei.ActualitzaZona(zona);

                zones = servei.GetZonesCentre(TipusCentre.Palma);
                Assert.IsTrue(zones.Any(f => f.Nom == nomZona + "Canvi"));
            }
        }
    }
}
