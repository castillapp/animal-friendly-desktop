using DesktopApp.State.Navigators;
using Persistencia.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace DesktopApp.ViewModels.Factories
{
    public class TreballadorsListViewModelFactory : IViewModelFactory<TreballadorsListViewModel>
    {
        private readonly IAdministrarTreballadorsService administrarTreballadorsService;
        private readonly IViewModelFactory<TreballadorFitxaViewModel> treballadorFitxaModelFactory;
        private readonly IViewModelFactory<TreballadorAssignatsViewModel> treballadorAssignatModelFactory;
        private readonly INavigator navigator;

        public TreballadorsListViewModelFactory(IAdministrarTreballadorsService administrarTreballadorsService,
            IViewModelFactory<TreballadorFitxaViewModel> treballadorFitxaModelFactory,
            IViewModelFactory<TreballadorAssignatsViewModel> treballadorAssignatModelFactory,
            INavigator navigator)
        {
            this.administrarTreballadorsService = administrarTreballadorsService;
            this.treballadorFitxaModelFactory = treballadorFitxaModelFactory;
            this.treballadorAssignatModelFactory = treballadorAssignatModelFactory;
            this.navigator = navigator;
        }

        public TreballadorsListViewModel CreateViewModel()
        {
            return new TreballadorsListViewModel(administrarTreballadorsService, treballadorFitxaModelFactory, treballadorAssignatModelFactory, navigator);
        }
    }
}
