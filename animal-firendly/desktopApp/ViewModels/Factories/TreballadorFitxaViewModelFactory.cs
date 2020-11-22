using DesktopApp.State.Navigators;
using Persistencia.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace DesktopApp.ViewModels.Factories
{
    public class TreballadorFitxaViewModelFactory : IViewModelFactory<TreballadorFitxaViewModel>
    {
        private readonly IAdministrarTreballadorsService administrarTreballadorsService;
        private readonly INavigator navigator;

        public TreballadorFitxaViewModelFactory(IAdministrarTreballadorsService administrarTreballadorsService, INavigator navigator)
        {
            this.administrarTreballadorsService = administrarTreballadorsService;
            this.navigator = navigator;
        }

        public TreballadorFitxaViewModel CreateViewModel()
        {
            return new TreballadorFitxaViewModel(administrarTreballadorsService, navigator);
        }
    }
}
