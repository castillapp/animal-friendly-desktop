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
        private readonly IAdministrarTreballadorsService administrarTreballadorsService;
        private readonly IAdministrarCentreService administrarCentreService;
        private readonly IViewModelFactory<ZonaFitxaViewModel> zonaFitxaViewModelFactory;
        private readonly INavigator navigator;
        private readonly IAuthenticator authenticator;

        public ZonesListViewModelFactory(IAdministrarTreballadorsService administrarTreballadorsService,
            IAdministrarCentreService administrarCentreService,
            IViewModelFactory<ZonaFitxaViewModel> zonaFitxaViewModelFactory,
            INavigator navigator, IAuthenticator authenticator)
        {
            this.administrarTreballadorsService = administrarTreballadorsService;
            this.administrarCentreService = administrarCentreService;
            this.zonaFitxaViewModelFactory = zonaFitxaViewModelFactory;
            this.navigator = navigator;
            this.authenticator = authenticator;
        }

        public ZonesListViewModel CreateViewModel()
        {
            return new ZonesListViewModel(administrarTreballadorsService, administrarCentreService,
                zonaFitxaViewModelFactory,
                navigator, authenticator);
        }
    }
}
