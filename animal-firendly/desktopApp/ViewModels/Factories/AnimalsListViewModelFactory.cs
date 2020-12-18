using DesktopApp.State.Authenticators;
using DesktopApp.State.Navigators;
using Persistencia.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace DesktopApp.ViewModels.Factories
{
    public class AnimalsListViewModelFactory : IViewModelFactory<AnimalsListViewModel>
    {
        private readonly IGestionarAnimalsService gestionarAnimalsService;
        private readonly IAdministrarTreballadorsService treballadorsService;
        private readonly IViewModelFactory<AnimalFitxaViewModel> animalFitxaViewModelFactory;
        private readonly IViewModelFactory<VisitesListViewModel> visitesListVewModelFactory;
        private readonly IViewModelFactory<ZonesListViewModel> zonesListModelFactory;
        private readonly INavigator navigator;
        private readonly IAuthenticator authenticator;

        public AnimalsListViewModelFactory(IGestionarAnimalsService gestionarAnimalsService,
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
        }

        public AnimalsListViewModel CreateViewModel()
        {
            return new AnimalsListViewModel(gestionarAnimalsService, treballadorsService,
                animalFitxaViewModelFactory, visitesListVewModelFactory, zonesListModelFactory,
                navigator, authenticator);
        }
    }
}
