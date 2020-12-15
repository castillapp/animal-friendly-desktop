using DesktopApp.State.Navigators;
using Persistencia.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace DesktopApp.ViewModels.Factories
{
    public class ZonaFitxaViewModelFactory : IViewModelFactory<ZonaFitxaViewModel>
    {
        private readonly IAdministrarCentreService administrarCentreService;
        private readonly INavigator navigator;

        public ZonaFitxaViewModelFactory(IAdministrarCentreService administrarCentreService, INavigator navigator)
        {
            this.administrarCentreService = administrarCentreService;
            this.navigator = navigator;
        }

        public ZonaFitxaViewModel CreateViewModel()
        {
            return new ZonaFitxaViewModel(administrarCentreService, navigator);
        }
    }
}
