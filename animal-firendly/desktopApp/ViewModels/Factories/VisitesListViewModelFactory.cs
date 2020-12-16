using DesktopApp.State.Navigators;
using Persistencia.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace DesktopApp.ViewModels.Factories
{
    public class VisitesListViewModelFactory : IViewModelFactory<VisitesListViewModel>
    {
        private readonly IGestionarAnimalsService gestionarAnimalsService;
        private readonly IViewModelFactory<VisitaFitxaViewModel> visitaFitxaViewModelFactory;
        private readonly INavigator navigator;

        public VisitesListViewModelFactory(IGestionarAnimalsService gestionarAnimalsService,
            IViewModelFactory<VisitaFitxaViewModel> visitaFitxaViewModelFactory,
            INavigator navigator)
        {
            this.gestionarAnimalsService = gestionarAnimalsService;
            this.visitaFitxaViewModelFactory = visitaFitxaViewModelFactory;
            this.navigator = navigator;
        }

        public VisitesListViewModel CreateViewModel()
        {
            return new VisitesListViewModel(gestionarAnimalsService, visitaFitxaViewModelFactory, navigator);
        }
    }
}
