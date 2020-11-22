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
        private readonly INavigator navigator;

        public TreballadorsListViewModelFactory(IAdministrarTreballadorsService administrarTreballadorsService, 
            IViewModelFactory<TreballadorFitxaViewModel> treballadorFitxaModelFactory, INavigator navigator)
        {
            this.administrarTreballadorsService = administrarTreballadorsService;
            this.treballadorFitxaModelFactory = treballadorFitxaModelFactory;
            this.navigator = navigator;
        }

        public TreballadorsListViewModel CreateViewModel()
        {
            return new TreballadorsListViewModel(administrarTreballadorsService, treballadorFitxaModelFactory, navigator);
        }
    }
}
