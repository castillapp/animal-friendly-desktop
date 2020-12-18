using DesktopApp.Commands;
using DesktopApp.ConstantsData;
using DesktopApp.State.Authenticators;
using DesktopApp.State.Navigators;
using DesktopApp.ViewModels.Factories;
using Persistencia.Models;
using Persistencia.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace DesktopApp.ViewModels
{
    public class ZonesListViewModel : BaseViewModel, IModelModificable
    {
        private readonly IAdministrarTreballadorsService treballadorService;
        private readonly IAdministrarCentreService administraCentreService;
        private readonly IGestionarAnimalsService gestionarAnimalsService;
        private readonly IViewModelFactory<ZonaFitxaViewModel> zonaFitxaViewModelFactory;
        private readonly INavigator navigator;
        private readonly IAuthenticator authenticator;
        private List<Zona> zones;
        private IEnumerable<Centre> centres;
        private AnimalsListViewModel animalsListViewModel;
        private Animal animalAMoure;
        private TipusOperacio tipusAccioModificacio;

        public TipusOperacio TipusAccioModificacio
        {
            get { return tipusAccioModificacio; }
            set { tipusAccioModificacio = value; OnPropertyChanged(nameof(TipusAccioModificacio)); }
        }

        public List<Zona> Zones
        {
            get { return zones; }
            private set { zones = value; OnPropertyChanged(nameof(Zones)); }
        }
        public Zona ZonaSeleccionada { get; set; }

        public ZonesListViewModel(IAdministrarTreballadorsService treballadorService
            , IAdministrarCentreService administraCentreService
            , IGestionarAnimalsService gestionarAnimalsService
            , IViewModelFactory<ZonaFitxaViewModel> zonaFitxaViewModelFactory
            , INavigator navigator
            , IAuthenticator authenticator)
        {
            this.treballadorService = treballadorService;
            this.administraCentreService = administraCentreService;
            this.gestionarAnimalsService = gestionarAnimalsService;
            this.zonaFitxaViewModelFactory = zonaFitxaViewModelFactory;
            this.navigator = navigator;
            this.authenticator = authenticator;
            Carregar();
            AccioModificacio = new AccioModificacioModelCommand<ZonesListViewModel>(this);
        }

        /// <summary>
        /// Llista les zones de dels centres dels quals el treballador pertany
        /// </summary>
        public void Carregar()
        {
            centres = treballadorService.LlistaCentresAssignats(new Treballador() { DNI = authenticator.GetDNIUsuari });
            var zones = new List<Zona>();
            foreach (var centre in centres)
            {
                zones.AddRange(administraCentreService.GetZonesCentre(centre.TipusCentre));
            }
            this.zones = zones;
            TipusAccioModificacio = TipusOperacio.Llegeix;
        }

        /// <summary>
        /// Llista les zones del centre que li passem. Es fa servir per gestionar el selector de canvi de zona de l'animal
        /// </summary>
        /// <param name="centre">Centre on pertany l'animal</param>
        /// <param name="viewModelAnterior">Llistat d'animals, pantalla anterior, per tornar enrere</param>
        public void Carregar(Centre centre, Animal animal, AnimalsListViewModel viewModelAnterior)
        {
            this.animalAMoure = animal;
            centres = new List<Centre>() { centre };
            zones = administraCentreService.GetZonesCentre(centre.TipusCentre).ToList();
            this.animalsListViewModel = viewModelAnterior;
            TipusAccioModificacio = TipusOperacio.AnimalMoureAZona;
        }

        public ICommand AccioModificacio { get; }

        public void FerModificacio(TipusOperacio tipusOperacio)
        {
            switch (tipusOperacio)
            {
                case TipusOperacio.Crea:
                    var zonaViewModel = zonaFitxaViewModelFactory.CreateViewModel();
                    zonaViewModel.ObreFitxa(this, new Zona(), tipusOperacio);
                    navigator.CurrentViewModel = zonaViewModel;
                    break;
                case TipusOperacio.Modifica:
                    if (ZonaSeleccionada == null)
                    {
                        BaseViewModel.MessageViewModel.DisplayMessage("No s'ha seleccionat cap zona per mostrar o editar");
                        break;
                    }
                    zonaViewModel = zonaFitxaViewModelFactory.CreateViewModel();
                    zonaViewModel.ObreFitxa(this, ZonaSeleccionada, tipusOperacio);
                    navigator.CurrentViewModel = zonaViewModel;
                    break;
                case TipusOperacio.Llegeix:
                    if (ZonaSeleccionada == null)
                    {
                        BaseViewModel.MessageViewModel.DisplayMessage("No s'ha seleccionat cap zona per mostrar o editar");
                        break;
                    }
                    zonaViewModel = zonaFitxaViewModelFactory.CreateViewModel();
                    zonaViewModel.ObreFitxa(this, ZonaSeleccionada, tipusOperacio);
                    navigator.CurrentViewModel = zonaViewModel;
                    break;
                case TipusOperacio.Accepta:
                    if (ZonaSeleccionada == null)
                    {
                        BaseViewModel.MessageViewModel.DisplayMessage("No s'ha seleccionat cap zona per assignar");
                        break;
                    }
                    gestionarAnimalsService.MouAnimal(animalAMoure, ZonaSeleccionada);
                    animalsListViewModel.Carregar();
                    navigator.CurrentViewModel = animalsListViewModel;
                    break;
                case TipusOperacio.Cancela:
                    navigator.CurrentViewModel = animalsListViewModel;
                    break;
                case TipusOperacio.Elimina:
                case TipusOperacio.TreballadorAssignarCentre:
                default:
                    throw new NotSupportedException("funcionalitat no suportada");
            }
        }
    }
}
