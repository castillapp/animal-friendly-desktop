using DesktopApp.State.Authenticators;
using DesktopApp.State.Navigators;
using Persistencia.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace DesktopApp.ViewModels.Factories
{
    public class ZonesListViewModelFactory : IViewModelFactory<ZonesListViewModel>
    {
        private readonly IAdministrarTreballadorsService treballadorService;
        private readonly IAdministrarCentreService administraCentreService;
        private readonly IGestionarAnimalsService gestionarAnimalsService;
        private readonly IViewModelFactory<ZonaFitxaViewModel> zonaFitxaViewModelFactory;
        private readonly INavigator navigator;
        private readonly IAuthenticator authenticator;

        public ZonesListViewModelFactory(IAdministrarTreballadorsService treballadorService
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
        }

        public ZonesListViewModel CreateViewModel()
        {
            return new ZonesListViewModel(treballadorService, administraCentreService, gestionarAnimalsService,
                zonaFitxaViewModelFactory,
                navigator, authenticator);
        }
    }
}
