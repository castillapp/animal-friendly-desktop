using DesktopApp.State.Navigators;
using Persistencia.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace DesktopApp.ViewModels.Factories
{
    public class AnimalFitxaViewModelFactory : IViewModelFactory<AnimalFitxaViewModel>
    {
        private readonly IGestionarAnimalsService gestionarAnimalsService;
        private readonly INavigator navigator;

        public AnimalFitxaViewModelFactory(IGestionarAnimalsService gestionarAnimalsService, INavigator navigator)
        {
            this.gestionarAnimalsService = gestionarAnimalsService;
            this.navigator = navigator;
        }

        public AnimalFitxaViewModel CreateViewModel()
        {
            return new AnimalFitxaViewModel(gestionarAnimalsService, navigator);
        }
    }
}
