using Microsoft.VisualStudio.TestTools.UnitTesting;
using Persistencia.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DesktopAppTests.ViewModels
{
    [TestClass]
    public class AdministrarTreballadorsViewModels
    {
        [TestMethod]
        public void AdminTreballadorsVM_EliminarTreballador_TreballadorEliminatNoExisteix()
        {
            var treballadors = MockPersistencia.Data.MockDatabase.Treballadors;
            var llistaTreballadors = treballadors.ListAll();
            var treballadorABorrar = llistaTreballadors[2];

            var viewModel = Bootstrap.GetTreballadorsListViewModel();
            viewModel.TreballadorSeleccionat = treballadorABorrar;
            viewModel.FerModificacio(DesktopApp.ConstantsData.TipusOperacio.Elimina);

            llistaTreballadors = treballadors.ListAll();
            Assert.IsFalse(llistaTreballadors.Any(f => f.Id == treballadorABorrar.Id));
        }

        [TestMethod]
        public void AdminTreballadorsVM_AfegirTreballador_TreballadorAfegitExisteix()
        {
            var treballadors = MockPersistencia.Data.MockDatabase.Treballadors;
            var llistaTreballadors = treballadors.ListAll();

            var fitxaViewModel = Bootstrap.GetTreballadorFitxaViewModel();

            var nouTreballador = new Treballador()
            {
                Nom = "nou",
                Cognoms = "nou nou",
                DNI = "11",
                Naixement = "11/11/11",
                Email = "test@test.com",
                IdTipusTreballador = 0
            };

            fitxaViewModel.ObreFitxa(Bootstrap.GetTreballadorsListViewModel(), nouTreballador, DesktopApp.ConstantsData.TipusOperacio.Crea);
            fitxaViewModel.FerModificacio(DesktopApp.ConstantsData.TipusOperacio.Accepta);

            llistaTreballadors = treballadors.ListAll();
            Assert.IsTrue(llistaTreballadors.Any(f => f.DNI == nouTreballador.DNI));
        }

        [TestMethod]
        public void AdminTreballadorsVM_AfegirTreballadorCancela_TreballadorAfegitCancelatNoExisteix()
        {
            var treballadors = MockPersistencia.Data.MockDatabase.Treballadors;
            var llistaTreballadors = treballadors.ListAll();

            var fitxaViewModel = Bootstrap.GetTreballadorFitxaViewModel();

            var nouTreballador = new Treballador()
            {
                Nom = "nou",
                Cognoms = "nou nou",
                DNI = "11",
                Naixement = "11/11/11",
                Email = "test@test.com",
                IdTipusTreballador = 0
            };

            fitxaViewModel.ObreFitxa(Bootstrap.GetTreballadorsListViewModel(), nouTreballador, DesktopApp.ConstantsData.TipusOperacio.Crea);
            fitxaViewModel.FerModificacio(DesktopApp.ConstantsData.TipusOperacio.Cancela);

            llistaTreballadors = treballadors.ListAll();
            Assert.IsFalse(llistaTreballadors.Any(f => f.DNI == nouTreballador.DNI));
        }

        [TestMethod]
        public void AdminTreballadorsVM_ModificarTreballador_TreballadorModificatSi()
        {
            var treballadors = MockPersistencia.Data.MockDatabase.Treballadors;
            var llistaTreballadors = treballadors.ListAll();
            var treballadorATestejar = llistaTreballadors[2];
            var DNIAntic = treballadorATestejar.DNI;
            var DNINou = "111";
            var fitxaViewModel = Bootstrap.GetTreballadorFitxaViewModel();

            fitxaViewModel.ObreFitxa(Bootstrap.GetTreballadorsListViewModel(), treballadorATestejar, DesktopApp.ConstantsData.TipusOperacio.Modifica);
            fitxaViewModel.DNI = DNINou;
            fitxaViewModel.FerModificacio(DesktopApp.ConstantsData.TipusOperacio.Accepta);

            llistaTreballadors = treballadors.ListAll();
            var treballadorComparar = llistaTreballadors.Find(f => f.Id == treballadorATestejar.Id);
            Assert.AreNotEqual(treballadorComparar.DNI, DNIAntic);
            Assert.AreEqual(treballadorComparar.DNI, DNINou);
        }

        [TestMethod]
        public void AdminTreballadorsVM_ModificarTreballador_TreballadorModificatNo()
        {
            var treballadors = MockPersistencia.Data.MockDatabase.Treballadors;
            var llistaTreballadors = treballadors.ListAll();
            var treballadorATestejar = llistaTreballadors[2];
            var DNIAntic = treballadorATestejar.DNI;
            var DNINou = "111";
            var fitxaViewModel = Bootstrap.GetTreballadorFitxaViewModel();

            fitxaViewModel.ObreFitxa(Bootstrap.GetTreballadorsListViewModel(), treballadorATestejar, DesktopApp.ConstantsData.TipusOperacio.Modifica);
            fitxaViewModel.DNI = DNINou;
            fitxaViewModel.FerModificacio(DesktopApp.ConstantsData.TipusOperacio.Cancela);

            llistaTreballadors = treballadors.ListAll();
            var treballadorComparar = llistaTreballadors.Find(f => f.Id == treballadorATestejar.Id);
            Assert.AreEqual(treballadorComparar.DNI, DNIAntic);
            Assert.AreNotEqual(treballadorComparar.DNI, DNINou);
        }

        [TestMethod]
        public void AdminTreballadorsVM_VisualitzaTreballador_TreballadorDadesMostradesIguals()
        {
            var treballadors = MockPersistencia.Data.MockDatabase.Treballadors;
            var llistaTreballadors = treballadors.ListAll();
            var treballadorATestejar = llistaTreballadors[2];
            var fitxaViewModel = Bootstrap.GetTreballadorFitxaViewModel();

            fitxaViewModel.ObreFitxa(Bootstrap.GetTreballadorsListViewModel(), treballadorATestejar, DesktopApp.ConstantsData.TipusOperacio.Llegeix);

            Assert.AreEqual(treballadorATestejar.Nom, fitxaViewModel.Nom);
            Assert.AreEqual(treballadorATestejar.Cognoms, fitxaViewModel.Cognoms);
            Assert.AreEqual(treballadorATestejar.DNI, fitxaViewModel.DNI);
            Assert.AreEqual(treballadorATestejar.Email, fitxaViewModel.Email);
            Assert.AreEqual(treballadorATestejar.Telefon, fitxaViewModel.Telefon);
            Assert.AreEqual(treballadorATestejar.Naixement, fitxaViewModel.Naixement);
            Assert.AreEqual(treballadorATestejar.TipusTreballador, fitxaViewModel.TipusTreballador);
            Assert.AreEqual(treballadorATestejar.Sou, fitxaViewModel.Sou);
        }
    }
}
