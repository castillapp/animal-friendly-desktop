using DesktopApp.Commands;
using DesktopApp.ConstantsData;
using DesktopApp.State.Authenticators;
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
    public class AnimalsListViewModel : BaseViewModel, IModelModificable
    {
        public ICommand AccioModificacio { get; }
        private List<Animal> animals;
        private readonly IGestionarAnimalsService gestionarAnimalsService;
        private readonly IAdministrarTreballadorsService treballadorsService;
        private readonly IViewModelFactory<AnimalFitxaViewModel> animalFitxaViewModelFactory;
        private readonly IViewModelFactory<VisitesListViewModel> visitesListVewModelFactory;
        private readonly IViewModelFactory<ZonesListViewModel> zonesListModelFactory;
        private readonly INavigator navigator;
        private readonly IAuthenticator authenticator;
        private Treballador treballador;
        private IEnumerable<Centre> centresTreballador;

        public Animal AnimalSeleccionat { get; set; }
        public List<Animal> Animals
        {
            get { return animals; }
            private set { animals = value; OnPropertyChanged(nameof(Animals)); }
        }

        /// <summary>
        /// Llista els animals de dels centres dels quals el treballador pertany
        /// </summary>
        public void Carregar()
        {
            treballador = treballadorsService.GetTreballador(authenticator.GetDNIUsuari);
            centresTreballador = treballadorsService.LlistaCentresAssignats(treballador);
            var animals = new List<Animal>();
            foreach (var centre in centresTreballador)
            {
                animals.AddRange(gestionarAnimalsService.LlistaAnimalsCentre(centre.TipusCentre));
            }
            this.animals = animals;
        }

        public AnimalsListViewModel(IGestionarAnimalsService gestionarAnimalsService,
            IAdministrarTreballadorsService treballadorsService,
            IViewModelFactory<AnimalFitxaViewModel> animalFitxaViewModelFactory,
            IViewModelFactory<VisitesListViewModel> visitesListVewModelFactory,
            IViewModelFactory<ZonesListViewModel> zonesListModelFactory,
            INavigator navigator,
            IAuthenticator authenticator)
        {
            this.gestionarAnimalsService = gestionarAnimalsService;
            this.treballadorsService = treballadorsService;
            this.animalFitxaViewModelFactory = animalFitxaViewModelFactory;
            this.visitesListVewModelFactory = visitesListVewModelFactory;
            this.zonesListModelFactory = zonesListModelFactory;
            this.navigator = navigator;
            this.authenticator = authenticator;
            Carregar();
            AccioModificacio = new AccioModificacioModelCommand<AnimalsListViewModel>(this);
        }

        public void FerModificacio(TipusOperacio tipusOperacio)
        {
            switch (tipusOperacio)
            {
                case TipusOperacio.Crea:
                    var animalViewModel = animalFitxaViewModelFactory.CreateViewModel();
                    animalViewModel.ObreFitxa(this, new Animal(), tipusOperacio);
                    navigator.CurrentViewModel = animalViewModel;
                    break;
                case TipusOperacio.Modifica:
                case TipusOperacio.Llegeix:
                    if (AnimalSeleccionat == null)
                    {
                        BaseViewModel.MessageViewModel.DisplayMessage("No s'ha seleccionat cap animal per mostrar o editar");
                        break;
                    }
                    animalViewModel = animalFitxaViewModelFactory.CreateViewModel();
                    animalViewModel.ObreFitxa(this, AnimalSeleccionat, tipusOperacio);
                    navigator.CurrentViewModel = animalViewModel;
                    break;
                case TipusOperacio.AnimalMoureAZona:
                    var zonesList = zonesListModelFactory.CreateViewModel();
                    zonesList.Carregar(new Centre() { Id = AnimalSeleccionat.IdCentre }, AnimalSeleccionat, this);
                    navigator.CurrentViewModel = zonesList;
                    break;
                case TipusOperacio.AnimalLlistarAtencions:
                    if (AnimalSeleccionat == null)
                    {
                        BaseViewModel.MessageViewModel.DisplayMessage("No s'ha seleccionat cap animal per mostrar o editar");
                        break;
                    }
                    var atencionsAnimal = visitesListVewModelFactory.CreateViewModel();
                    atencionsAnimal.Carregar(AnimalSeleccionat, treballador, this);
                    navigator.CurrentViewModel = atencionsAnimal;
                    break;
                case TipusOperacio.Elimina:
                case TipusOperacio.Accepta:
                case TipusOperacio.Cancela:
                case TipusOperacio.TreballadorAssignarCentre:
                default:
                    throw new NotSupportedException("Acció no suportada");
            }
        }
    }
}
