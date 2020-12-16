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
    public class ZonesListViewModel : BaseViewModel, IModelModificable
    {
        private readonly IAdministrarTreballadorsService treballadorService;
        private readonly IAdministrarCentreService administraCentreService;
        private readonly IViewModelFactory<ZonaFitxaViewModel> zonaFitxaViewModelFactory;
        private readonly INavigator navigator;
        private readonly IAuthenticator authenticator;
        private List<Zona> zones;
        private IEnumerable<Centre> centresTreballador;

        public List<Zona> Zones
        {
            get { return zones; }
            private set { zones = value; OnPropertyChanged(nameof(Zones)); }
        }
        public Zona ZonaSeleccionada { get; set; }

        public ZonesListViewModel(IAdministrarTreballadorsService treballadorService
            , IAdministrarCentreService administraCentreService
            , IViewModelFactory<ZonaFitxaViewModel> zonaFitxaViewModelFactory
            , INavigator navigator
            , IAuthenticator authenticator)
        {
            this.treballadorService = treballadorService;
            this.administraCentreService = administraCentreService;
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
            centresTreballador = treballadorService.LlistaCentresAssignats(new Treballador() { DNI = authenticator.GetDNIUsuari});
            var zones = new List<Zona>();
            foreach (var centre in centresTreballador)
            {
                zones.AddRange(administraCentreService.GetZonesCentre(centre.TipusCentre));
            }
            this.zones = zones;
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
                    if(ZonaSeleccionada == null)
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
                case TipusOperacio.Elimina:
                case TipusOperacio.Accepta:
                case TipusOperacio.Cancela:
                case TipusOperacio.TreballadorAssignarCentre:
                default:
                    throw new NotSupportedException("funcionalitat no suportada");
            }
        }
    }
}
