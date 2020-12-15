using DesktopApp.State.Navigators;
using Persistencia.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace DesktopApp.ViewModels.Factories
{
    public class VisitaFitxaViewModelFactory : IViewModelFactory<VisitaFitxaViewModel>
    {
        private readonly IGestionarAnimalsService gestionarAnimalsService;
        private readonly INavigator navigator;

        public VisitaFitxaViewModelFactory(IGestionarAnimalsService gestionarAnimalsService, INavigator navigator)
        {
            this.gestionarAnimalsService = gestionarAnimalsService;
            this.navigator = navigator;
        }

        public VisitaFitxaViewModel CreateViewModel()
        {
            return new VisitaFitxaViewModel(gestionarAnimalsService, navigator);
        }
    }
}
