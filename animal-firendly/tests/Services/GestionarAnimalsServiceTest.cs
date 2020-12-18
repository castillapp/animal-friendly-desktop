using Microsoft.VisualStudio.TestTools.UnitTesting;
using Persistencia.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DesktopAppTests.Services
{
    [TestClass]
    public class GestionarAnimalsServiceTest
    {
        [TestMethod]
        public void AdministrarCentreService_LlistaAnimals_HiHaAnimals()
        {
            using (var scope = Bootstrap.GetLifetimeScope())
            {
                var login = Bootstrap.GetAuthenticator(scope, "44444444D", "vet1");
                var servei = Bootstrap.GetGestionarAnimalsService(scope);
                var animals = servei.GetAll();
                Assert.IsTrue(animals.Count() > 0);
            }
        }

        [TestMethod]
        public void AdministrarCentreService_LlistaAnimalsCentre_HiHaAnimals()
        {
            using (var scope = Bootstrap.GetLifetimeScope())
            {
                var login = Bootstrap.GetAuthenticator(scope, "44444444D", "vet1");
                var servei = Bootstrap.GetGestionarAnimalsService(scope);
                var animals = servei.LlistaAnimalsCentre(Persistencia.Models.TipusCentre.Palma);
                Assert.IsTrue(animals.Count() > 0);
            }
        }

        [TestMethod]
        public void AdministrarCentreService_MouAnimalZona_EsMouOk()
        {
            using (var scope = Bootstrap.GetLifetimeScope())
            {
                var login = Bootstrap.GetAuthenticator(scope, "44444444D", "vet1");
                var servei = Bootstrap.GetGestionarAnimalsService(scope);
                var centreServei = Bootstrap.GetAdministrarCentreService(scope);
                var animal = servei.LlistaAnimalsCentre(Persistencia.Models.TipusCentre.Palma).First();
                var zones = centreServei.GetZonesCentre(Persistencia.Models.TipusCentre.Palma);

                //agafem una nova zona diferent a la que esta ara
                var novaZona = zones.First(f => f.Id != animal.IdZona);

                var zonaInicial = animal.Zona;
                servei.MouAnimal(animal, novaZona);

                var animalMogut = servei.LlistaAnimalsCentre(Persistencia.Models.TipusCentre.Palma).Single(f => f.Id == animal.Id);
                Assert.IsTrue(animalMogut.Zona.Id == novaZona.Id);
            }
        }

        public void AdministrarCentreService_CreaIModificaAnimal_EsCreaIEsModificaOk()
        {
            using (var scope = Bootstrap.GetLifetimeScope())
            {
                var login = Bootstrap.GetAuthenticator(scope, "44444444D", "vet1");
                var servei = Bootstrap.GetGestionarAnimalsService(scope);

                Random rnd = new Random();
                int id = rnd.Next(10000000, 99999999);
                var nom = id.ToString() + 'A';

                var animal = new Animal()
                {
                    DataAcollida = "2021",
                    DataArribada = "2020",
                    DataNaixement = "2019",
                    TipusCentre = TipusCentre.Barcelona,
                    Nom = nom,
                    NumXip = nom,
                    Raca = "test",
                    TipusAnimal = TipusAnimal.Gos,
                    TeXip = true
                };

                servei.Crea(animal);
                var animals = servei.GetAll();
                Assert.IsTrue(animals.Any(f => f.Nom == nom));

                animal.Nom = nom + "B";
                servei.Modifica(animal);
                animals = servei.GetAll();
                Assert.IsTrue(animals.Any(f => f.Nom == nom + "B"));
            }
        }

        [TestMethod]
        public void AdministrarCentreService_LlistaICreaAtencions_EsLlistaICreaOk()
        {
            using (var scope = Bootstrap.GetLifetimeScope())
            {
                var login = Bootstrap.GetAuthenticator(scope, "44444444D", "vet1");
                var servei = Bootstrap.GetGestionarAnimalsService(scope);
                var treballadorService = Bootstrap.GetAdministrarTreballadorsService(scope);
                var animal = servei.LlistaAnimalsCentre(Persistencia.Models.TipusCentre.Palma).First();

                Random rnd = new Random();
                int id = rnd.Next(10000000, 99999999);
                var motiu = id.ToString() + 'A';

                var treballador = treballadorService.GetTreballador(login.GetDNIUsuari);

                servei.NovaAtencioAnimal(new Persistencia.Models.AtencioAnimal()
                {
                    Animal = animal.Id,
                    Data = DateTime.Now,
                    Motiu = motiu
                }, treballador);

                var atencions = servei.LlistarAtencionsAnimal(animal);
                Assert.IsTrue(atencions.Any(f => f.Motiu == motiu));
            }
        }
    }
}
