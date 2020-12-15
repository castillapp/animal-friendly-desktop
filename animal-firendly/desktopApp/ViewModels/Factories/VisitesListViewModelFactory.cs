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
        private readonly IViewModelFactory<VisitesListViewModel> visitesListViewModelFactory;
        private readonly INavigator navigator;

        public VisitesListViewModelFactory(IGestionarAnimalsService gestionarAnimalsService,
            IViewModelFactory<VisitaFitxaViewModel> visitaFitxaViewModelFactory,
            IViewModelFactory<VisitesListViewModel> visitesListViewModelFactory,
            INavigator navigator)
        {
            this.gestionarAnimalsService = gestionarAnimalsService;
            this.visitaFitxaViewModelFactory = visitaFitxaViewModelFactory;
            this.visitesListViewModelFactory = visitesListViewModelFactory;
            this.navigator = navigator;
        }

        public VisitesListViewModel CreateViewModel()
        {
            return new VisitesListViewModel(gestionarAnimalsService, visitaFitxaViewModelFactory, visitesListViewModelFactory, navigator);
        }
    }
}
