using DesktopApp.State.Navigators;
using Persistencia.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace DesktopApp.ViewModels.Factories
{
    public class AnimalFitxaViewModelFactory : IViewModelFactory<AnimalsListViewModel>
    {
        private readonly IGestionarAnimalsService gestionarAnimalsService;
        private readonly IViewModelFactory<AnimalFitxaViewModel> animalFitxaViewModelFactory;
        private readonly IViewModelFactory<AnimalsListViewModel> animalListVewModelFactory;

        public AnimalFitxaViewModelFactory(IGestionarAnimalsService gestionarAnimalsService,
            IViewModelFactory<AnimalFitxaViewModel> animalFitxaViewModelFactory,
            IViewModelFactory<AnimalsListViewModel> animalListVewModelFactory,
            INavigator navigator)
        {
            this.gestionarAnimalsService = gestionarAnimalsService;
            this.animalFitxaViewModelFactory = animalFitxaViewModelFactory;
            this.animalListVewModelFactory = animalListVewModelFactory;
            Navigator = navigator;
        }

        public INavigator Navigator { get; }

        public AnimalsListViewModel CreateViewModel()
        {
            return new AnimalsListViewModel(gestionarAnimalsService, animalFitxaViewModelFactory, animalListVewModelFactory, Navigator);
        }
    }
}
