using DesktopApp.State.Navigators;
using Persistencia.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace DesktopApp.ViewModels.Factories
{
    public class TreballadorAssignatsViewModelFactory : IViewModelFactory<TreballadorAssignatsViewModel>
    {
        private readonly IAdministrarTreballadorsService administrarTreballadorsService;
        private readonly INavigator navigator;

        public TreballadorAssignatsViewModelFactory(IAdministrarTreballadorsService administrarTreballadorsService, INavigator navigator)
        {
            this.administrarTreballadorsService = administrarTreballadorsService;
            this.navigator = navigator;
        }

        public TreballadorAssignatsViewModel CreateViewModel()
        {
            return new TreballadorAssignatsViewModel(administrarTreballadorsService,navigator);
        }
    }
}
