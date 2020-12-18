using DesktopApp.ConstantsData;
using DesktopApp.Commands;
using DesktopApp.State.Navigators;
using DesktopApp.ViewModels.Factories;
using Persistencia.Models;
using Persistencia.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace DesktopApp.ViewModels
{
    public class VisitesListViewModel : BaseViewModel, IModelModificable
    {
        private readonly IGestionarAnimalsService gestionarAnimalsService;
        private readonly IViewModelFactory<VisitaFitxaViewModel> visitaFitxaViewModelFactory;
        private readonly INavigator navigator;
        private IList<AtencioAnimal> visites;
        private AnimalsListViewModel animalsListViewModel;
        private Animal animal;
        private Treballador treballador;

        public IList<AtencioAnimal> AtencionsAnimal
        {
            get { return visites; }
            private set { visites = value; OnPropertyChanged(nameof(AtencionsAnimal)); }
        }

        public string TitolVista
        {
            get
            {
                return "Visites de " + animal.Nom;
            }
        }

        public ICommand AccioModificacio { get; }

        public VisitesListViewModel(IGestionarAnimalsService gestionarAnimalsService,
            IViewModelFactory<VisitaFitxaViewModel> visitaFitxaViewModelFactory,
            INavigator navigator)
        {
            this.gestionarAnimalsService = gestionarAnimalsService;
            this.visitaFitxaViewModelFactory = visitaFitxaViewModelFactory;
            this.navigator = navigator;
            AccioModificacio = new AccioModificacioModelCommand<VisitesListViewModel>(this);
        }

        /// <summary>
        /// Llista les visites de l'animal, agafant-los de la BBDD
        /// </summary>
        public void Carregar(Animal animal, Treballador treballador, AnimalsListViewModel animalsListViewModel)
        {
            this.animalsListViewModel = animalsListViewModel;
            this.animal = animal;
            this.treballador = treballador;
            Actualitza();
        }

        /// <summary>
        /// Refresca la llista
        /// </summary>
        public void Actualitza()
        {
            visites = gestionarAnimalsService.LlistarAtencionsAnimal(animal);
        }

        public void FerModificacio(TipusOperacio tipusOperacio)
        {
            switch (tipusOperacio)
            {
                case TipusOperacio.Cancela:
                    navigator.CurrentViewModel = animalsListViewModel;
                    break;
                case TipusOperacio.Crea:
                    var visitaFitxa = this.visitaFitxaViewModelFactory.CreateViewModel();
                    visitaFitxa.ObreFitxa(this, new AtencioAnimal(), animal, treballador, tipusOperacio);
                    navigator.CurrentViewModel = visitaFitxa;
                    break;
                default:
                    throw new NotSupportedException("Operació no permesa");
            }
        }
    }
}
