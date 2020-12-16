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
        private readonly IViewModelFactory<AnimalFitxaViewModel> animalFitxaViewModelFactory;
        private readonly INavigator navigator;

        public AnimalsListViewModelFactory(IGestionarAnimalsService gestionarAnimalsService,
            IViewModelFactory<AnimalFitxaViewModel> animalFitxaViewModelFactory,
            INavigator navigator)
        {
            this.gestionarAnimalsService = gestionarAnimalsService;
            this.animalFitxaViewModelFactory = animalFitxaViewModelFactory;
            this.navigator = navigator;
        }

        public AnimalsListViewModel CreateViewModel()
        {
            return new AnimalsListViewModel(gestionarAnimalsService, animalFitxaViewModelFactory, navigator);
        }
    }
}
